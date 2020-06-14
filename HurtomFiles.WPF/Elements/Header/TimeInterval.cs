using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace HurtomFiles.WPF
{
    public class TimeInterval : Label
    { 
        private DateTime start;

        public TimeInterval() 
        {
            this.start = DateTime.Now;
            this.FontSize = 15;
            this.Content = "";
        }

        public void Clear() 
        {
            start = DateTime.Now;
        }

        public void Write(string msg)
        {
            var finish = DateTime.Now;
            var span = finish - start;

            string result = $"{msg} - { span.Minutes}:{ span.Seconds}:{ span.Milliseconds}";
            Application.Current.Dispatcher.Invoke(() => this.Content = result);
        }

    }
}
