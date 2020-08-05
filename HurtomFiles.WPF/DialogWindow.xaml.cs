using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HurtomFiles.WPF
{
    /// <summary>
    /// Interaction logic for AddFavorive.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        private static string value = "";
        public static string Value 
        {
            set => DialogWindow.value = value;
            get 
            {
                string value = DialogWindow.value;
                DialogWindow.value = "";
                return value;
            }
        }

        public DialogWindow(string windowName = "NONAME WINDOW", string defaultValue = "") 
        {
            InitializeComponent();
            this.Title = windowName;
            Value = defaultValue;

            DoingButton.Click += Doing;
        }

        public string GetClipboardText()
        {
            if (Clipboard.ContainsText() == true)
            {
                return Clipboard.GetText();
            }
            return "";
        }

        public void Doing(Object sender, RoutedEventArgs e) 
        {
            Value = ValueText.Text;
            this.Close();
        }
    }
}
