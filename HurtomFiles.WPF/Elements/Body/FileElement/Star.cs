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
                        ? Properties.Resources.YELLOW_STAR : Properties.Resources.WHITE_STAR;
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
            Source = Properties.Resources.WHITE_STAR;
            Color = StarColors.WHITE;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void SetFocus(object sender, EventArgs e) => Focused = true;

        private void LostFocus(object sender, EventArgs e) => Focused = false;

        public static BitmapImage ToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        public enum StarColors : int
        {
            WHITE = 0,
            YELLOW = 1,
        }
    }
}
