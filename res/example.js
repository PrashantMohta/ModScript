/* commented by default to prevent modlog spam

On = importNamespace("On");
ModHooks = Modding.ModHooks;

On.PlayMakerFSM.add_OnEnable((orig,self)=>{
    orig(self);
    Log(self.FsmName);
})

function HeroUpdateHookFn(){
    var hc = HeroController.instance;
    Log(hc.gameObject.transform.position.ToString()); 
}

ModHooks.add_HeroUpdateHook(HeroUpdateHookFn)
*/

UnityEngine.SceneManagement.SceneManager.add_activeSceneChanged((to,from)=>{
    Log(`changed scene from ${from.name} to ${to.name}`);
});