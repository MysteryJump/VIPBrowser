using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIPBrowser
{
    /// <summary>
    /// プラグインに関するクラスです
    /// </summary>
    public class PluginInfo
    {
        private string _location;
        private string _className;
        
        /// <summary>
        /// PluginInfoクラスのコンストラクタ
        /// </summary>
        /// <param name="path">アセンブリファイルのパス</param>
        /// <param name="cls">クラスの名前</param>
        private PluginInfo(string path, string cls)
        {
            this._location = path;
            this._className = cls;
        }

        /// <summary>
        /// アセンブリファイルのパス
        /// </summary>
        public string Location
        {
            get { return _location; }
        }

        /// <summary>
        /// クラスの名前
        /// </summary>
        public string ClassName
        {
            get { return _className; }
        }

        /// <summary>
        /// 有効なプラグインを探す
        /// </summary>
        /// <returns>有効なプラグインのPluginInfo配列</returns>
        public static PluginInfo[] FindPlugins(string pluginDir)
        {
            System.Collections.ArrayList plugins =
                new System.Collections.ArrayList();
            //IPlugin型の名前
            string ipluginName = typeof(VIPBrowserPlugin.IPlugin).FullName;

            if (!System.IO.Directory.Exists(pluginDir))
                throw new ApplicationException(
                    "プラグインフォルダ\"" + pluginDir +
                    "\"が見つかりませんでした。");

            //.dllファイルを探す
            string[] dlls =
                System.IO.Directory.GetFiles(pluginDir, "*.dll");

            foreach (string dll in dlls)
            {
                try
                {
                    //アセンブリとして読み込む
                    System.Reflection.Assembly asm =
                        System.Reflection.Assembly.LoadFrom(dll);
                    foreach (Type t in asm.GetTypes())
                    {
                        //アセンブリ内のすべての型について、
                        //プラグインとして有効か調べる
                        if (t.IsClass && t.IsPublic && !t.IsAbstract
                            && t.GetInterface(ipluginName) != null)
                        {
                            //PluginInfoをコレクションに追加する
                            plugins.Add(
                                new PluginInfo(dll, t.FullName));
                        }
                    }
                }
                catch
                {
                }
            }

            //コレクションを配列にして返す
            return (PluginInfo[])
                plugins.ToArray(typeof(PluginInfo));
        }
              /// <summary>
        /// 有効なプラグインを探す
        /// </summary>
        /// <returns>有効なプラグインのPluginInfo配列</returns>
        public static PluginInfo[] Find2Plugins(string pluginDir)
        {
            System.Collections.ArrayList plugins =
                new System.Collections.ArrayList();
            //IPlugin型の名前
            string ipluginName = typeof(VIPBrowserPlugin.IPlugin2).FullName;

            if (!System.IO.Directory.Exists(pluginDir))
                throw new ApplicationException(
                    "プラグインフォルダ\"" + pluginDir +
                    "\"が見つかりませんでした。");

            //.dllファイルを探す
            string[] dlls =
                System.IO.Directory.GetFiles(pluginDir, "*.dll");

            foreach (string dll in dlls)
            {
                try
                {
                    //アセンブリとして読み込む
                    System.Reflection.Assembly asm =
                        System.Reflection.Assembly.LoadFrom(dll);
                    foreach (Type t in asm.GetTypes())
                    {
                        //アセンブリ内のすべての型について、
                        //プラグインとして有効か調べる
                        if (t.IsClass && t.IsPublic && !t.IsAbstract
                            && t.GetInterface(ipluginName) != null)
                        {
                            //PluginInfoをコレクションに追加する
                            plugins.Add(
                                new PluginInfo(dll, t.FullName));
                        }
                    }
                }
                catch
                {
                }
            }
            //コレクションを配列にして返す
            return (PluginInfo[])
                plugins.ToArray(typeof(PluginInfo));
        }

        /// <summary>
        /// プラグインクラスのインスタンスを作成する
        /// </summary>
        /// <returns>プラグインクラスのインスタンス</returns>
        public VIPBrowserPlugin.IPlugin CreateInstance(VIPBrowserPlugin.IPluginHost host)
        {
            try
            {
                //アセンブリを読み込む
                System.Reflection.Assembly asm = System.Reflection
                    .Assembly.LoadFrom(this.Location);
                //クラス名からインスタンスを作成する
                VIPBrowserPlugin.IPlugin plugin = (VIPBrowserPlugin.IPlugin)
                    asm.CreateInstance(this.ClassName);
                //初期化
                plugin.Initialize(host);
                return plugin;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// プラグインクラスのインスタンスを作成する
        /// </summary>
        /// <returns>プラグインクラスのインスタンス</returns>
        public VIPBrowserPlugin.IPlugin2 CreateInstance(VIPBrowserPlugin.IPluginHostCh2Browser host)
        {
            try
            {
                //アセンブリを読み込む
                System.Reflection.Assembly asm = System.Reflection
                    .Assembly.LoadFrom(this.Location);
                //クラス名からインスタンスを作成する
                VIPBrowserPlugin.IPlugin2 plugin = (VIPBrowserPlugin.IPlugin2)
                    asm.CreateInstance(this.ClassName);
                //初期化
                plugin.Initialize(host);
                return plugin;
            }
            catch
            {
                return null;
            }
        }
    }
}
