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
            steamAPIStatus = InitSteamAPI();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SDK());
            ShutdownSteamAPI();
        }

        static bool InitSteamAPI()
        {
            try
            {
                return SteamAPI.Init();
            }
            catch
            {
                return false;
            }
        }

        static void ShutdownSteamAPI()
        {
            if (steamAPIStatus == false)
                return;

            try
            {
                SteamAPI.Shutdown();
            }
            catch
            {
            }
        }
    }
}
