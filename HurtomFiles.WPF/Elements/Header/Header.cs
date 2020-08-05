using System;
using System.Windows.Controls;

namespace HurtomFiles.WPF
{
    public class Header : Border
    {
        private readonly TimeInterval timer = new TimeInterval();
        
        public Header()
        {
            this.Style = App.Styles.HeaderStyle;

            var panel = App.Panels.HorizontalStack;

            var img = App.Images.HurtomTitle;

            panel.Children.Add(img);
            panel.Children.Add(timer);
            this.Child = panel;
        }

        public void WriteTimer() => timer.Write("Час старту");
    }
}
