using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HurtomFiles.WPF
{
    public class SideBar : Element
    {
        public SideBar() : base() => Set();

        public Button ShowElements_Button { set; get; }
        public Button ShowFavorites_Button { set; get; }

        private void Set()
        {
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
                FontSize = 15,
                Content = content,
                Padding = new Thickness(5), 
            };
        }
    }
}
