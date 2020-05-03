using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HurtomFiles.Logic
{
    public class FileInformationBuffer
    {
        private List<FileInformation> buffer = new List<FileInformation>();

        public bool IsBuffering { set; get; } = false;

        public string Page { set; get; }

        public int Position { set; get; } = 0;

        public int Count => buffer.Count;

        public FileInformation this[int i] => buffer[i];

        public FileInformationBuffer() { }


        public void Buffering() 
        {
            IsBuffering = false;
            buffer = new List<FileInformation>();

            if (this.Page == "") return;

            var links = Task.Run(() => new FileInformationLinkPage(Page)).Result;
            this.Page = links.NextPage;

            for (int i = 0; i < links.Count; i++)
            {
                var fileInformation = Task.Run(() => new FileInformation(links[i])).Result;

                buffer.Add(fileInformation);
            }

            IsBuffering = true;
        }

        // TODO: it is not working
        public void _Buffering()
        {
            IsBuffering = false;
            buffer = new List<FileInformation>();

            if (this.Page == "") return;

            var links = Task.Run(() => new FileInformationLinkPage(Page)).Result;

            if (Position == 0) this.Page = links.NextPage;

            for (int i = Position; i < links.Count; i++)
            {
                if (IsBuffering) 
                {
                    Position = i;
                    return;
                }

                var fileInformation = Task.Run(() => new FileInformation(links[i])).Result;

                buffer.Add(fileInformation);
            }

            IsBuffering = true;
        }
    }
}
