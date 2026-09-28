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

        // ===== 窗口 =====
        AutoScaleMode = AutoScaleMode.None;
        BackColor = Color.White;
        ForeColor = Color.FromArgb(32, 32, 32);

        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;
        ShowInTaskbar = false;
        MaximizeBox = false;
        MinimizeBox = false;

        // 给 DWM 阴影留 1px 外边距
        Padding = new Padding(1);

        // 双缓冲，避免首次白屏和闪烁
        DoubleBuffered = true;

        ClientSize = new Size(432, 222);

        // ===== 标题 =====
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(32, 32, 32);
        lblTitle.Location = new Point(64, 42);
        lblTitle.Text = "Win1223 Cloud PC";

        // ===== 状态 =====
        lblStatus.AutoSize = true;
        lblStatus.Font = new Font("Segoe UI", 10F);
        lblStatus.ForeColor = Color.FromArgb(100, 110, 120);
        lblStatus.Location = new Point(64, 94);
        lblStatus.Text = "正在连接服务器...";

        // ===== 进度条 =====
        progress.Location = new Point(64, 148);
        progress.Size = new Size(304, 10);

        Controls.Add(lblTitle);
        Controls.Add(lblStatus);
        Controls.Add(progress);

        ResumeLayout(false);
        PerformLayout();
    }
}

internal sealed class ProgressPanel : Control
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
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer |
            ControlStyles.ResizeRedraw,
            true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        var bgRect = new Rectangle(0, 0, Width - 1, Height - 1);

        using var bg = new SolidBrush(Color.FromArgb(230, 235, 242));
        using var fg = new SolidBrush(Color.FromArgb(0, 120, 215));

        FillRound(e.Graphics, bg, bgRect, Height / 2);

        int w = (int)(Width * Value / 100.0);

        if (w > 0)
        {
            FillRound(
                e.Graphics,
                fg,
                new Rectangle(0, 0, w, Height),
                Height / 2);
        }
    }

    private static void FillRound(
        Graphics g,
        Brush brush,
        Rectangle rect,
        int radius)
    {
        using GraphicsPath path = new();

        int d = radius * 2;

        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();

        g.FillPath(brush, path);
    }
}
