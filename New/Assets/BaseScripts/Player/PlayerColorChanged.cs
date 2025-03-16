using System.Collections;
using add;
using UnityEngine;

namespace BaseScripts.Player
{
    public class PlayerColorChanged : MonoBehaviour
    {
        [SerializeField] private Color touchColor;
        [SerializeField] private Color baseColor;

        [SerializeField] private GameObject ITSABAGXAXAA;
        
        private SpriteRenderer _spriteRenderer;
        
        private Coroutine _coroutine;
        
        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = baseColor;
        }
        
        public void StartCoroutiner()
        {
            StopCoroutiner();
            _coroutine = StartCoroutine(StartColorChanger());
        }
        
        private void StopCoroutiner()
        {
            if (_coroutine == null)
                return;
            
            StopCoroutine(_coroutine);
        }
        
        
        private IEnumerator StartColorChanger()
        {
            AudioManager.Instance.PlaySound(RegisterAllSound.Instance.errorTouchString, 1, false);
            GameObject bug = Instantiate(ITSABAGXAXAA,transform.position,Quaternion.identity);
            _spriteRenderer.color = touchColor;
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = baseColor;
        }
    }
}
