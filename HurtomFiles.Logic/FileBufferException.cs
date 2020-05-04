using System;
using System.Collections.Generic;
using System.Text;

namespace HurtomFiles.Logic
{
    public class FileBufferException : Exception
    {
        public FileBufferException(string msg) : base(msg) { }
    }
}
