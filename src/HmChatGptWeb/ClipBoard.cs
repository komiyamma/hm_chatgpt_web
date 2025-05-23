﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data;
using System.Drawing;
using System.IO;

namespace HmChatGptWeb;

public partial class HmChatGptWeb
{
    public void SendToClipboard(string text)
    {
        // クリップボードにテキストを保存
        Clipboard.SetText(text);
    }

    private Dictionary<string, object> storedData = new Dictionary<string, object>();

    // クリップボードの内容を記憶
    public void CaptureClipboard()
    {
        storedData.Clear();

        IDataObject dataObject = Clipboard.GetDataObject();
        if (dataObject == null) return;

        foreach (string format in dataObject.GetFormats())
        {
            try
            {
                object data = dataObject.GetData(format);

                // Stream（画像やファイル）などはメモリにコピー
                if (data is MemoryStream stream)
                {
                    MemoryStream copy = new MemoryStream();
                    stream.Position = 0;
                    stream.CopyTo(copy);
                    copy.Position = 0;
                    storedData[format] = copy;
                }
                else if (data is string text)
                {
                    storedData[format] = string.Copy(text);
                }
                else if (data is string[] array)
                {
                    storedData[format] = (string[])array.Clone();
                }
                else
                {
                    storedData[format] = data; // 可能な限りコピー
                }
            }
            catch
            {
                // 一部の形式は例外が出るためスキップ
            }
        }
    }

    // 保存しておいたデータをクリップボードに戻す
    public void RestoreClipboard()
    {
        DataObject newData = new DataObject();

        foreach (var kvp in storedData)
        {
            newData.SetData(kvp.Key, kvp.Value);
        }

        Clipboard.SetDataObject(newData, true);
    }
}
