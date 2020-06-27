using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HurtomFiles.WPF
{
    public class SideBar : Border //: Element
    {
        public SideBar() //: base() 
            => Set();

        public Button ShowElements_Button { set; get; }
        public Button ShowFavorites_Button { set; get; }

        private void Set()
        {
            //BrushConverter bc = new BrushConverter();
            //Brush brush = (Brush)bc.ConvertFrom("#6689a2");
            //brush.Freeze();
            //this.Background = brush;
            

            this.Style = App.ThisApp.FindResource("SideBarStyle") as Style;
            //this.Padding = new Thickness(5);

            StackPanel stack = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };

            ShowElements_Button = SetButton("Всі фільми");
            ShowFavorites_Button = SetButton("Обране");

            stack.Children.Add(ShowElements_Button);
            stack.Children.Add(ShowFavorites_Button);

            this.Child = stack;
        }

        private Button SetButton(string content)
        { 
            return new Button() 
            {
                Style = App.ThisApp.FindResource("BtnStyle") as Style,
                Content = content,
            };
        }
    }
}
