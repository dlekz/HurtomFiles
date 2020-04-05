using System;
using System.Collections.Generic;
using System.Text;

namespace HurtomFiles.Logic
{
    public readonly struct Title
    {
        // TODO: Parse title
        public readonly string fullTitle;
        public readonly string origTitle;
        public readonly string ukrTitle;
        public readonly int year;
        public readonly string information;

        public Title(string titleString)
        {
            this.fullTitle = titleString;
            origTitle = ukrTitle = information = "";
            year = 0;
        }

        public override string ToString() => fullTitle;
    }
}
