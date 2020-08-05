using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;


namespace HurtomFiles.WPF
{
    public class Star : Image
    {
        public bool Focused { private set; get; } = false;

        private StarColors color;

        public StarColors Color 

        {
            set 
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    this.Source = (value == StarColors.YELLOW)
                        ? App.ThisApp.FindResource("YellowStar") as BitmapImage 
                        : App.ThisApp.FindResource("WhiteStar") as BitmapImage;
                    color = value;
                });
            }

            get => color;
        }

        public Star() : base()
        {
            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;
            Stretch = Stretch.Fill;
            Height = 30;
            Width = 30;
            Source = App.ThisApp.FindResource("WhiteStar") as BitmapImage;
            Color = StarColors.WHITE;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void SetFocus(object sender, EventArgs e) => Focused = true;

        private void LostFocus(object sender, EventArgs e) => Focused = false;

        public enum StarColors : int
        {
            WHITE = 0,
            YELLOW = 1,
        }
    }
}
