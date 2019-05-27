//=========       Copyright © Reperio Studios 2013-2019 @ Bernt Andreas Eide!       ============//
//
// Purpose: Global Defs.
//
//=============================================================================================//

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BB2SDK.Core
{
    public static class Globals
    {
        public static string GetAppPath() { return Application.StartupPath; }
        public static string GetTexturePath() { return string.Format("{0}\\assets", GetAppPath()); }

        // Return the image found at the specified 'path'.
        public static Image GetTexture(string folder, string name, string extension = "png")
        {
            try
            {
                return Image.FromFile(string.Format("{0}\\{1}\\{2}.{3}", GetTexturePath(), folder, name, extension));
            }
            catch
            {
                return Properties.Resources.unknown;
            }
        }

        // Return the image found, does a recursive search for the desired image name.
        public static Image GetTexture(string name, string extension = "png")
        {
            try
            {
                string folder = null;
                foreach (string file in Directory.EnumerateFiles(GetTexturePath(), string.Format("*.{0}", extension), SearchOption.AllDirectories))
                {
                    string rawFileName = Path.GetFileNameWithoutExtension(file);
                    if ((rawFileName.Contains(name)) && (rawFileName.Length == name.Length))
                        folder = Path.GetDirectoryName(file).Replace(GetTexturePath(), "");
                }

                return Image.FromFile(string.Format("{0}\\{1}\\{2}.{3}", GetTexturePath(), folder, name, extension));
            }
            catch
            {
                return Properties.Resources.unknown;
            }
        }
    }
}
