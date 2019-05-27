//=========       Copyright © Reperio Studios 2013-2019 @ Bernt Andreas Eide!       ============//
//
// Purpose: Main entry point.
//
//=============================================================================================//

using BB2SDK.UI;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BB2SDK
{
    public static class Program
    {
        public static bool steamAPIStatus { get; set; }
        [STAThread]
        static void Main()
        {
            steamAPIStatus = SteamAPI.Init();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SDK());

            if (steamAPIStatus)
                SteamAPI.Shutdown();
        }
    }
}
