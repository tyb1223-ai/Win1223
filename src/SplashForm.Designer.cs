using System.Drawing.Drawing2D;

namespace Win1223;

partial class SplashForm
{
    private Label lblTitle;
    private Label lblStatus;
    private Panel progress;

    private void InitializeComponent()
    {
        lblTitle = new Label();
        lblStatus = new Label();
        progress = new Panel();

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

        progress.Paint += (_, e) =>
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            using var bg =
                new SolidBrush(Color.FromArgb(230,235,242));

            using var fg =
                new SolidBrush(Color.FromArgb(0,120,215));

            var rect =
                new Rectangle(0,0,300,10);

            e.Graphics.FillRoundedRectangle(bg, rect,5);

            var w =
                (int)(300 * progress.Value / 100.0);

            if (w > 0)
                e.Graphics.FillRoundedRectangle(
                    fg,
                    new Rectangle(0,0,w,10),
                    5);
        };

        Controls.Add(lblTitle);
        Controls.Add(lblStatus);
        Controls.Add(progress);

        ResumeLayout(false);
        PerformLayout();
    }
}

static class GraphicsExt
{
    public static void FillRoundedRectangle(
        this Graphics g,
        Brush brush,
        Rectangle r,
        int radius)
    {
        using GraphicsPath path = new();

        path.AddArc(r.X,r.Y,radius,radius,180,90);
        path.AddArc(r.Right-radius,r.Y,radius,radius,270,90);
        path.AddArc(r.Right-radius,r.Bottom-radius,radius,radius,0,90);
        path.AddArc(r.X,r.Bottom-radius,radius,radius,90,90);

        path.CloseFigure();

        g.FillPath(brush,path);
    }
}
