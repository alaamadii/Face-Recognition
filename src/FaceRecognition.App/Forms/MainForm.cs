using FaceRecognition.App.Presentation;
using FaceRecognition.Core.States;

namespace FaceRecognition.App.Forms;

/// <summary>Main application interface and presentation-state owner.</summary>
public partial class MainForm : Form
{
    private ApplicationState applicationState = ApplicationState.Idle;

    public MainForm()
    {
        InitializeComponent();
        ShowSection(ApplicationSection.Recognition);
        ResetRecognitionDisplay();
        ApplyApplicationState(applicationState);
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        ReplacePreviewImage(null);
        base.OnFormClosed(e);
    }

    private void RecognitionNavigationButton_Click(object? sender, EventArgs e) =>
        ShowSection(ApplicationSection.Recognition);

    private void DatasetNavigationButton_Click(object? sender, EventArgs e) =>
        ShowSection(ApplicationSection.Dataset);

    private void SettingsNavigationButton_Click(object? sender, EventArgs e) =>
        ShowSection(ApplicationSection.Settings);

    private void StartCameraButton_Click(object? sender, EventArgs e) =>
        SetFeedback(
            "Kamera bağlantısı PR #02 kapsamında eklenecek. Kamera şu anda başlatılmadı.",
            FeedbackTone.Information);

    private void StopCameraButton_Click(object? sender, EventArgs e) =>
        SetFeedback("Etkin bir kamera oturumu bulunmuyor.", FeedbackTone.Information);

    private void SelectImageButton_Click(object? sender, EventArgs e) =>
        SetFeedback(
            "Yerel görüntü seçimi PR #02 kapsamında eklenecek. Herhangi bir dosya açılmadı.",
            FeedbackTone.Information);

    private void ClearPreviewButton_Click(object? sender, EventArgs e)
    {
        ReplacePreviewImage(null);
        ResetRecognitionDisplay();

        if (applicationState.Mode is not ApplicationMode.Idle)
        {
            applicationState = applicationState.TransitionTo(ApplicationMode.Idle);
        }

        ApplyApplicationState(applicationState);
        SetFeedback("Önizleme ve geçici sonuçlar temizlendi.", FeedbackTone.Success);
    }

    private void ApplyApplicationState(ApplicationState state)
    {
        applicationState = state;
        ApplicationCapabilities capabilities = ApplicationCapabilities.From(state);

        startCameraButton.Enabled = capabilities.CanStartCamera;
        stopCameraButton.Enabled = capabilities.CanStopCamera;
        selectImageButton.Enabled = capabilities.CanSelectImage;
        clearPreviewButton.Enabled = capabilities.CanClearPreview;

        cameraStatusCard.UpdateValue(state.Mode switch
        {
            ApplicationMode.CameraStarting => "Başlatılıyor",
            ApplicationMode.CameraActive => "Aktif",
            ApplicationMode.Faulted => "Hata",
            _ => "Durduruldu",
        });

        if (state.Mode is ApplicationMode.Faulted)
        {
            SetFeedback(state.Message ?? "Beklenmeyen bir uygulama hatası oluştu.", FeedbackTone.Error);
        }
    }

    private void ShowSection(ApplicationSection section)
    {
        recognitionPanel.Visible = section is ApplicationSection.Recognition;
        datasetPanel.Visible = section is ApplicationSection.Dataset;
        settingsPanel.Visible = section is ApplicationSection.Settings;

        Panel selectedPanel = section switch
        {
            ApplicationSection.Recognition => recognitionPanel,
            ApplicationSection.Dataset => datasetPanel,
            ApplicationSection.Settings => settingsPanel,
            _ => throw new ArgumentOutOfRangeException(nameof(section), section, null),
        };
        selectedPanel.BringToFront();

        SetNavigationButtonState(recognitionNavigationButton, section is ApplicationSection.Recognition);
        SetNavigationButtonState(datasetNavigationButton, section is ApplicationSection.Dataset);
        SetNavigationButtonState(settingsNavigationButton, section is ApplicationSection.Settings);

        (pageEyebrowLabel.Text, pageTitleLabel.Text, pageSubtitleLabel.Text) = section switch
        {
            ApplicationSection.Recognition => (
                "YÜZ ALGILAMA VE TANIMA",
                "Tanıma",
                "Kamera veya yerel görüntü üzerinden deneysel, tamamen yerel işleme."),
            ApplicationSection.Dataset => (
                "GÖNÜLLÜ YEREL ÖRNEKLER",
                "Veri Seti",
                "Örnek yönetimi ve LBPH eğitimi PR #04 kapsamında eklenecek."),
            ApplicationSection.Settings => (
                "UYGULAMA BİLGİLERİ",
                "Ayarlar",
                "Kamera, model ve yerel depolama seçenekleri sonraki aşamalarda eklenecek."),
            _ => throw new ArgumentOutOfRangeException(nameof(section), section, null),
        };
    }

    private static void SetNavigationButtonState(Button button, bool isActive)
    {
        button.BackColor = isActive ? AppTheme.Accent : AppTheme.Sidebar;
        button.ForeColor = Color.White;
    }

    private void ResetRecognitionDisplay()
    {
        detectedFacesCard.UpdateValue("0");
        predictedLabelCard.UpdateValue("—");
        distanceCard.UpdateValue("—");
        modelStatusCard.UpdateValue("Kullanılamıyor");
        previewPlaceholderLabel.Visible = previewPictureBox.Image is null;
    }

    private void ReplacePreviewImage(Image? nextImage)
    {
        if (ReferenceEquals(previewPictureBox.Image, nextImage))
        {
            return;
        }

        Image? previousImage = previewPictureBox.Image;
        previewPictureBox.Image = nextImage;
        previewPlaceholderLabel.Visible = nextImage is null;
        previousImage?.Dispose();
    }

    private void SetFeedback(string message, FeedbackTone tone)
    {
        feedbackMessageLabel.Text = message;
        feedbackPanel.BackColor = tone switch
        {
            FeedbackTone.Information => AppTheme.InfoBackground,
            FeedbackTone.Success => AppTheme.SuccessBackground,
            FeedbackTone.Error => AppTheme.DangerBackground,
            _ => AppTheme.InfoBackground,
        };
        feedbackMessageLabel.ForeColor = tone switch
        {
            FeedbackTone.Information => AppTheme.Info,
            FeedbackTone.Success => AppTheme.Success,
            FeedbackTone.Error => AppTheme.Danger,
            _ => AppTheme.Info,
        };
    }

    private enum ApplicationSection
    {
        Recognition,
        Dataset,
        Settings,
    }

    private enum FeedbackTone
    {
        Information,
        Success,
        Error,
    }
}
