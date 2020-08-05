using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HurtomFiles.WPF
{
    public class SideBar : Border
    {
        public Button ShowElements_Button { set; get; }
        public Button ShowFavorites_Button { set; get; }

        public SideBar() 
        {
            this.Style = App.Styles.SideBarStyle;

            StackPanel stack = App.Panels.ButtonStack;

            ShowElements_Button = App.Elements.NormalButton;
            ShowElements_Button.Content = "Всі фільми";

            ShowFavorites_Button = App.Elements.NormalButton;
            ShowFavorites_Button.Content = "Обране";

            stack.Children.Add(ShowElements_Button);
            stack.Children.Add(ShowFavorites_Button);

            this.Child = stack;
        }

    }
}
