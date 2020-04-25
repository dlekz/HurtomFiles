﻿using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using HurtomFiles.Logic;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HurtomFiles.WPF
{
    public class FileInformationElement : Element
    {

        private FileInformation Value { set; get;}
        private TextBlock text;

        public static bool Focused { set; get; } = false;

        private FileInformationElement() : base(Brushes.WhiteSmoke, Brushes.Black,
            thickness: new Thickness(3), margin: new Thickness(5, 5, 0, 5)){ }

        public FileInformationElement(string uri) : this()
        {
            Value = Task.Run(() => new FileInformation(uri)).Result;

            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;
            this.MouseDown += ClickLink;

            Set(Value);
        }

        public FileInformationElement(FileInformation info) : this()
        {
            Value = info;
            this.MouseEnter += SetFocus;
            this.MouseLeave += LostFocus;
            this.MouseDown += ClickLink;
            Set(info); 
        }

        private void Set(FileInformation info) 
        {
            this.Width = 200;
            this.Height = 300;

            StackPanel stack = new StackPanel();

            text = new TextBlock()
            {
                Text = info.title.ToString(),
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily("Verdana Bold"),
                Foreground = Brushes.Black
            };

            stack.Children.Add(SetImage(new Uri(info.imageUri), this.Height - 60));
            stack.Children.Add(text);
            this.Child = stack;
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
            this.BorderBrush = Brushes.Green;
            this.text.Foreground = Brushes.Green;
            Focused = true;
        }

        private void LostFocus(object sender, EventArgs e) 
        {
            this.BorderBrush = Brushes.Black;
            this.text.Foreground = Brushes.Black;
            Focused = false;
        }

        private void ClickLink(object sender, EventArgs e) 
        {
            var psi = new ProcessStartInfo
            {
                FileName = Value.source,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
