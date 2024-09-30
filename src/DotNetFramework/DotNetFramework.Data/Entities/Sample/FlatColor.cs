using System;

namespace DotNetFramework.Data.Entities
{
    public class FlatColor
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _hex;
        public string Hex
        {
            get { return _hex; }
            set { _hex = value; }
        }
    }
}
