using System;
using System.Collections;
using System.Collections.Generic;

using TMPro;
using Jint;
using Jint.Runtime.Interop;
using Satchel;
using System.IO;

using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ModScript
{
    public class ModScript:Mod,ICustomMenuMod
    {
        internal static ModScript Instance;
        internal ScriptManager scriptManager;

        public override string GetVersion()
        {
            return "v0.1";
        }
        
        public override void Initialize()
        {
            Instance = this;
            scriptManager = new ScriptManager();
        }
        public  bool ToggleButtonInsideMenu {get;}= true;
        public MenuScreen GetMenuScreen(MenuScreen modListMenu,ModToggleDelegates? toggle){
            return BetterMenu.GetMenu(modListMenu,toggle);
        }
    }
}
