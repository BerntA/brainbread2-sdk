//=========       Copyright © Reperio Studios 2013-2019 @ Bernt Andreas Eide!       ============//
//
// Purpose: Image Button.
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

namespace BB2SDK.Controls
{
    public partial class ImageButton : UserControl
    {
        private bool m_bRollover;
        private Image m_pTextureDefault;
        private Image m_pTextureOver;
        public ImageButton(string textureDefault, string textureRollover)
        {
            m_bRollover = false;

            m_pTextureDefault = Globals.GetTexture(textureDefault);
            m_pTextureOver = Globals.GetTexture(textureRollover);

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

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage((m_bRollover ? m_pTextureOver : m_pTextureDefault), 0, 0, Width, Height);
        }
    }
}
