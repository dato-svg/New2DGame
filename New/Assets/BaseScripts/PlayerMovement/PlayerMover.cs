using UnityEngine;

namespace BaseScripts.PlayerMovement
{
    public class PlayerMover : IMovable
    {
        private readonly Rigidbody2D _rb;
        private readonly PlayerData _data;

        public PlayerMover(Rigidbody2D rb, PlayerData data)
        {
            _rb = rb;
            _data = data;
        }

        public void Move(float direction)
        {
            Vector2 velocity = _rb.linearVelocity;
            velocity.x = direction * _data.Speed;
            
            _rb.linearVelocity = velocity;
        }
    }
}