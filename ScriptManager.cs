using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using Jint;
using Jint.Native;
using Jint.Runtime.Interop;

using Modding;
using UnityEngine;
using UnityEngine.SceneManagement;
using HutongGames.PlayMaker;


using Satchel;
using TMPro;
namespace ModScript
{
    public  class ScriptManager 
    {
        internal static Engine JS;

        public List<string> loadedScripts = new();

        public ScriptManager(){
            var CUR_DIR = AssemblyUtils.getCurrentDirectory();
            var DIR = Path.Combine(CUR_DIR,"Scripts");
            if(!Directory.Exists(DIR)){
                Directory.CreateDirectory(DIR);
                File.WriteAllBytes(Path.Combine(CUR_DIR,"env.js"),AssemblyUtils.GetBytesFromResources("env.js"));
                File.WriteAllBytes(Path.Combine(DIR,"example.js"),AssemblyUtils.GetBytesFromResources("example.js"));
            }
            StartModScripts();
        }   


        public void AddAssemblyTypeReferences(Assembly asm){
            var AssemblyTypes = asm.GetTypes();
            foreach(var type in AssemblyTypes){
                JS.SetValue(type.ToString(), TypeReference.CreateTypeReference(JS, type)); // adds all types in assembly c# in JS 
            }
        }
        public  void StartModScripts(){
            JS = new Engine(
                cfg => {
                cfg.AllowClr(AppDomain.CurrentDomain.GetAssemblies());
                cfg.CatchClrExceptions();
                }
            );    
            AddAssemblyTypeReferences(Assembly.GetAssembly(typeof(HeroController))); //Assembly-CSharp
            AddAssemblyTypeReferences(Assembly.GetAssembly(typeof(PlayMakerFSM)));  //Playmaker
            InitScript(Path.Combine(AssemblyUtils.getCurrentDirectory(),"env.js"));
            LoadScripts("Scripts");
        }

        public void LoadScripts(string path){
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
            loadedScripts.Add(Path.GetFileName(path));
            var script = File.ReadAllText(path);
            JS.Execute(script);
        }

        private  void Log(string s) => ModScript.Instance.Log(s);

    }
}
