using System;

namespace Win98App.Base.Helpers
{
    // WinForms ComboBoxes don't play well with enums.
    // Can use this class to setup a middle-man.
    [Serializable]
    public class ComboBoxEnumItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
