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
    public class ModScript:Mod 
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
            ModHooks.HeroUpdateHook += HeroUpdate;
            scriptManager = new ScriptManager();
        }

        internal void HeroUpdate()
        {
            if(Input.GetKeyDown(KeyCode.R)){
                scriptManager.DisableHooks();
                scriptManager = new ScriptManager();
            }
        }

    }
}
