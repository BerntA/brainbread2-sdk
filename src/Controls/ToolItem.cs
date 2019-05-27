//=========       Copyright © Reperio Studios 2013-2019 @ Bernt Andreas Eide!       ============//
//
// Purpose: Tool App/URL Item.
//
//=============================================================================================//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BB2SDK.Core;
using System.Diagnostics;

namespace BB2SDK.Controls
{
    public partial class ToolItem : UserControl
    {
        private bool m_bRollover;
        private Image m_pTexture;
        private string _text;
        private string _uri;
        private string _args;
        public ToolItem(string text, string image, string uri, string args)
        {
            this._text = text;
            this._uri = uri;
            this._args = args;
            m_bRollover = false;
            m_pTexture = Globals.GetTexture(image);
            InitializeComponent();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            m_bRollover = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            m_bRollover = false;
            Invalidate();
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            try
            {
                Process.Start(this._uri, string.IsNullOrEmpty(this._args) ? string.Empty : this._args);
            }
            catch
            {
                MessageBox.Show(this, string.Format("Unable to launch '{0}'!", this._uri), "Fatal Error!");
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            StringFormat f = new StringFormat();
            f.LineAlignment = StringAlignment.Center;
            f.Alignment = StringAlignment.Near;

            if (m_bRollover)
                e.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, Width, Height));

            e.Graphics.DrawImage(m_pTexture, 1, 1, Height - 2, Height - 2);
            e.Graphics.DrawString(this._text, this.Font, new SolidBrush(m_bRollover ? Color.Black : Color.White), new RectangleF(Height + 3, 0, Width - Height, Height), f);
        }
    }
}
