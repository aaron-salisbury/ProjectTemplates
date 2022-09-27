using System;

namespace Win98App.Base.Helpers
{
    // WinForms ComboBoxes don't play well with enums.
    // Can use this class to setup a middle-man.
    [Serializable]
    public class ComboBoxEnumItem
    {
        private int _value;
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
    }
}
