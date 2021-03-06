﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Windows;

namespace HurtomFiles.AvaloniaApp
{
    public class Element : Border
    {
        public Element(SolidColorBrush background = null, SolidColorBrush borderBlush = null, 
            Thickness thickness = new Thickness(), 
            Thickness padding = new Thickness(), 
            Thickness margin = new Thickness())
        {
            Thickness defThickness = new Thickness();

            this.Background = background ?? Brushes.WhiteSmoke;
            this.BorderBrush = borderBlush ?? Brushes.Black;
            this.BorderThickness = (thickness != defThickness) ? thickness : new Thickness(2);
            this.Padding = (padding != defThickness) ? padding : new Thickness(5);
            this.Margin = (margin != defThickness) ? margin : new Thickness(5);
        }
    }
}
