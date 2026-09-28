using System.Drawing.Drawing2D;

namespace Win1223;

partial class SplashForm
{
    private Label lblTitle;
    private Label lblStatus;
    private ProgressPanel progress;

    private void InitializeComponent()
    {
        lblTitle = new Label();
        lblStatus = new Label();
        progress = new ProgressPanel();

        SuspendLayout();

        BackColor = Color.White;
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(430,220);

        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI",18,FontStyle.Bold);
        lblTitle.Location = new Point(65,45);
        lblTitle.Text = "Win1223 Cloud PC";

        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI",10);
        lblStatus.Location = new Point(65,95);
        lblStatus.Text = "正在连接服务器...";

        progress.Location = new Point(65,145);
        progress.Size = new Size(300,10);

        Controls.Add(lblTitle);
        Controls.Add(lblStatus);
        Controls.Add(progress);

        ResumeLayout(false);
        PerformLayout();
    }
}

internal class ProgressPanel : Control
{
    private int _value;

    public int Value
    {
        get => _value;
        set
        {
            _value = Math.Max(0, Math.Min(100, value));
            Invalidate();
        }
    }

    public ProgressPanel()
    {
        DoubleBuffered = true;
        SetStyle(
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.UserPaint |
            ControlStyles.OptimizedDoubleBuffer,
            true);

        BackColor = Color.Transparent;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        var rect = new Rectangle(0,0,Width-1,Height-1);

        using var bg =
            new SolidBrush(Color.FromArgb(230,235,242));

        using var fg =
            new SolidBrush(Color.FromArgb(0,120,215));

        FillRound(e.Graphics,bg,rect,Height/2);

        int w = (int)(Width * Value / 100.0);

        if (w > 0)
        {
            FillRound(
                e.Graphics,
                fg,
                new Rectangle(0,0,w,Height),
                Height/2);
        }
    }

    private static void FillRound(
        Graphics g,
        Brush brush,
        Rectangle r,
        int radius)
    {
        using GraphicsPath path = new();

        path.AddArc(r.X,r.Y,radius*2,radius*2,180,90);
        path.AddArc(r.Right-radius*2,r.Y,radius*2,radius*2,270,90);
        path.AddArc(r.Right-radius*2,r.Bottom-radius*2,radius*2,radius*2,0,90);
        path.AddArc(r.X,r.Bottom-radius*2,radius*2,radius*2,90,90);

        path.CloseFigure();

        g.FillPath(brush,path);
    }
}
