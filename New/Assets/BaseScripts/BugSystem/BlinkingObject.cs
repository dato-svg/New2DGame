using System.Collections;
using UnityEngine;

namespace BaseScripts.BugSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BlinkingObject : MonoBehaviour, IBlinkable
    {
        [field:SerializeField] public bool Blinking { get; private set; }
        
        [SerializeField] private float activeTime = 2f; 
        [SerializeField] private float blinkInterval = 0.2f;
        [SerializeField] private int blinkCount = 5;
        
        [SerializeField] private BodyChanger _bodyChanger;
        private Collider2D _collider2D;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        
        private Coroutine _blinkCoroutine;

        private void Awake()
        {
            _bodyChanger = GetComponentInParent<BodyChanger>();
            _collider2D = GetComponent<Collider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            Activate();
        }
        
        [ContextMenu("Activate")]
        public void Activate()
        {
            blinkCount = 10;
            
            Deactivate();
            _blinkCoroutine = StartCoroutine(Blink());
        }
        
        private IEnumerator Blink()
        {
            while (blinkCount > 0)
            {
                yield return new WaitForSeconds(blinkInterval);
                ActiveEnableObject(false);
                yield return new WaitForSeconds(blinkInterval);
                ActiveEnableObject(true);
                blinkCount--;
                Blinking = true;
            }
            
            Debug.Log("blinking");
            _bodyChanger.ActiveRandomObject();
            ActiveEnableObject(true);
            Blinking = false;
            yield return new WaitForSeconds(activeTime);
            Activate();
        }


        [ContextMenu("Deactivate")]
        public void Deactivate()
        {
            if (_blinkCoroutine == null)
                return;
            
            StopCoroutine(_blinkCoroutine);
        }

        private void ActiveEnableObject(bool active)
        {
            _spriteRenderer.enabled = active;
            _collider2D.enabled = active;
            _rigidbody2D.bodyType = active ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        }
    }
}