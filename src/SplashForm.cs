namespace Win1223;

public class SplashForm : Form
{
    private readonly Label title;
    private readonly Label status;
    private readonly ProgressBar progress;

    public SplashForm()
    {
        Text = "Win1223";

        ClientSize = new Size(430,220);

        StartPosition = FormStartPosition.CenterScreen;

        FormBorderStyle = FormBorderStyle.FixedDialog;

        MaximizeBox = false;
        MinimizeBox = false;

        BackColor = Color.White;

        Font = new Font("Segoe UI",9);

        title = new Label
        {
            Text="Win1223 Cloud PC",
            Font=new Font("Segoe UI",18,FontStyle.Bold),
            AutoSize=true,
            Location=new Point(60,40)
        };

        status = new Label
        {
            Text="正在连接服务器...",
            AutoSize=true,
            ForeColor=Color.Gray,
            Location=new Point(60,90)
        };

        progress = new ProgressBar
        {
            Location=new Point(60,145),
            Size=new Size(310,16),
            Style=ProgressBarStyle.Continuous
        };

        Controls.Add(title);
        Controls.Add(status);
        Controls.Add(progress);
    }

    public void SetStatus(string text)
    {
        if(InvokeRequired)
        {
            Invoke(()=>SetStatus(text));
            return;
        }

        status.Text=text;
    }

    public void SetProgress(int value)
    {
        if(InvokeRequired)
        {
            Invoke(()=>SetProgress(value));
            return;
        }

        progress.Value=Math.Max(0,Math.Min(100,value));
    }
}
