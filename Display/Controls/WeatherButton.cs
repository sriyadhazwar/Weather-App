using System.Drawing.Drawing2D;
using Button = System.Windows.Forms.Button;

namespace Display.Controls
{
    public class WeatherButton : Button
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }
}