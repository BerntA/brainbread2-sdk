//=========       Copyright © Reperio Studios 2013-2019 @ Bernt Andreas Eide!       ============//
//
// Purpose: Main form.
//
//=============================================================================================//

using BB2SDK.Controls;
using Steamworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BB2SDK.UI
{
    public partial class SDK : BaseForm
    {
        public SDK()
            : base()
        {
            Opacity = 0;

            TableLayoutPanel layout = new TableLayoutPanel();
            layout.Parent = baseArea;
            layout.Dock = DockStyle.Fill;

            Label lbl1 = new Label();
            lbl1.Dock = DockStyle.Top;
            lbl1.TextAlign = ContentAlignment.MiddleLeft;
            lbl1.Text = "Applications";
            lbl1.ForeColor = Color.Yellow;
            lbl1.Font = new Font("Calibri", 11, FontStyle.Italic);
            lbl1.Parent = layout;
            lbl1.MouseDown += new MouseEventHandler(OnMouseDown);

            Label lbl2 = new Label();
            lbl2.Dock = DockStyle.Top;
            lbl2.TextAlign = ContentAlignment.MiddleLeft;
            lbl2.Text = "Documentation";
            lbl2.ForeColor = Color.Yellow;
            lbl2.Font = new Font("Calibri", 11, FontStyle.Italic);
            lbl2.Parent = layout;
            lbl2.MouseDown += new MouseEventHandler(OnMouseDown);

            layout.SetCellPosition(lbl1, new TableLayoutPanelCellPosition(0, 0));

            string gamePath = string.Empty;
            if (Program.steamAPIStatus && SteamApps.BIsAppInstalled((AppId_t)346330))
                SteamApps.GetAppInstallDir((AppId_t)346330, out gamePath, 1024);

            if (!string.IsNullOrEmpty(gamePath) && !string.IsNullOrWhiteSpace(gamePath))
                UpdateHammerGameConfig(gamePath);

            layout.SetCellPosition(CreateItem(layout, "Level Editor", "hammer", string.Format("{0}\\bin\\hammer.exe", gamePath)), new TableLayoutPanelCellPosition(0, 1));
            layout.SetCellPosition(CreateItem(layout, "Model Viewer", "hlmv", string.Format("{0}\\bin\\hlmv.exe", gamePath), string.Format("-game \"{0}\\brainbread2\"", gamePath)), new TableLayoutPanelCellPosition(0, 2));
            layout.SetCellPosition(CreateItem(layout, "VPK UI", "vpkui", string.Format("{0}\\bin\\vpkui.exe", gamePath)), new TableLayoutPanelCellPosition(0, 3));
            layout.SetCellPosition(CreateItem(layout, "Faceposer", "faceposer", string.Format("{0}\\bin\\hlfaceposer.exe", gamePath), string.Format("-game \"{0}\\brainbread2\"", gamePath)), new TableLayoutPanelCellPosition(0, 4));
            layout.SetCellPosition(CreateItem(layout, "QC Eyes", "qceyes", string.Format("{0}\\bin\\qc_eyes.exe", gamePath)), new TableLayoutPanelCellPosition(0, 5));
            layout.SetCellPosition(CreateItem(layout, "Workshopper", "workshopper", "./workshopper.exe"), new TableLayoutPanelCellPosition(0, 6));

            layout.SetCellPosition(lbl2, new TableLayoutPanelCellPosition(0, 7));

            layout.SetCellPosition(CreateItem(layout, "BrainBread 2 Workshop Guide", "document", "https://steamcommunity.com/sharedfiles/filedetails/?id=381191681"), new TableLayoutPanelCellPosition(0, 8));
            layout.SetCellPosition(CreateItem(layout, "Workshop Terms & Conditions", "document", "https://steamcommunity.com/workshop/workshoplegalagreement/"), new TableLayoutPanelCellPosition(0, 9));
            layout.SetCellPosition(CreateItem(layout, "HowTo - Objectives", "document", "https://steamcommunity.com/sharedfiles/filedetails/?id=467018609"), new TableLayoutPanelCellPosition(0, 10));
            layout.SetCellPosition(CreateItem(layout, "Steam Community Hub", "document", "https://steamcommunity.com/app/346330/guides/?searchText=&browsefilter=trend&browsesort=creationorder&requiredtags[]=Modding+or+Configuration&requiredtags[]=-1"), new TableLayoutPanelCellPosition(0, 11));

            Text = "BrainBread 2 Mod Tools";
        }

        public ToolItem CreateItem(Control layout, string text, string image, string uri, string args = null)
        {
            ToolItem it = new ToolItem(text, image, uri, args);
            it.Parent = layout;
            it.Dock = DockStyle.Top;
            it.Height = 24;
            return it;
        }

        private void UpdateHammerGameConfig(string path)
        {
            try
            {
                string hammerConfig = Properties.Resources.GameConfig.Replace("%s1", path);
                File.WriteAllText(string.Format("{0}\\bin\\GameConfig.txt", path), hammerConfig, Encoding.Default);
            }
            catch
            {
                MessageBox.Show(this, "Unable to create the game config for hammer editor!", "Fatal Error!");
            }
        }

        protected override void OnFormActive()
        {
            base.OnFormActive();

            if (!Program.steamAPIStatus)
            {
                MessageBox.Show(this, "Unable to init Steam API!", "Fatal Error!");
                Close();
                return;
            }

            bool appidCheck = (SteamUtils.GetAppID() == (AppId_t)382990);
            if (!appidCheck)
            {
                MessageBox.Show(this, "Invalid Steam AppID detected! Verify your files!", "Fatal Error!");
                Close();
                return;
            }
        }
    }
}
