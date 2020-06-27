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
    public class Header : Element
    {
        private readonly TimeInterval timer = new TimeInterval();
        public Header() : base() 
            => Set();

        private void Set()
        {
            //BrushConverter bc = new BrushConverter();
            //Brush brush = (Brush)bc.ConvertFrom("#6689a2");
            //brush.Freeze();
            //this.Background = brush;
            this.Style = App.ThisApp.FindResource("HeaderStyle") as Style;

            var panel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Orientation = Orientation.Horizontal,
            };

            var img = new Image()
            {
                Stretch = Stretch.Fill,
                Height = 110,
                Width = 521,
                Source = LoadImage(Properties.Resources.HurtomTitle),
            };

            panel.Children.Add(img);
            panel.Children.Add(timer);
            this.Child = panel;
        }

        public void WriteTimer() => timer.Write("Час старту");

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
