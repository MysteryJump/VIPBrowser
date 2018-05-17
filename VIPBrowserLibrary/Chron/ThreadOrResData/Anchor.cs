using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VIPBrowserLibrary.Chron.ThreadOrResData
{
    /// <summary>
    /// アンカーを一つを表すクラスです
    /// </summary>
    public class Anchor
    {
        private List<bool> data;
        /// <summary>
        /// アンカーの数字の部分よりこのクラスの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="ancStr"></param>
        public Anchor(string ancStr)
        {
            data = new List<bool>(10000);
            data.AddRange(new bool[10000]);
            this.baseStr = ancStr;
            this.Parse();
        }
        /// <summary>
        /// アンカーの取得を行います
        /// </summary>
        public int[] AnchorNumber
        {
            get
            {
                List<int> anchors = new List<int>();
                for (int i = 1; i < data.Count; i++)
                {
                    if (data[i] == true)
                    {
                        anchors.Add(i);
                    }
                }
                return anchors.ToArray();
            }
        }
        private void Parse()
        {
            string[] splStr = this.baseStr.Split(',');
            foreach (var item in splStr)
            {
                item.Trim();
                if (item.Contains("-"))
                {
                    int firstPlace = item.IndexOf("-");
                    string firstWord = item.Substring(0, firstPlace);
                    string secondWord = item.Substring(firstPlace + 1);
                    int first = int.Parse(firstWord);
                    int second = int.Parse(secondWord);
                    if (first > second)
                    {
                        int third = second;
                        second = first;
                        first = second;
                    }
                    for (int i = first; i <= second; i++)
                    {
                        data[i] = true;
                    }
                }
                else
                {
                    data[int.Parse(item)] = true;
                }
            }
        }
        private string baseStr;
        /// <summary>
        /// アンカーに特定のレス番を追加します
        /// </summary>
        /// <param name="num">追加するレス番号</param>
        public void AddNumber(int num)
        {
            this.baseStr += "," + num.ToString();
            data[num] = true;
        }
        /// <summary>
        /// このインスタンスが格納しているアンカーを取得します
        /// </summary>
        /// <returns>アンカーの<see cref="T:System.String"/>値</returns>
        public override string ToString()
        {
            return this.baseStr;
        }
    }
}
