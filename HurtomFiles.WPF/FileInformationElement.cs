using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HurtomFiles.Logic;
using System.Threading.Tasks;

namespace HurtomFiles.WPF
{
    public class FileInformationElement : Border
    {
        public FileInformationElement(string uri)
        {
            var info = Task.Run(() => new FileInformation(uri)).Result;

            Set(info);
        }

        public FileInformationElement(FileInformation info) => Set(info);

        private void Set(FileInformation info) 
        {
            this.Width = 200;
            this.Height = 300;
            this.Background = Brushes.WhiteSmoke;
            this.BorderBrush = Brushes.Black;
            this.BorderThickness = new Thickness(2);
            this.Padding = new Thickness(5);
            this.Margin = new Thickness(5);

            StackPanel stack = new StackPanel();

            TextBlock text = new TextBlock()
            {
                Text = info.title.ToString(),
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Verdana Bold"),
            };

            stack.Children.Add(SetImage(new Uri(info.imageUri), this.Height - 60));
            stack.Children.Add(text);
            this.Child = stack;
        }

        private Image SetImage(Uri uri, double height) =>
            new Image()
            {
                Stretch = Stretch.Fill,
                Source = new BitmapImage(uri),
                Height = height,
            };
    }
}
