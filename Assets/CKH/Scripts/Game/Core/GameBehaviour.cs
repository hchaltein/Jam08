using UnityEngine;
using System.Collections;

public class GameBehaviour : MonoBehaviour
{
    // It's hard to keep track of which method is a message.
    // messages could be private methods that are NEVER called by 
    // your class... some visual studio configurations could even 
    // give you a warning saying that the method is private and is 
    // never used.. so you can easily delete those methods thinking
    // that they are never gonna be called, but in reality unity 
    // will call those methods even if they are private.
    // 
    // There is a HUGE list of MonoBehaviour messages that you can use
    // in your methods and I usualy keep them insade a "Unity Messages"
    // region to avoid any confusion.
    //
    // You can find the list here:
    // http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
    #region Unity Messages

    /// <summary>
    /// OnEnable is called whenever the component is enabled.
    /// This could be when the scene is loaded and the object is
    /// already enabled or when you set "this.enabled = true" in
    /// a disabled object or activate it's object go.SetActive(true).
    /// Use this method to initialize all variables that need to 
    /// be reset whenever the object is disabled/enabled.
    /// 
    /// You can find a nice discussion about OnEnable, Awake and Start here:
    /// http://answers.unity3d.com/questions/217941/onenable-awake-start-order.html
    /// </summary>
    protected virtual void OnEnable()
    {
    }

    /// <summary>
    /// OnDisable is called whenever the component or it's object is disabled 
    /// (this.enabled = false; gameObject.SetActive(false) Duh =P) or destroyed 
    /// (Destroy(myAwesomeComponent) or Destroy(myAwesomeObject))
    /// You can use this to play sounds on projectile hit for example.
    /// </summary>
    protected virtual void OnDisable()
    {
    }

    /// <summary>
    /// OnDestroy is called whenever an object is destroyed addition to OnDesable.
    /// </summary>
    protected virtual void OnDestroy()
    {

    }

    /// <summary>
    /// Awake is called when the component is created.
    ///  * When the scene is loaded;
    ///  * When you create an instance of the object (Object.Instaciate);
    ///  * When you add a component to an existing game object (gameObject.AddComponent);
    /// 
    /// I generally use Awake to setup cross-reference between components/objects.
    /// (It is safe to reference external components in the Away method because at this point
    /// all objects were already loaded in the scene. However, its not safe to use those 
    /// references since the other objects might not have set themselves up yet)
    /// </summary>
    protected virtual void Awake()
    {
    }

    /// <summary>
    /// Start is called right after the first time the object is enabled.
    /// At the point start is called it's safe to assume that every object loaded with 
    /// the scene is set-up. In other words, if you did reference everything in your
    /// Awake methods, it's now safe to start using your references. (actually 
    /// in some extreme cases, what you really need is to tweak the script execution
    /// order, but in MOST cases, you are safe to use your references in the Start method)
    /// </summary>
    protected virtual void Start()
    {
        World.Register(this);
    }

    /// <summary>
    /// Use Update for physics non-related stuff (Speccialy Input stuff!!). Since 
    /// Since InputDown and InputUp events can sometimes be missed in between FixedUpdate loops.
    /// 
    /// Differences between Update and FixedUpdate:
    /// https://unity3d.com/pt/learn/tutorials/modules/beginner/scripting/update-and-fixedupdate
    /// </summary>
    protected virtual void Update()
    {
    }

    /// <summary>
    /// Use FixedUpdate when dealing with Rigid Bodies.
    /// </summary>
    protected virtual void FixedUpdate()
    {
    }
    #endregion

    /// <summary>
    /// Called whenever the player shifts dimensions.
    /// </summary>
    public virtual void ShiftTo(Dimensions dimension)
    {
    }

    /// <summary>
    /// Save your state! we just got to a checkpoint.
    /// </summary>
    public virtual void SetCheckpoint()
    {
    }

    /// <summary>
    /// It seems that we just died! Load back your checkyount state.
    /// </summary>
    public virtual void LoadCheckpoint()
    {
    }
}
