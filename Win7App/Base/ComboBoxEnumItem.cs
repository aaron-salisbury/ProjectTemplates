using System;

namespace Win7App.Base
{
    // WPF ComboBoxes don't play well with enums.
    // Can use this class to setup a middle-man within the view model.
    public class ComboBoxEnumItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
