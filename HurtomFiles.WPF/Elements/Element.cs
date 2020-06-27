using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HurtomFiles.WPF
{
    public class Element : Border
    {
        [Obsolete ("Use static resouces")]
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
