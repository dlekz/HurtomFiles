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
    public class HeaderElement : Element
    {
        public HeaderElement() : base() => Set();

        private void Set() 
        {
            StackPanel stack = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
            };

            var img = new Image()
            {
                Stretch = Stretch.Fill,
                Height = 110,
                Width = 521,
                Source = LoadImage(Properties.Resources.HurtomTitle),
            };

            stack.Children.Add(img);
            this.Child = stack;

        }

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
