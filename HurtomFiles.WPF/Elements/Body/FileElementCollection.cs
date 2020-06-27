using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using HurtomFiles.Logic;
using HurtomFiles.WPF.Elements;

namespace HurtomFiles.WPF
{
    public class FileElementCollection
    {
        public List<FileElement> Value { set; get; } = new List<FileElement>();

        public FileElementCollection() 
        {
        }

        public void Add(FileElement element) =>
            this.Value.Add(element);


    }
}
