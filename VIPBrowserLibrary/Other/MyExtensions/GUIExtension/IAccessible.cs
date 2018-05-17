using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#pragma warning disable 1591
namespace VIPBrowserLibrary.Other.MyExtensions.GUIExtension
{
    public interface IAccessible
    {
        //AccessibleObject AccessibilityObject { get; }
        string AccessibleDefaultActionDescription { get; set; }
        string AccessibleDescription { get; set; }
        string AccessibleName { get; set; }
        AccessibleRole AccessibleRole { get; set; }
    }
}
#pragma warning restore
