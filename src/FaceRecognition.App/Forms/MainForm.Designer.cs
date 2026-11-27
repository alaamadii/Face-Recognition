using FaceRecognition.App.Controls;
using FaceRecognition.App.Presentation;

namespace FaceRecognition.App.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private ToolTip interfaceToolTip = null;
    private TableLayoutPanel rootLayout = null;
    private TableLayoutPanel sidebarLayout = null;
    private Button recognitionNavigationButton = null;
    private Button datasetNavigationButton = null;
    private Button settingsNavigationButton = null;
    private TableLayoutPanel contentLayout = null;
    private Label pageEyebrowLabel = null;
    private Label pageTitleLabel = null;
    private Label pageSubtitleLabel = null;
    private Panel pageHostPanel = null;
    private Panel recognitionPanel = null;
    private Panel datasetPanel = null;
    private Panel settingsPanel = null;
    private Button startCameraButton = null;
    private Button stopCameraButton = null;
    private Button selectImageButton = null;
    private Button clearPreviewButton = null;
    private PictureBox previewPictureBox = null;
    private Label previewPlaceholderLabel = null;
    private MetricCard detectedFacesCard = null;
    private MetricCard predictedLabelCard = null;
    private MetricCard distanceCard = null;
    private MetricCard cameraStatusCard = null;
    private MetricCard modelStatusCard = null;
    private Panel feedbackPanel = null;
    private Label feedbackMessageLabel = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        interfaceToolTip = new ToolTip(components);
        rootLayout = new TableLayoutPanel();
        sidebarLayout = new TableLayoutPanel();
        contentLayout = new TableLayoutPanel();
        pageEyebrowLabel = new Label();
        pageTitleLabel = new Label();
        pageSubtitleLabel = new Label();
        pageHostPanel = new Panel();
        recognitionPanel = CreateRecognitionPage();
        datasetPanel = CreatePlaceholderPage(
            "Veri Seti ve Eğitim",
            "PLANLANDI — PR #04",
            "Gönüllü yerel örneklerin eklenmesi, doğrulanması, silinmesi ve LBPH model eğitimi bu aşamada uygulanmamıştır.",
            "Yalnızca bilgilendirilmiş gönüllülerden alınan örnekler kullanılacak ve tüm veriler cihazda tutulacaktır.");
        settingsPanel = CreatePlaceholderPage(
            "Kamera ve Model Ayarları",
            "PLANLANDI — SONRAKİ AŞAMALAR",
            "Kamera seçimi, model bilgisi, tanıma eşikleri ve yerel depolama yönetimi özellikleri henüz kullanılabilir değildir.",
            "Uygulama bulut bağlantısı kurmaz. Biyometrik örnekler ve eğitilmiş modeller Git dışında, yerel olarak saklanacaktır.");

        SuspendLayout();

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = AppTheme.PageBackground;
        ClientSize = new Size(1240, 800);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        MinimumSize = new Size(1024, 680);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Face Recognition Studio";

        rootLayout.ColumnCount = 2;
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 224F));
        rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        rootLayout.Dock = DockStyle.Fill;
        rootLayout.Margin = Padding.Empty;
        rootLayout.RowCount = 1;
        rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        ConfigureSidebar();
        ConfigureContent();

        rootLayout.Controls.Add(sidebarLayout, 0, 0);
        rootLayout.Controls.Add(contentLayout, 1, 0);
        Controls.Add(rootLayout);

        ResumeLayout(performLayout: true);
    }

    private void ConfigureSidebar()
    {
        sidebarLayout.BackColor = AppTheme.Sidebar;
        sidebarLayout.ColumnCount = 1;
        sidebarLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        sidebarLayout.Dock = DockStyle.Fill;
        sidebarLayout.Margin = Padding.Empty;
        sidebarLayout.Padding = new Padding(12, 0, 12, 16);
        sidebarLayout.RowCount = 6;
        sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 118F));
        sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
        sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        sidebarLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 132F));

        var brandPanel = new Panel
        {
            BackColor = AppTheme.Sidebar,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            Padding = new Padding(8, 25, 8, 12),
        };
        var brandTitleLabel = new Label
        {
            AutoEllipsis = true,
            Dock = DockStyle.Top,
            Font = new Font("Segoe UI Semibold", 15F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.White,
            Height = 34,
            Text = "Face Recognition",
            TextAlign = ContentAlignment.MiddleLeft,
        };
        var brandSubtitleLabel = new Label
        {
            AutoEllipsis = true,
            Dock = DockStyle.Top,
            Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = AppTheme.SidebarMutedText,
            Height = 28,
            Text = "YMH219 · Yerel CV Stüdyosu",
            TextAlign = ContentAlignment.MiddleLeft,
        };
        brandPanel.Controls.Add(brandSubtitleLabel);
        brandPanel.Controls.Add(brandTitleLabel);

        recognitionNavigationButton = CreateNavigationButton(
            "Tanıma",
            RecognitionNavigationButton_Click,
            tabIndex: 0);
        datasetNavigationButton = CreateNavigationButton(
            "Veri Seti",
            DatasetNavigationButton_Click,
            tabIndex: 1);
        settingsNavigationButton = CreateNavigationButton(
            "Ayarlar",
            SettingsNavigationButton_Click,
            tabIndex: 2);

        var privacyPanel = new Panel
        {
            BackColor = AppTheme.SidebarHover,
            Dock = DockStyle.Fill,
            Margin = new Padding(4, 10, 4, 0),
            Padding = new Padding(14, 12, 14, 10),
        };
        var privacyTitleLabel = new Label
        {
            Dock = DockStyle.Top,
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.White,
            Height = 25,
            Text = "Yerel işleme",
        };
        var privacyBodyLabel = new Label
        {
            AutoEllipsis = true,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = AppTheme.SidebarMutedText,
            Text = "Bulut bağlantısı yok. Sonuçlar deneyseldir ve kimlik doğrulaması değildir.",
        };
        privacyPanel.Controls.Add(privacyBodyLabel);
        privacyPanel.Controls.Add(privacyTitleLabel);

        sidebarLayout.Controls.Add(brandPanel, 0, 0);
        sidebarLayout.Controls.Add(recognitionNavigationButton, 0, 1);
        sidebarLayout.Controls.Add(datasetNavigationButton, 0, 2);
        sidebarLayout.Controls.Add(settingsNavigationButton, 0, 3);
        sidebarLayout.Controls.Add(privacyPanel, 0, 5);
    }

    private void ConfigureContent()
    {
        contentLayout.BackColor = AppTheme.PageBackground;
        contentLayout.ColumnCount = 1;
        contentLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        contentLayout.Dock = DockStyle.Fill;
        contentLayout.Margin = Padding.Empty;
        contentLayout.RowCount = 2;
        contentLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
        contentLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        var headerPanel = new Panel
        {
            BackColor = AppTheme.PageBackground,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            Padding = new Padding(30, 18, 30, 8),
        };
        pageEyebrowLabel.AutoEllipsis = true;
        pageEyebrowLabel.Dock = DockStyle.Top;
        pageEyebrowLabel.Font = new Font("Segoe UI Semibold", 8F, FontStyle.Bold, GraphicsUnit.Point);
        pageEyebrowLabel.ForeColor = AppTheme.Accent;
        pageEyebrowLabel.Height = 20;

        pageTitleLabel.AutoEllipsis = true;
        pageTitleLabel.Dock = DockStyle.Top;
        pageTitleLabel.Font = new Font("Segoe UI Semibold", 21F, FontStyle.Bold, GraphicsUnit.Point);
        pageTitleLabel.ForeColor = AppTheme.Text;
        pageTitleLabel.Height = 40;

        pageSubtitleLabel.AutoEllipsis = true;
        pageSubtitleLabel.Dock = DockStyle.Fill;
        pageSubtitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        pageSubtitleLabel.ForeColor = AppTheme.MutedText;

        headerPanel.Controls.Add(pageSubtitleLabel);
        headerPanel.Controls.Add(pageTitleLabel);
        headerPanel.Controls.Add(pageEyebrowLabel);

        pageHostPanel.BackColor = AppTheme.PageBackground;
        pageHostPanel.Dock = DockStyle.Fill;
        pageHostPanel.Margin = Padding.Empty;
        pageHostPanel.Padding = new Padding(30, 0, 30, 26);
        pageHostPanel.Controls.Add(settingsPanel);
        pageHostPanel.Controls.Add(datasetPanel);
        pageHostPanel.Controls.Add(recognitionPanel);

        contentLayout.Controls.Add(headerPanel, 0, 0);
        contentLayout.Controls.Add(pageHostPanel, 0, 1);
    }

    private Panel CreateRecognitionPage()
    {
        var page = new Panel
        {
            BackColor = AppTheme.PageBackground,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
        };
        var layout = new TableLayoutPanel
        {
            BackColor = AppTheme.PageBackground,
            ColumnCount = 1,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            RowCount = 3,
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 68F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 86F));

        var actions = new FlowLayoutPanel
        {
            AutoScroll = true,
            BackColor = AppTheme.PageBackground,
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            Margin = Padding.Empty,
            Padding = new Padding(0, 8, 0, 8),
            WrapContents = false,
        };

        startCameraButton = CreateActionButton(
            "Kamerayı Başlat",
            isPrimary: true,
            StartCameraButton_Click,
            tabIndex: 3,
            width: 148);
        stopCameraButton = CreateActionButton(
            "Kamerayı Durdur",
            isPrimary: false,
            StopCameraButton_Click,
            tabIndex: 4,
            width: 154);
        selectImageButton = CreateActionButton(
            "Görüntü Seç",
            isPrimary: false,
            SelectImageButton_Click,
            tabIndex: 5,
            width: 132);
        clearPreviewButton = CreateActionButton(
            "Önizlemeyi Temizle",
            isPrimary: false,
            ClearPreviewButton_Click,
            tabIndex: 6,
            width: 168);

        interfaceToolTip.SetToolTip(startCameraButton, "Kamera desteği PR #02 kapsamında eklenecek.");
        interfaceToolTip.SetToolTip(stopCameraButton, "Etkin kamera oturumunu durdurur.");
        interfaceToolTip.SetToolTip(selectImageButton, "Yerel görüntü seçimi PR #02 kapsamında eklenecek.");
        interfaceToolTip.SetToolTip(clearPreviewButton, "Görüntüyü ve geçici sonuçları temizler.");

        actions.Controls.Add(startCameraButton);
        actions.Controls.Add(stopCameraButton);
        actions.Controls.Add(selectImageButton);
        actions.Controls.Add(clearPreviewButton);

        var workspace = new TableLayoutPanel
        {
            BackColor = AppTheme.PageBackground,
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            RowCount = 1,
        };
        workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
        workspace.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
        workspace.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        Panel previewCard = CreatePreviewCard();
        TableLayoutPanel statusColumn = CreateStatusColumn();
        workspace.Controls.Add(previewCard, 0, 0);
        workspace.Controls.Add(statusColumn, 1, 0);

        feedbackPanel = new Panel
        {
            BackColor = AppTheme.InfoBackground,
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 12, 0, 0),
            Padding = new Padding(18, 12, 18, 10),
        };
        var feedbackTitleLabel = new Label
        {
            Dock = DockStyle.Top,
            Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = AppTheme.Text,
            Height = 22,
            Text = "DURUM",
        };
        feedbackMessageLabel = new Label
        {
            AutoEllipsis = true,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = AppTheme.Info,
            Text = "Hazır. Kamera etkin değil ve yerel tanıma modeli kullanılamıyor.",
            TextAlign = ContentAlignment.MiddleLeft,
        };
        feedbackMessageLabel.AccessibleName = "İşlem ve hata geri bildirimi";
        feedbackPanel.Controls.Add(feedbackMessageLabel);
        feedbackPanel.Controls.Add(feedbackTitleLabel);

        layout.Controls.Add(actions, 0, 0);
        layout.Controls.Add(workspace, 0, 1);
        layout.Controls.Add(feedbackPanel, 0, 2);
        page.Controls.Add(layout);
        return page;
    }

    private Panel CreatePreviewCard()
    {
        var card = new Panel
        {
            BackColor = AppTheme.Surface,
            Dock = DockStyle.Fill,
            Margin = new Padding(0, 0, 12, 0),
            Padding = new Padding(1),
        };
        var layout = new TableLayoutPanel
        {
            BackColor = AppTheme.Surface,
            ColumnCount = 1,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            Padding = new Padding(14, 0, 14, 14),
            RowCount = 2,
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        var previewHeaderLabel = new Label
        {
            AutoEllipsis = true,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = AppTheme.Text,
            Text = "Kamera ve görüntü önizlemesi",
            TextAlign = ContentAlignment.MiddleLeft,
        };
        var previewSurface = new Panel
        {
            BackColor = AppTheme.PreviewBackground,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
        };
        previewPictureBox = new PictureBox
        {
            AccessibleDescription = "Kamera karesi veya seçilen yerel görüntü burada gösterilir.",
            AccessibleName = "Yüz tanıma önizlemesi",
            BackColor = AppTheme.PreviewBackground,
            Dock = DockStyle.Fill,
            SizeMode = PictureBoxSizeMode.Zoom,
            TabIndex = 7,
            TabStop = false,
        };
        previewPlaceholderLabel = new Label
        {
            BackColor = AppTheme.PreviewBackground,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = Color.FromArgb(209, 213, 219),
            Text = "Önizleme yok\r\nKamera başlatın veya yerel bir görüntü seçin.",
            TextAlign = ContentAlignment.MiddleCenter,
        };
        previewSurface.Controls.Add(previewPlaceholderLabel);
        previewSurface.Controls.Add(previewPictureBox);
        previewPlaceholderLabel.BringToFront();

        layout.Controls.Add(previewHeaderLabel, 0, 0);
        layout.Controls.Add(previewSurface, 0, 1);
        card.Controls.Add(layout);
        return card;
    }

    private TableLayoutPanel CreateStatusColumn()
    {
        var column = new TableLayoutPanel
        {
            BackColor = AppTheme.PageBackground,
            ColumnCount = 1,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            RowCount = 2,
        };
        column.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        column.RowStyles.Add(new RowStyle(SizeType.Absolute, 240F));
        column.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

        var metrics = new TableLayoutPanel
        {
            BackColor = AppTheme.PageBackground,
            ColumnCount = 2,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            RowCount = 3,
        };
        metrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        metrics.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        metrics.RowStyles.Add(new RowStyle(SizeType.Percent, 33.333F));
        metrics.RowStyles.Add(new RowStyle(SizeType.Percent, 33.333F));
        metrics.RowStyles.Add(new RowStyle(SizeType.Percent, 33.334F));

        detectedFacesCard = new MetricCard("Algılanan yüz", "0") { Dock = DockStyle.Fill };
        predictedLabelCard = new MetricCard("Tahmin etiketi", "—") { Dock = DockStyle.Fill };
        distanceCard = new MetricCard("LBPH uzaklığı", "—") { Dock = DockStyle.Fill };
        cameraStatusCard = new MetricCard("Kamera", "Durduruldu") { Dock = DockStyle.Fill };
        modelStatusCard = new MetricCard("Model", "Kullanılamıyor") { Dock = DockStyle.Fill };

        metrics.Controls.Add(detectedFacesCard, 0, 0);
        metrics.Controls.Add(predictedLabelCard, 1, 0);
        metrics.Controls.Add(distanceCard, 0, 1);
        metrics.Controls.Add(cameraStatusCard, 1, 1);
        metrics.Controls.Add(modelStatusCard, 0, 2);
        metrics.SetColumnSpan(modelStatusCard, 2);

        Panel resultNotice = CreateInformationCard(
            "Sonuçları anlama",
            "Henüz bir model yüklenmedi. Gelecekte gösterilecek LBPH değeri bir uzaklıktır; olasılık, doğruluk yüzdesi veya doğrulanmış kimlik değildir.");

        column.Controls.Add(metrics, 0, 0);
        column.Controls.Add(resultNotice, 0, 1);
        return column;
    }

    private static Panel CreateInformationCard(string title, string body)
    {
        var panel = new Panel
        {
            BackColor = AppTheme.Surface,
            Dock = DockStyle.Fill,
            Margin = new Padding(5, 8, 5, 0),
            Padding = new Padding(16, 13, 16, 12),
        };
        var titleLabel = new Label
        {
            AutoEllipsis = true,
            Dock = DockStyle.Top,
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = AppTheme.Text,
            Height = 27,
            Text = title,
        };
        var bodyLabel = new Label
        {
            AutoEllipsis = true,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = AppTheme.MutedText,
            Text = body,
        };
        panel.Controls.Add(bodyLabel);
        panel.Controls.Add(titleLabel);
        return panel;
    }

    private static Panel CreatePlaceholderPage(string title, string status, string description, string privacyNote)
    {
        var page = new Panel
        {
            BackColor = AppTheme.PageBackground,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            Padding = new Padding(0, 8, 0, 0),
            Visible = false,
        };
        var card = new Panel
        {
            BackColor = AppTheme.Surface,
            Dock = DockStyle.Top,
            Height = 270,
            Padding = new Padding(30, 28, 30, 24),
        };
        var layout = new TableLayoutPanel
        {
            BackColor = AppTheme.Surface,
            ColumnCount = 1,
            Dock = DockStyle.Fill,
            Margin = Padding.Empty,
            RowCount = 4,
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
        layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));

        var titleLabel = new Label
        {
            AutoEllipsis = true,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = AppTheme.Text,
            Text = title,
            TextAlign = ContentAlignment.MiddleLeft,
        };
        var statusLabel = new Label
        {
            AutoEllipsis = true,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = AppTheme.Accent,
            Text = status,
            TextAlign = ContentAlignment.MiddleLeft,
        };
        var descriptionLabel = new Label
        {
            AutoEllipsis = true,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = AppTheme.MutedText,
            Text = description,
            TextAlign = ContentAlignment.MiddleLeft,
        };
        var privacyLabel = new Label
        {
            AutoEllipsis = true,
            BackColor = AppTheme.InfoBackground,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point),
            ForeColor = AppTheme.Info,
            Padding = new Padding(12, 8, 12, 8),
            Text = privacyNote,
            TextAlign = ContentAlignment.MiddleLeft,
        };
        layout.Controls.Add(titleLabel, 0, 0);
        layout.Controls.Add(statusLabel, 0, 1);
        layout.Controls.Add(descriptionLabel, 0, 2);
        layout.Controls.Add(privacyLabel, 0, 3);
        card.Controls.Add(layout);
        page.Controls.Add(card);
        return page;
    }

    private static Button CreateNavigationButton(string text, EventHandler handler, int tabIndex)
    {
        var button = new Button
        {
            AccessibleDescription = $"{text} bölümünü açar.",
            AccessibleName = text,
            BackColor = AppTheme.Sidebar,
            Cursor = Cursors.Hand,
            Dock = DockStyle.Fill,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = Color.White,
            Margin = new Padding(0, 4, 0, 4),
            Padding = new Padding(14, 0, 0, 0),
            TabIndex = tabIndex,
            Text = text,
            TextAlign = ContentAlignment.MiddleLeft,
            UseVisualStyleBackColor = false,
        };
        button.FlatAppearance.BorderSize = 0;
        button.FlatAppearance.MouseOverBackColor = AppTheme.SidebarHover;
        button.Click += handler;
        return button;
    }

    private static Button CreateActionButton(
        string text,
        bool isPrimary,
        EventHandler handler,
        int tabIndex,
        int width)
    {
        var button = new Button
        {
            AccessibleName = text,
            BackColor = isPrimary ? AppTheme.Accent : AppTheme.Surface,
            Cursor = Cursors.Hand,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point),
            ForeColor = isPrimary ? Color.White : AppTheme.Text,
            Height = 44,
            Margin = new Padding(0, 0, 10, 0),
            TabIndex = tabIndex,
            Text = text,
            UseVisualStyleBackColor = false,
            Width = width,
        };
        button.FlatAppearance.BorderColor = isPrimary ? AppTheme.Accent : AppTheme.Border;
        button.FlatAppearance.BorderSize = 1;
        button.FlatAppearance.MouseOverBackColor = isPrimary ? AppTheme.AccentHover : AppTheme.PageBackground;
        button.Click += handler;
        return button;
    }
}
