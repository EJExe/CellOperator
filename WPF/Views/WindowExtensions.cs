using System.Windows;

namespace WPF.Extensions
{
    public static class WindowExtensions
    {
        public static void CenterWindowOnScreen(this Window window)
        {
            if (window == null) return;

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = window.Width;
            double windowHeight = window.Height;

            if (windowWidth == 0)
            {
                window.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                windowWidth = window.DesiredSize.Width;
            }

            if (windowHeight == 0)
            {
                window.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                windowHeight = window.DesiredSize.Height;
            }

            window.Left = (screenWidth / 2) - (windowWidth / 2);
            window.Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }
}