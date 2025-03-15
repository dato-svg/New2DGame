using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace BaseScripts.PlayerMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Character : MonoBehaviour
    {
        [FormerlySerializedAs("_playerRB")] [SerializeField] private Rigidbody2D playerRB;
        [FormerlySerializedAs("_groundLayer")] [SerializeField] private LayerMask groundLayer;
        public PlayerData playerData;

        private IMovable _mover;
        private IJumpable _jumper;
        private PlayerInputHandler _inputHandler;

        private void Awake()
        {
            playerRB = GetComponent<Rigidbody2D>();

            _mover = new PlayerMover(playerRB, playerData);
            _jumper = new PlayerJumper(playerRB, playerData,groundLayer);
            _inputHandler = new PlayerInputHandler();
        }
        
        private void Update()
        {
            _mover.Move(_inputHandler.MoveDirection);
            
            _jumper.UpdateGroundedState();

            if (_inputHandler.JumpPressed && playerData.IsGrounded)
            {
                _jumper.Jump();
                _inputHandler.ResetJump();
            }
        }
        
    }
}