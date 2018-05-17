using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace VIPBrowserLibrary.Other.MyExtensions.GUIExtension
{
    /// <summary>
    /// Controlが格納しているメンバを表します
    /// </summary>
    public interface IControl : IDropTarget, ISynchronizeInvoke, IWin32Window, IBindableComponent, IComponent, IDisposable,IAccessible
    {

    }
}
