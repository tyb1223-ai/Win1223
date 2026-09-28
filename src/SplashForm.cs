namespace Win1223;

public partial class SplashForm : Form
{
    public SplashForm()
    {
        InitializeComponent();
    }

    public void SetStatus(string text)
    {
        if (InvokeRequired)
        {
            Invoke(() => SetStatus(text));
            return;
        }

        lblStatus.Text = text;
    }

    public void SetProgress(int value)
    {
        if (InvokeRequired)
        {
            Invoke(() => SetProgress(value));
            return;
        }

        progress.Value = Math.Max(0, Math.Min(100, value));
    }
}
