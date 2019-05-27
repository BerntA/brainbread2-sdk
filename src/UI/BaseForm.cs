//=========       Copyright © Reperio Studios 2013-2019 @ Bernt Andreas Eide!       ============//
//
// Purpose: Base Form with custom border.
//
//=============================================================================================//

using BB2SDK.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BB2SDK.UI
{
    public partial class BaseForm : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        protected ImageButton exitButton;
        protected Timer timFadeIn;
        protected Timer timFadeOut;
        protected Panel baseArea;

        public BaseForm()
        {
            InitializeComponent();

            Panel border = new Panel();
            border.Parent = this;
            border.Bounds = new Rectangle(0, 0, Width, 30);
            border.BackColor = Color.FromArgb(24, 22, 23);
            border.BorderStyle = BorderStyle.None;
            border.MouseDown += new MouseEventHandler(OnMouseDown);

            Label lbl1 = new Label();
            lbl1.Parent = border;
            lbl1.Dock = DockStyle.Fill;
            lbl1.Text = "BrainBread 2 Mod Tools";
            lbl1.TextAlign = ContentAlignment.MiddleLeft;
            lbl1.BackColor = Color.Transparent;
            lbl1.ForeColor = Color.White;
            lbl1.Font = new Font("Tahoma", 13, FontStyle.Bold);
            lbl1.MouseDown += new MouseEventHandler(OnMouseDown);

            exitButton = new ImageButton("exit", "exit_hover");
            exitButton.Parent = this;
            exitButton.Click += new EventHandler(OnClickExit);
            exitButton.Name = "CloseButton";
            exitButton.Bounds = new Rectangle(Width - 65, 0, 65, 30);
            exitButton.BringToFront();

            baseArea = new Panel();
            baseArea.Parent = this;
            baseArea.ForeColor = Color.White;
            baseArea.BackColor = Color.Transparent;
            baseArea.BorderStyle = BorderStyle.FixedSingle;
            baseArea.Bounds = new Rectangle(0, 30, Width, Height - 30);
            baseArea.MouseDown += new MouseEventHandler(OnMouseDown);

            timFadeIn = new Timer();
            timFadeIn.Interval = 15;
            timFadeIn.Tick += new EventHandler(timFadeIn_Tick);
            timFadeIn.Enabled = true;

            timFadeOut = new Timer();
            timFadeOut.Enabled = false;
            timFadeOut.Interval = 10;
            timFadeOut.Tick += new EventHandler(timFadeOut_Tick);
        }

        virtual protected void OnFormActive()
        {
        }

        virtual protected void OnFormExit()
        {
            Dispose();
        }

        protected void OnMouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                return;
            }

            base.OnMouseDown(e);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (Opacity > 0)
            {
                e.Cancel = true;
                timFadeOut.Enabled = true;
                return;
            }

            OnFormExit();
            base.OnFormClosing(e);
        }

        private void OnClickExit(object sender, EventArgs e)
        {
            timFadeOut.Enabled = true;
        }

        private void timFadeOut_Tick(object sender, EventArgs e)
        {
            Opacity -= .05;
            if (Opacity <= .05)
            {
                Opacity = 0;
                timFadeOut.Enabled = false;
                Close();
            }
        }

        private void timFadeIn_Tick(object sender, EventArgs e)
        {
            Opacity += .05;
            if (Opacity >= .95)
            {
                Opacity = 1;
                timFadeIn.Enabled = false;
                OnFormActive();
            }
        }
    }
}
