using add;
using UnityEngine;

namespace BaseScripts.PlayerMovement
{
    public class PlayerJumper : IJumpable
    {
        private readonly Rigidbody2D _rb;
        private readonly PlayerData _data;
        private readonly LayerMask _groundLayer;

        public PlayerJumper(Rigidbody2D rb, PlayerData data, LayerMask groundLayer)
        {
            _rb = rb;
            _data = data;
            _groundLayer = groundLayer;
        }

        public void Jump()
        {
            if (_data.IsGrounded)
            {
                _rb.AddForce(Vector2.up * _data.JumpForce, ForceMode2D.Impulse);
                AudioManager.Instance.PlaySound(RegisterAllSound.Instance.jumpSoundString);
                _data.IsGrounded = false;
            }
        }
        
        public void UpdateGroundedState() =>
            _data.IsGrounded = GroundCheck();
        
        private bool GroundCheck()
        {
            RaycastHit2D hit = Physics2D.CircleCast(
                _rb.position,
                _data.GroundCheckerRadius,
                Vector2.down,
                _data.GroundCheckerDistance,
                _groundLayer
            );
            
            return hit.collider != null;

        }
    }
}