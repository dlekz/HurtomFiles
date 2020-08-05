using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HurtomFiles.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Application ThisApp;

        public App() 
        {
            ThisApp = this;
        }

        public static class Styles 
        {
            public static Style ButtonStyle => 
                ThisApp.FindResource("BtnStyle") as Style;

            public static Style ElementStyle =>
                ThisApp.FindResource("ElementStyle") as Style;

            public static Style HorizontalStackStyle =>
                ThisApp.FindResource("HorizontalStackStyle") as Style;

            public static Style ButtonStackStyle =>
                ThisApp.FindResource("ButtonStackStyle") as Style;

            public static Style HeaderStyle => 
                ThisApp.FindResource("HeaderStyle") as Style;

            public static Style SideBarStyle => 
                ThisApp.FindResource("SideBarStyle") as Style;
            
            public static Style BodyStyle =>
                ThisApp.FindResource("BodyStyle") as Style;

        }

        public static class Images 
        {
            public static Image HurtomTitle => new Image()
            {
                Stretch = Stretch.Fill,
                Height = 110,
                Width = 521,
                Source = ThisApp.FindResource("HurtomTitleBitmap") as BitmapImage,
            };
        }

        public static class Panels 
        {
            public static StackPanel HorizontalStack => new StackPanel()
            {
                Style = App.Styles.HorizontalStackStyle,
            };

            public static StackPanel ButtonStack => new StackPanel()
            {
                Style = App.Styles.ButtonStackStyle,
            };
        }

        public static class Elements 
        {
            public static Button NormalButton => new Button()
            {
                Style = App.Styles.ButtonStyle,
            };
        }

        public static string GetClipboardText()
        {
            if (Clipboard.ContainsText() == true)
            {
                return Clipboard.GetText();
            }
            return "";
        }
    }
}
