using System;
using System.Collections.Generic;
using System.Text;

namespace HurtomFiles.Logic
{
    public struct Link
    {
        private const string domainName = "https://toloka.to/";
        private readonly string value;

        public Link(string link) 
        {
            if (link.Contains(domainName))
                value = link;
            else 
                value = domainName + link;
        }

        public override string ToString() => value;
        public override bool Equals(object obj)
        {
            if (obj is string str)
                return this.value == str;
            if (obj is Link link2)
                return this.value == link2.value;

            throw new Exception("obj must be Link or string");
        }
        public override int GetHashCode()
        {
            return -1584136870 + EqualityComparer<string>.Default.GetHashCode(value);
        }

        public static bool operator ==(Link link, object obj) 
        {
            if (obj is string str) 
                return link.value == str;
            if (obj is Link link2) 
                return link.value == link2.value;

            throw new Exception("obj must be Link or string");
        }
        public static bool operator !=(Link link, object obj)
        {
            if (obj is string str)
                return link.value != str;
            if (obj is Link link2)
                return link.value != link2.value;

            throw new Exception("obj must be Link or string");
        }
        

    }
}
