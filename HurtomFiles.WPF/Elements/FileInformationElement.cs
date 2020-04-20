using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HurtomFiles.Logic;
using System.Threading.Tasks;

namespace HurtomFiles.WPF
{
    public class FileInformationElement : Element
    {
        private FileInformationElement() : base(Brushes.WhiteSmoke, Brushes.Black,
            thickness: new Thickness(3), margin: new Thickness(5, 5, 0, 5)){ }

        public FileInformationElement(string uri) : this()
        {
            var info = Task.Run(() => new FileInformation(uri)).Result;

            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;
            Set(info);
        }

        public FileInformationElement(FileInformation info) : this()
        {
            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;
            Set(info); 
        }

        private void Set(FileInformation info) 
        {
            this.Width = 200;
            this.Height = 300;

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

        private void SetFocus(object sender, EventArgs e) 
        {
            this.BorderBrush = Brushes.Green;  
        }

        private void LostFocus(object sender, EventArgs e) 
        {
            this.BorderBrush = Brushes.Black;
        }
    }
}
