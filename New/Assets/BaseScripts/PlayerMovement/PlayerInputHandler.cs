using UnityEngine;
using UnityEngine.InputSystem;

namespace BaseScripts.PlayerMovement
{
    public class PlayerInputHandler
    {
        private readonly PlayerInput _playerInput;

        public float MoveDirection { get; private set; }
        public bool JumpPressed { get; private set; }

        public PlayerInputHandler()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();

            _playerInput.Player.Move.performed += Move;
            _playerInput.Player.Move.canceled += CanceledMove;

            _playerInput.Player.Jump.performed += Jumped;
        }

        private void Jumped(InputAction.CallbackContext obj) => 
            JumpPressed = true;

        private void CanceledMove(InputAction.CallbackContext obj) => 
            MoveDirection = 0;

        private void Move(InputAction.CallbackContext obj) => 
            MoveDirection = obj.ReadValue<Vector2>().x;

        public void ResetJump() => 
            JumpPressed = false;
    }
}