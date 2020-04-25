﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace HurtomFiles.WPF
{
    public class AddMoreElements_Button : Button
    {
        public AddMoreElements_Button() 
        {
            Set();
        }

        public void Set() 
        {
            this.Width = 200;
            this.FontSize = 15;
            this.Content = "Дивитися більше";
            this.Margin = new Thickness(5);
            this.Padding = new Thickness(5);
        }
    }
}
