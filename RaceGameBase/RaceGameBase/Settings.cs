using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Win32;
using Microsoft.Xna.Framework.Input;

namespace RaceGameBase
{
    public class Settings
    {
        public static RegistryKey key;

        public static void Initialize()
        {
            key = Registry.CurrentUser.CreateSubKey(@"Software\AddictedToSpeed", RegistryKeyPermissionCheck.ReadWriteSubTree,  RegistryOptions.None);
            SetKey("Fullscreen", false);
            SetKey("Sound", true);
            SetKey("Music", true);
            SetKey("Effects", true);

            SetKey("P1left", (int)Keys.Left);
            SetKey("P1right", (int)Keys.Right);
            SetKey("P1gas", (int)Keys.Up);
            SetKey("P1brake", (int)Keys.Down);
            SetKey("P1nitro", (int)Keys.RightShift);

            SetKey("P2left", (int)Keys.A);
            SetKey("P2right", (int)Keys.D);
            SetKey("P2gas", (int)Keys.W);
            SetKey("P2brake", (int)Keys.S);
            SetKey("P2nitro", (int)Keys.LeftShift);
        }

        public static void SetKey(string name, object value)
        {         
            if (key.GetValue(name) == null)
            {
                key.SetValue(name, value);
            }
        }

        public static void ChangeKey(string name, object value)
        {
            key.SetValue(name, value);
        }

        public static object GetKey(string name)
        {
            if (key.GetValue(name) != null)
            {
                return key.GetValue(name);
            }
            throw new Exception("Key doesn't exists");
            
        }

    }
}