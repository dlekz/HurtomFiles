using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Windows.Media;
using HurtomFiles.Logic;
using System.Threading.Tasks;

namespace HurtomFiles.WPF
{
    public class FileElementPanel : WrapPanel
    {
        private FileBuffer Buffer { set; get; } = new FileBuffer();

        public FileElementPanel()
        {
            this.Set();
        }

        public FileElementPanel(string uri)
        {
            this.Set();

            var infoColl = Task.Run(() => new FileLinkPage(uri)).Result;

            foreach (var info in infoColl.GetFileCollection())
                this.Add(info);

            Buffer.Page = new Link(infoColl.NextPage.ToString());
            Buffer.Buffering();
        }

        public void Add(FilePage info)
        {
            Application.Current.Dispatcher.Invoke(() => this.Children.Add(new FileInformationElement(info)));
        }

        public void AddRange(FilePage[] files) 
        {
            foreach (var file in files)
                this.Add(file);
        }

        private void Set() 
        {
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.Background = Brushes.Orange;
            this.Margin = new Thickness(0);
        }

        public void AddPage()
        {
            try
            {
                Buffer.Check();
                this.AddRange(Buffer.Get);
                Buffer.Buffering();
            }
            catch (FileBufferException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Source);
            }
        }
    }
}
