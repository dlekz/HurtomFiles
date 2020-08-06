using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HtmlAgilityPack;
using HurtomFiles.Logic;

namespace HurtomFiles.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Body body;
        private readonly Button addMoreElements;
        //private readonly LoadingElement loadingElement = new LoadingElement();
        private readonly SideBar sideBarElement = new SideBar();
        private readonly Header headerElement = new Header();

        // TODO: it's work, but slow
        // TODO: https://toloka.to/t51625 info not found
        // TODO: https://toloka.to/t109534 image not load
        public MainWindow()
        {

            InitializeComponent();
            this.MouseMove += CursorChange;

            this.addMoreElements = App.Elements.NormalButton;
            this.addMoreElements.Content = "Дивитися більше";
            this.addMoreElements.Margin = new Thickness(5, 50, 5, 50);
            this.addMoreElements.Click += AddMoreElements_Button_Click;

            this.sideBarElement.ShowElements_Button.Click += ShowElements_Button_Click;
            this.sideBarElement.ShowFavorites_Button.Click += ShowFavorites_Button_Click;

            this.KeyDown += Key_Down;
            this.Closing += Before_Close;

            body = new Body("https://toloka.to/f16");

            var elementsPanel = new StackPanel();

            elementsPanel.Children.Add(body);
            elementsPanel.Children.Add(addMoreElements);

            var scroll = new ScrollViewer() { Content = elementsPanel };

            this.HeaderGrid.Children.Add(headerElement);
            this.SideBar.Children.Add(sideBarElement);
            this.MainGrid.Children.Add(scroll);

            Task.Run(() => body.Buffering(9));
            headerElement.WriteTimer();
        }

        private void CursorChange(object sender, EventArgs e) 
        {
            if (body.Focused)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Arrow;
        }

        private void AddMoreElements_Button_Click(object sender, EventArgs e) 
        {
            Task.Run(() => body.AddPage());
        }

        private void ShowElements_Button_Click(object sender, EventArgs e) 
        {
            body.Show(FileElementTypes.MAIN);
            addMoreElements.Visibility = Visibility.Visible;
        }

        private void ShowFavorites_Button_Click(object sender, EventArgs e)
        {
            body.Show(FileElementTypes.FAVORITES);
            addMoreElements.Visibility = Visibility.Hidden;
        }

        private void Before_Close(object sender, EventArgs e)
        {
            body.Favorites.Set();
        }

        private void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.Key.Equals(Key.V) 
                && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control
                && body.ActiveElements == FileElementTypes.FAVORITES)
            {
                var addFavorite = new DialogWindow("Додати посилання", App.GetClipboardText());
                addFavorite.ShowDialog();
                body.Favorites.Add(DialogWindow.Value);
                body.Show(FileElementTypes.FAVORITES);
            }
            // TODO: label при не знаходженні результатів - інтерактивна; при нажиманні вибиває виключення
            if (e.Key.Equals(Key.F)
                && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                new DialogWindow("Пошук").ShowDialog();
                var elements = new SearchCommand(DialogWindow.Value).Result;

                if (elements.Count() == 0)
                    return;

                this.addMoreElements.Visibility = Visibility.Hidden;

                Application.Current.Dispatcher.Invoke(() => body.Children.Clear());
                
                foreach (var el in elements)
                    Application.Current.Dispatcher.Invoke(() => body.Children.Add(el));
            }
        }
    }
}
