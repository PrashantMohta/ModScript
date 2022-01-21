using Modding;
using System;
using System.IO;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Satchel;
using Satchel.BetterMenus;

namespace ModScript
{
    public static class BetterMenu{
        
        internal static Menu MenuRef;
        private static void OpenScriptsFolder(){
            IoUtils.OpenDefault(Path.Combine(AssemblyUtils.getCurrentDirectory(),"Scripts"));
        }

        private static void OpenLink(string link){ 
            Application.OpenURL(link);
        }
        internal static Menu PrepareMenu(){
            var menu = new Menu("ModScripts",new Element[]{
                new TextPanel("Allows writing scripts in JS to mod the game."),  
              new MenuRow(
                    new List<Element>{
                        new MenuButton("Open Folder","Open Scripts Folder",(_)=>OpenScriptsFolder()),
                        new MenuButton("Need Help?","Join the HK Modding Discord",(_)=>OpenLink("https://discord.gg/rqsRHRt25h")),
                    },
                    Id:"HelpButtonGroup"
                ){ XDelta = 425f},
                new TextPanel("Loaded ModScripts"),
            });
            var ls = ModScript.Instance.scriptManager.loadedScripts;
            for(var i = 0; i < ls.Count ; i++){
                menu.AddElement(new TextPanel($"{ls[i]}"));
            }
            return menu;
        }
        internal static MenuScreen GetMenu(MenuScreen lastMenu, ModToggleDelegates? toggleDelegates){
            if(MenuRef == null){
                MenuRef = PrepareMenu();
            }
            return MenuRef.GetMenuScreen(lastMenu);
        }
    }
}
