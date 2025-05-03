using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmChatGptWeb;

public partial class HmChatGptWeb
{
    private const byte VK_ESC = 0x1B; // ESCキーの仮想キーコード

    public void SendShiftEscSync()
    {
        SendShiftEsc();
    }

    private async void SendShiftEsc()
    {
        // Ctrl キーを解放
        keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, 0);
        // Alt キーを解放
        keybd_event(VK_ALT, 0, KEYEVENTF_KEYUP, 0);

        // Shift キーを押下
        keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYDOWN, 0);
        // ESC キーを押下
        keybd_event(VK_ESC, 0, KEYEVENTF_KEYDOWN, 0);

        await Task.Delay(100);

        // ESC キーを解放
        keybd_event(VK_ESC, 0, KEYEVENTF_KEYUP, 0);
        // Shift キーを解放
        keybd_event(VK_SHIFT, 0, KEYEVENTF_KEYUP, 0);
    }


}
