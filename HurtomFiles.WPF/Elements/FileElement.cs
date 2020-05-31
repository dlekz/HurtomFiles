using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HurtomFiles.Logic;
using System.Threading.Tasks;
using System.Diagnostics;
using HurtomFiles.WPF.Elements;

namespace HurtomFiles.WPF
{
    public class FileElement : Element
    {

        public readonly FilePage source;
        private TextBlock text;

        public StarElement star = new StarElement();


        public bool Focused { set; get; } = false;

        public FileElement() : base(Brushes.WhiteSmoke, Brushes.Black,
            thickness: new Thickness(3), margin: new Thickness(5, 5, 0, 5))
        {
            this.Width = 200;
            this.Height = 300;
        }

        public FileElement(FilePage info) : this()
        {
            source = info;
            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;
            Set(info);
        }

        private void Set(FilePage info)
        {          
            StackPanel stack = new StackPanel();
            //stack.Children.Add(star);
            text = new TextBlock()
            {
                Text = info.title.ToString(),
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Verdana Bold"),
                Foreground = Brushes.Black
            };
            if (info.imageUri != "")
            {
                stack.Children.Add(SetImage(new Uri(info.imageUri), this.Height - 60));
            }

            stack.Children.Add(text);

            Grid grid = new Grid();

            grid.Children.Add(stack);
            grid.Children.Add(star);

            Grid.SetZIndex(star, 10);

            this.Child = grid;
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
            this.text.Foreground = Brushes.Green;
            Focused = true;
        }

        private void LostFocus(object sender, EventArgs e)
        {
            this.BorderBrush = Brushes.Black;
            this.text.Foreground = Brushes.Black;
            Focused = false;
        }
    }
}
