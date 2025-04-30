using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace HmChatGptWeb;

[ComVisible(true)]
[ClassInterface(ClassInterfaceType.AutoDual)]
[Guid("EEF21C40-AE19-4107-8FBF-399CCD7A5E3B")]
public partial class HmChatGptWeb
{
    // キーボード入力をシミュレートするためのAPI
    [DllImport("user32.dll")]
    private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

    // KeyDownのフラグ
    private const uint KEYEVENTF_KEYDOWN = 0x0000;
    // KeyUpのフラグ
    private const uint KEYEVENTF_KEYUP = 0x0002;

    // Virtual-Key Codes
    private const byte VK_CONTROL = 0x11;
    private const byte VK_SHIFT = 0x10;
    private const byte VK_ALT = 0x12;
    private const byte VK_C = 0x43;
    private const byte VK_V = 0x56;
    private const byte VK_RETURN = 0x0D;
    private const byte VK_TAB = 0x09; // タブキーの仮想キーコード


    public void CaptureForBrowserPane(String text)
    {
        try
        {
            CaptureClipboard();
        }
        catch (Exception ex)
        {
            Task.Delay(300).Wait();
            // クリップボードにテキストを保存
            CaptureClipboard();
        }
        try
        {
            Clipboard.SetText(text);
        }
        catch (Exception ex)
        {
            Task.Delay(300).Wait();
            // クリップボードにテキストを保存
            Clipboard.SetText(text);
        }

    }

    public void SendCtrlVSync()
    {
        SendCtrlV();
    }

    public void SendReturnVSync()
    {
        SendReturn();
    }


    private static async void SendShiftTab()
    {
        // Tab キーを押下
        keybd_event(VK_TAB, 0, KEYEVENTF_KEYDOWN, 0);
        await Task.Delay(100);
        // Tab キーを解放
        keybd_event(VK_TAB, 0, KEYEVENTF_KEYUP, 0);
    }

    private static async void SendCtrlV()
    {
        // Shift キーを解放
        keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
        // Alt キーを解放
        keybd_event(VK_ALT, 0, KEYEVENTF_KEYUP, 0);

        // Ctrl キーを押下
        keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYDOWN, 0);
        // V キーを押下
        keybd_event(VK_V, 0, KEYEVENTF_KEYDOWN, 0);

        await Task.Delay(100);

        // V キーを解放
        keybd_event(VK_V, 0, KEYEVENTF_KEYUP, 0);
        // Ctrl キーを解放
        keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
    }

    private static async void SendReturn()
    {
        // Ctrl キーを解放
        keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
        // Shift キーを解放
        keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
        // Alt キーを解放
        keybd_event(VK_ALT, 0, KEYEVENTF_KEYUP, 0);

        // Enter キーを押下
        keybd_event(VK_RETURN, 0, KEYEVENTF_KEYDOWN, 0);

        await Task.Delay(100);

        // Enter キーを解放
        keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0);
    }
}
