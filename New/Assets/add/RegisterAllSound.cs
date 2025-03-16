using UnityEngine;
using UnityEngine.Serialization;

namespace add
{
    public class RegisterAllSound : MonoBehaviour
    {
        public static  RegisterAllSound Instance;
        
       public AudioClip errorTouch; 
       public string errorTouchString = "errorTouch";
       
       public AudioClip jumpSound;
       public string jumpSoundString = "jumpSound";
       
       public AudioClip walkSound;
       public string walkSoundString = "walkSound";


       private void Awake()
       {
           if (Instance == null)
           {
               Instance = this;
               DontDestroyOnLoad(this);
           }
           else
           {
               Destroy(this);
           }
           
       }

       private void Start()
       {
           AudioManager.Instance.RegisterSound(errorTouchString, errorTouch);
           AudioManager.Instance.RegisterSound(jumpSoundString, jumpSound);
           AudioManager.Instance.RegisterSound(walkSoundString, walkSound);
       }
       
    }
}
