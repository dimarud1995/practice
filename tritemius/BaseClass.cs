using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tritemius
{
    abstract class BaseClass
    {
        protected String _key;
        protected string _text;
        public abstract string Text
        {
            get;
            set;
        }
        public abstract String Key
        {
            get;
            set;
        }
        public abstract string encrypt();
        public abstract string decrypt();
    }
}
