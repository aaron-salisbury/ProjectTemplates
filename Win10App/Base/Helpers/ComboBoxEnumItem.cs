using System;

namespace Win10App.Base.Helpers
{
    // UWP ComboBoxes don't play well with enums.
    // Can use this class to setup a middle-man within the view model.
    public class ComboBoxEnumItem
    {
        public int Value { get; set; }
        public string Text { get; set; }
    }
}
