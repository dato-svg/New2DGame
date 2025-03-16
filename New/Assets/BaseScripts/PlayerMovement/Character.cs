using add;
using UnityEngine;
using UnityEngine.Serialization;

namespace BaseScripts.PlayerMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour
    {
        [FormerlySerializedAs("_playerRB")] [SerializeField] private Rigidbody2D playerRB;
        [FormerlySerializedAs("_groundLayer")] [SerializeField] private LayerMask groundLayer;
        public PlayerData playerData;
        public Animator animator;
        
        private IMovable _mover;
        private IJumpable _jumper;
        private PlayerInputHandler _inputHandler;
        private AnimationPlayer  _animationPlayer;

        [SerializeField] private GameObject skin;
        [SerializeField] private  bool normalPerson;
        
        private SpriteRenderer _spriteRenderer;
        

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.enabled = true;
            skin.SetActive(false);
            
            playerRB = GetComponent<Rigidbody2D>();

            _mover = new PlayerMover(playerRB, playerData);
            _jumper = new PlayerJumper(playerRB, playerData,groundLayer);
            _inputHandler = new PlayerInputHandler();
            _animationPlayer = new AnimationPlayer(animator);
        }
        
        private void Update()
        {
            _mover.Move(_inputHandler.MoveDirection);
            if(_inputHandler.MoveDirection != 0 ) 
                _animationPlayer.StartRun();

            else if (_inputHandler.MoveDirection == 0)
                _animationPlayer.StopRun();

            PlayerRotation();
            _jumper.UpdateGroundedState();

            if (_inputHandler.JumpPressed && playerData.IsGrounded)
            {
                _jumper.Jump();
                _animationPlayer.StartJump();
                _inputHandler.ResetJump();
            }
        }

        private void PlayerRotation()
        {
            if (normalPerson)
            {
                _spriteRenderer.enabled = false;
                skin.SetActive(true);
                
                var euler = Random.Range(-180, 180);
                if (_inputHandler.MoveDirection >= 1)
                    transform.rotation = Quaternion.Euler(0, euler, euler);
            
                else if(_inputHandler.MoveDirection <= -1) 
                    transform.rotation = Quaternion.Euler(0, -euler, -euler);
            }
        }
        
    }
}