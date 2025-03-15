using BaseScripts.Visitor;
using UnityEngine;

namespace BaseScripts.Factory.NumberFactory
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Number : MonoBehaviour
    {
        [field:SerializeField,Range(1,500)]public float Speed {get; private set; }
        private Rigidbody2D _rigidbody;
        private void Awake() => 
            _rigidbody = GetComponent<Rigidbody2D>();
        
        private void Update() => 
            MoveDown();

        public abstract void Accept(NumberVisitor visitor);
        
        public void MoveTo(Vector2 position) => 
            transform.position = position;

        public void MoveDown() => 
            _rigidbody.linearVelocity = new Vector2(0, -Speed * Time.deltaTime);
    }
}