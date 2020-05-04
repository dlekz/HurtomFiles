using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HurtomFiles.Logic
{
    public class FileBuffer
    {
        private List<FilePage> buffer = new List<FilePage>();

        public bool IsBuffering { set; get; } = false;

        public Link Page { set; get; }

        public int Position { set; get; } = 0;

        public int Count => buffer.Count;

        public FilePage this[int i] => buffer[i];

        public FileBuffer() { }


        public void Buffering() 
        {
            IsBuffering = false;
            buffer = new List<FilePage>();

            if (this.Page.ToString() == "") return;

            var links = Task.Run(() => new FileLinkPage(Page.ToString())).Result;
            this.Page = new Link(links.NextPage.ToString());

            for (int i = 0; i < links.Count; i++)
            {
                var fileInformation = Task.Run(() => new FilePage(links[i])).Result;

                buffer.Add(fileInformation);
            }

            IsBuffering = true;
        }

        public void Check() 
        {
            if (this.Page == "")
                throw new FileBufferException("Page is not available");
            if (!this.IsBuffering || this.Count == 0)
                throw new FileBufferException("Buffer is not available");

        }

        public FilePage[] Get => buffer.ToArray();

        // TODO: it is not working
        public void _Buffering()
        {
            IsBuffering = false;
            buffer = new List<FilePage>();

            if (this.Page.ToString() == "") return;

            var links = Task.Run(() => new FileLinkPage(Page.ToString())).Result;

            if (Position == 0) 
                this.Page = new Link(links.NextPage.ToString());

            for (int i = Position; i < links.Count; i++)
            {
                if (IsBuffering) 
                {
                    Position = i;
                    return;
                }

                var fileInformation = Task.Run(() => new FilePage(links[i])).Result;

                buffer.Add(fileInformation);
            }

            IsBuffering = true;
        }
    }
}
