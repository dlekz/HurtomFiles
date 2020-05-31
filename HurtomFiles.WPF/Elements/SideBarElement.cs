using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HurtomFiles.WPF
{
    public class SideBarElement : Element
    {
        public SideBarElement() : base() => Set();

        public Button ShowElements_Button { set; get; }
        public Button ShowFavorites_Button { set; get; }

        private void Set()
        {
            StackPanel stack = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalAlignment = HorizontalAlignment.Stretch,
            };

            ShowElements_Button = new Button();
            ShowElements_Button.Content = "Всі фільми";
            ShowFavorites_Button = new Button();
            ShowFavorites_Button.Content = "Улюблені";

            stack.Children.Add(ShowElements_Button);
            stack.Children.Add(ShowFavorites_Button);

            this.Child = stack;
        }
    }
}
