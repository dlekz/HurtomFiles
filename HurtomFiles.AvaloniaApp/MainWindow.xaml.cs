using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Input;
//using HurtomFiles.WPF.Properties;
//using HtmlAgilityPack;
using HurtomFiles.Logic;
using MessageBox.Avalonia;

namespace HurtomFiles.AvaloniaApp
{
    public class MainWindow : Window
    {
        private readonly FileElementPanel elements;
        private readonly AddMoreElements_Button addMoreElement = new AddMoreElements_Button();
        private readonly LoadingElement loadingElement = new LoadingElement();
        private readonly SideBarElement sideBarElement = new SideBarElement();
        private readonly HeaderElement headerElement = new HeaderElement();

        // TODO: it's work, but slow
        // TODO: https://toloka.to/t51625 info not found
        // TODO: https://toloka.to/t109534 image not load
        public MainWindow()
        {
            InitializeComponent();
            //this.MouseMove += CursorChange;
            this.addMoreElement.Click += AddMoreElements_Button_Click;

            elements = new FileElementPanel("https://toloka.to/f16");

            var elementsPanel = new StackPanel();

            elementsPanel.Children.Add(elements);
            elementsPanel.Children.Add(addMoreElement);
           // elementsPanel.Children.Add(loadingElement);
           // elementsPanel.Children.Add(loadingElement.LoadingElement_Rotate());

            var scroll = new ScrollViewer() { Content = elementsPanel };

            //this.HeaderGrid.Children.Add(headerElement);
            //this.SideBar.Children.Add(sideBarElement);
            //this.MainGrid.Children.Add(scroll);
            
            this.FindControl<Grid>("HeaderGrid").Children.Add(headerElement);
            this.FindControl<Grid>("MainGrid").Children.Add(scroll);

            MessageBoxManager
                .GetMessageBoxStandardWindow("Count",elements.Elements.Count.ToString()).Show();
                //msgBox.Show();

            Task.Run(() => elements.Buffering(9));
        }

        private void AddMoreElements_Button_Click(object sender, EventArgs e) 
        {
            Task.Run(() => elements.AddPage());
        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}