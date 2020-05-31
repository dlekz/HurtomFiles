using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;


namespace HurtomFiles.WPF.Elements
{
    public class StarElement : Image
    {
        public bool Focused { set; get; } = false;

        public readonly BitmapImage WHITE_STAR = ToBitmapImage(Properties.Resources.Five_pointed_star);
        public readonly BitmapImage YELLOW_STAR = ToBitmapImage(Properties.Resources.Five_pointed_star_yellow);
        
        public string Color;
        public StarElement() : base()
        {
            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;
           // this.MouseDown += this_Click;
            Stretch = Stretch.Fill;
            Height = 30;
            Width = 30;
            Source = WHITE_STAR;
            Color = "WHITE";
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

        public void ChangeColor() 
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (Source == WHITE_STAR)
                {
                    SetColor_Yellow();
                }
                else if (Source == YELLOW_STAR)
                {
                    SetColor_White();
                }
            });
        }

        public void SetColor_White() =>
            Application.Current.Dispatcher.Invoke(() => 
            {
                this.Source = WHITE_STAR;
                this.Color = "WHITE";
            });

        public void SetColor_Yellow() =>
            Application.Current.Dispatcher.Invoke(() => 
            {
                this.Source = YELLOW_STAR;
                this.Color = "YELLOW";
            });

    }
}
