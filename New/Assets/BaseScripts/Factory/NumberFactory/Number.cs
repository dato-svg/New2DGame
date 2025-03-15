using BaseScripts.Visitor;
using DG.Tweening;
using UnityEngine;

namespace BaseScripts.Factory.NumberFactory
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Number : MonoBehaviour
    {
        [field: SerializeField, Range(5f, 25f)] public float RotationDuration { get; private set; } = 10f;
        
        [SerializeField] private NumberSpawner _spawner;
        
        private float _rotationDirection; 
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rotationDirection = -1;
        }

        public void Initialize(NumberSpawner spawner)
        {
            _spawner = spawner;
        }
        
        
        private void Update()
        {
            RotateTo();
        }
           
        
        public abstract void Accept(NumberVisitor visitor);
        
        public void MoveTo(Vector2 position) => 
            transform.position = position;
        
        
        public void RotateTo() 
        {
            float targetRotation = _rotationDirection * 360f;
            transform.DORotate(new Vector3(0, 0, targetRotation), RotationDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Restart)
                .SetEase(Ease.Linear);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player")) // TODO -- CHANGE TAG
                _spawner.TouchPlayerNumber(this);
            
            else if (other.collider.CompareTag("Floor")) // TODO --CHANGE THIS SHIT TOО
            {
              
                if (gameObject.GetComponent<OneNumber>()) 
                    _spawner.KillCurrentNumber(this,NumberType.One);

                if (gameObject.GetComponent<ZeroNumber>()) 
                    _spawner.KillCurrentNumber(this,NumberType.Zero);
               
            }
        }
        
    }
}