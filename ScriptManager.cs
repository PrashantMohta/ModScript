using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using Jint;
using Jint.Native;
using Jint.Runtime.Interop;

using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;


using Satchel;
using TMPro;
namespace ModScript
{
    public  class ScriptManager 
    {
        internal static Engine JS;
        private bool enabled = true;

        public ScriptManager(){
            StartModScripts();
        }   

        public void DisableHooks(){
            enabled = false;
        }
        public  void StartModScripts(){
            JS = new Engine(cfg => cfg.AllowClr(AppDomain.CurrentDomain.GetAssemblies()));    
            JS.SetValue("HeroController", TypeReference.CreateTypeReference(JS, typeof(HeroController)));
            LoadScripts("System");
            InitScript(Path.Combine(AssemblyUtils.getCurrentDirectory(),"env.js"));
            LoadScripts("Scripts");
            var HeroUpdateHook = JS.GetValue("__HeroUpdateHook");
            ModHooks.HeroUpdateHook += () => {
                if(enabled && HeroUpdateHook.IsCallable()){
                    JS.Invoke(HeroUpdateHook);
                }
            };
            var activeSceneChanged = JS.GetValue("__activeSceneChanged");
            UnityEngine.SceneManagement.SceneManager.activeSceneChanged += (Scene to,Scene from) => {
                if(enabled && activeSceneChanged.IsCallable()){
                    JS.Invoke(activeSceneChanged, to,from);
                }
            };
            
        }

        public  void LoadScripts(string path){
            var DIR = Path.Combine(AssemblyUtils.getCurrentDirectory(),path);
            if(!Directory.Exists(DIR)){
                Log($"Scripts Folder Does not exist at {DIR}");
                return;
            }
            foreach(string file in Directory.GetFiles(DIR)){
                string filename = Path.GetFileName(file);
                if(filename.EndsWith(".js")){
                    try{
                        InitScript(Path.Combine(DIR,filename));
                    } catch(Exception e){
                        Log($"Error Loading ModScript: {filename}");
                        Log(e.ToString());
                    }
                }
            }
        }

        public  void InitScript(string path){
            if(!File.Exists(path)) { 
                Log($"No Such file : {path}");
                return; 
            }
            var script = File.ReadAllText(path);
            JS.Execute(script);
        }

        private  void Log(string s) => ModScript.Instance.Log(s);

    }
}
