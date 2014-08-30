using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace EVILGEDDON_Trainer
{
    public partial class Form1 : Form
    {
        public static class Constants
        {
            public const int WM_HOTKEY_MSG_ID = 0x0312;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KeyHandler ghk;
            ghk = new KeyHandler(Keys.F1, this);
            ghk.Register();
        }

        public class KeyHandler
        {
            [DllImport("user32.dll")]
            private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

            [DllImport("user32.dll")]
            private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

            private int key;
            private IntPtr hWnd;
            private int id;

            public KeyHandler(Keys key, Form form)
            {
                this.key = (int)key;
                this.hWnd = form.Handle;
                id = this.GetHashCode();
            }

            public override int GetHashCode()
            {
                return key ^ hWnd.ToInt32();
            }

            public bool Register()
            {
                return RegisterHotKey(hWnd, id, 0, key);
            }

            public bool Unregiser()
            {
                return UnregisterHotKey(hWnd, id);
            }
        }

        private void HandleHotkey()
        {
            SendKeys.Send("gangnamstyle");
            SendKeys.Send("shitjustgotreal");
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                HandleHotkey();
            base.WndProc(ref m);
        }
    }
}
