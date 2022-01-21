ModScript

uses Jint interpretter to run scripts written in JavaScript with C# interop

- All loaded Assemblies are shared with JS
- All Types in Assembly-CSharp.dll & Playmaker.dll are exposed to JS (allows access to global types)
- Events can be subscribed to by using add_ prefix in JS , For Example
``` JS
On.PlayMakerFSM.add_OnEnable((orig,self)=>{
    orig(self);
})
```
- An example script ships with the mod (run the game once to create it)
- A special file `env.js` is created in the same folder as the mod dll, this file is loaded before any other file, so if some global setup is needed for your script, you can add it here.
