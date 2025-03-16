using System.Collections;
using BaseScripts.SceneGod;
using UnityEngine;

namespace BaseScripts.Camera222
{
    public class CameraMoveToPlayer : MonoBehaviour
    {
        private static readonly int Move = Animator.StringToHash("Move");
        [SerializeField] private Animator animator;
        
        [SerializeField] private AudioSource source;
        [SerializeField] private GameStarter  gameStarter;

        private bool _isMoving;
        private void Awake()
        {
            animator = GetComponent<Animator>();
            source = GetComponent<AudioSource>();
        }

        private void Start() => 
            source.Play();

        private void Update()
        {
            Debug.Log("s" + source.isPlaying);
            if (!_isMoving)
            {
                if (!source.isPlaying)
                {
                    animator.SetTrigger(Move);
                    _isMoving = true;
                    gameStarter.CanChangeScene = true;
                }
                  
            }
        }

        
    }
}
