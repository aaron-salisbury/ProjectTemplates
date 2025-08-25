using System;
using System.Windows.Forms;

namespace WinXPApp.Base.Extensions;

public static class ControlExtensions
{
    public static bool SetStyle(this Control control, ControlStyles controlStyle, bool value)
    {
        bool styleSet = false;

        Type controlType = typeof(Control);
        System.Reflection.MethodInfo misSetStyle = controlType.GetMethod("SetStyle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        if (misSetStyle != null && control != null)
        {
            misSetStyle.Invoke(control, new object[] { controlStyle, value });
            styleSet = true;
        }

        return styleSet;
    }
}
