namespace FaceRecognition.App.Forms;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

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
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(960, 640);
        MinimumSize = new Size(800, 540);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Face Recognition Studio";
    }
}
