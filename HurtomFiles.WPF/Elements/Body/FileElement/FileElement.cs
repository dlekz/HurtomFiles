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
    public class FileElement : Border
    {

        public readonly FilePage source;
        private TextBlock text;

        private Star star = new Star();

        public bool Focused { set; get; } = false;
        
        [Obsolete]
        public bool StarFocused => star.Focused;

        public Star.StarColors StarColor 
        {
            set => star.Color = value;
            get => star.Color;
        }

        public FileElement(FilePage info)
        {
            source = info;
            Set(info);
        }

        public FileElement(string uri)
        {
            source = Task.Run(() => new FilePage(uri)).Result;
            Set(source);
        }

        private void Set(FilePage info)
        {
            this.Style = App.ThisApp.FindResource("ElementStyle") as Style;

            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;

            StackPanel stack = new StackPanel();

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
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#6689a2");
            brush.Freeze();

            this.BorderBrush = brush;
            this.text.Foreground = brush;
            Focused = true;
        }

        private void LostFocus(object sender, EventArgs e)
        {
            this.BorderBrush = Brushes.Black;
            this.text.Foreground = Brushes.Black;
            Focused = false;
        }

        public void MouseClick() 
        {
            if (star.Focused)
            {
                this.ChangeStarColor();
            }
            else
            {
                var psi = new ProcessStartInfo
                {
                    FileName = this.source.source.ToString(),
                    UseShellExecute = true,
                };
                Process.Start(psi);
            }
        }

        public void After_MouseClick() 
        {
            
        }

        public void ChangeStarColor() 
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (StarColor == Star.StarColors.WHITE)
                {
                    StarColor = Star.StarColors.YELLOW;
                }
                else if (StarColor == Star.StarColors.YELLOW)
                {
                    StarColor = Star.StarColors.WHITE;
                }
            });
        }
    }
}
