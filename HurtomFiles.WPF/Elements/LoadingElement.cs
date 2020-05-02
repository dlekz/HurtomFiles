using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Media.Animation;

namespace HurtomFiles.WPF
{
    public class LoadingElement : Border
    {
        // TODO: this element must be rotated
        public LoadingElement() 
        {
            this.Height = 100;
            this.Width = 100;
            this.Margin = new Thickness(5, 50, 5, 50);

            Bitmap bitmap = Properties.Resources.Loading;
            bitmap.MakeTransparent(bitmap.GetPixel(0, 0));

            this.Background = new ImageBrush(ToBitmapImage(bitmap));

            //LoadingElement_Rotate();
            RotateTransform rotate = new RotateTransform(50);
            this.RenderTransform = rotate;
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
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

        public RotateTransform LoadingElement_Rotate() 
        {
            RotateTransform rotate = new RotateTransform()
            {
                CenterX = 50,
                CenterY = 50,
            };
            
            this.RenderTransform = rotate;

            EventTrigger loaded = new EventTrigger();
            //loaded.RoutedEvent =             

            //Storyboard storyboard = new Storyboard() 
            //{
            //    RepeatBehavior = 
            //};

            return rotate;
        }
    }
}
