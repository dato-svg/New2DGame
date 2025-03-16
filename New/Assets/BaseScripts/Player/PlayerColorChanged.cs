using System.Collections;
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
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = baseColor;
        }
        
        private IEnumerator StartColorChanger()
        {
            GameObject bug = Instantiate(ITSABAGXAXAA,transform.position,Quaternion.identity);
            _spriteRenderer.color = touchColor;
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = baseColor;
        }
    }
}
