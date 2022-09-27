using System;

namespace Win98Core.SampleTools
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

        public FlatColor(string name, string hex)
        {
            Name = name;
            Hex = hex;
        }
    }
}
