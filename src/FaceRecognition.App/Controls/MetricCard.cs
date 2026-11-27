using FaceRecognition.App.Presentation;

namespace FaceRecognition.App.Controls;

internal sealed class MetricCard : UserControl
{
    private readonly Font titleFont;
    private readonly Font valueFont;
    private readonly Label titleLabel;
    private readonly Label valueLabel;

    public MetricCard(string title, string initialValue)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);

        titleFont = new Font("Segoe UI", 8.5F, FontStyle.Regular, GraphicsUnit.Point);
        valueFont = new Font("Segoe UI Semibold", 13F, FontStyle.Bold, GraphicsUnit.Point);
        titleLabel = new Label();
        valueLabel = new Label();

        BackColor = AppTheme.Surface;
        Margin = new Padding(5);
        MinimumSize = new Size(105, 76);
        Padding = new Padding(14, 11, 14, 10);
        TabStop = false;

        titleLabel.AutoEllipsis = true;
        titleLabel.Dock = DockStyle.Top;
        titleLabel.Font = titleFont;
        titleLabel.ForeColor = AppTheme.MutedText;
        titleLabel.Height = 23;
        titleLabel.Text = title;
        titleLabel.TextAlign = ContentAlignment.MiddleLeft;

        valueLabel.AutoEllipsis = true;
        valueLabel.Dock = DockStyle.Fill;
        valueLabel.Font = valueFont;
        valueLabel.ForeColor = AppTheme.Text;
        valueLabel.Text = initialValue;
        valueLabel.TextAlign = ContentAlignment.MiddleLeft;

        Controls.Add(valueLabel);
        Controls.Add(titleLabel);
        AccessibleName = title;
        AccessibleDescription = $"{title}: {initialValue}";
    }

    public void UpdateValue(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        valueLabel.Text = value;
        AccessibleDescription = $"{titleLabel.Text}: {value}";
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        ControlPaint.DrawBorder(e.Graphics, ClientRectangle, AppTheme.Border, ButtonBorderStyle.Solid);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            titleFont.Dispose();
            valueFont.Dispose();
        }

        base.Dispose(disposing);
    }
}
