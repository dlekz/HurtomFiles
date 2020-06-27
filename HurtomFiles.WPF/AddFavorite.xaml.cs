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
    public partial class AddFavorite : Window
    {
        private static string bookmark = "";
        public static string Bookmark 
        {
            set => bookmark = value;
            get 
            {
                string value = bookmark;
                bookmark = "";
                return value;
            }
        }

        public AddFavorite()
        {
            InitializeComponent();
            AddBookmark.Click += AddBookmark_Click;
            BookmarkUri.Text = GetClipboardText();
        }

        public string GetClipboardText()
        {
            if (Clipboard.ContainsText() == true)
            {
                return Clipboard.GetText();
            }
            return "";
        }

        public void AddBookmark_Click(Object sender, RoutedEventArgs e) 
        {
            Bookmark = BookmarkUri.Text;
            this.Close();
        }
    }
}
