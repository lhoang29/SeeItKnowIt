using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SeeItKnowIt
{
    public static class Keyboard
    {
        const int INPUT_TYPE_KEYBOARD = 1;
        const uint KEYEVENTF_KEYUP = 0x0002;

        const ushort VK_SHIFT = 0x10;
        const ushort VK_CONTROL = 0x11;
        const ushort VK_MENU = 0x12;

        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        struct KEYBDINPUT
        {
            public ushort wVirtualKey;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        struct HARDWAREINPUT
        {
            public int uMsg;
            public short wParamL;
            public short wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT MouseInput;

            [FieldOffset(0)]
            public KEYBDINPUT KeyboardInput;

            [FieldOffset(0)]
            public HARDWAREINPUT HardwareInput;
        }

        struct INPUT
        {
            public int type;
            public MOUSEKEYBDHARDWAREINPUT AllInput;
        }

        [DllImport("user32.dll")]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(ushort vKey);

        public static void SimulateCopyToClipboard()
        {
            if (Keyboard.IsKeyDown(VK_MENU))
            {
                Keyboard.SimulateKeyUp(VK_MENU);
            }
            if (Keyboard.IsKeyDown(VK_SHIFT))
            {
                Keyboard.SimulateKeyUp(VK_SHIFT);
            }

            if (!Keyboard.IsKeyDown(VK_CONTROL))
            {
                Keyboard.SimulateKeyDown(VK_CONTROL);
            }
            if (!Keyboard.IsKeyDown('C'))
            {
                Keyboard.SimulateKeyDown('C');
            }
            Keyboard.SimulateKeyUp('C');
            Keyboard.SimulateKeyUp(VK_CONTROL);
        }

        private static bool IsKeyDown(ushort key)
        {
            return (GetAsyncKeyState(key) & 0x10000) != 0;
        }

        private static void SimulateKeyUp(ushort key)
        {
            INPUT input = new INPUT();
            input.type = INPUT_TYPE_KEYBOARD;
            int size = Marshal.SizeOf(input);
            input.AllInput.KeyboardInput.wVirtualKey = key;
            input.AllInput.KeyboardInput.dwFlags = KEYEVENTF_KEYUP;
            SendInput(1, ref input, size);
        }

        private static void SimulateKeyDown(ushort key)
        {
            INPUT input = new INPUT();
            input.type = INPUT_TYPE_KEYBOARD;
            int size = Marshal.SizeOf(input);
            input.AllInput.KeyboardInput.wVirtualKey = key;
            SendInput(1, ref input, size);
        }
    }
}