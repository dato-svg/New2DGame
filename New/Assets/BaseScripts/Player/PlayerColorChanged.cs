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
        
        [SerializeField] private AudioClip errorTouch; 
        private readonly string _errorTouchString = "ErrorTouch"; 
        
        private SpriteRenderer _spriteRenderer;
        
        private Coroutine _coroutine;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = baseColor;
            AudioManager.Instance.RegisterSound(_errorTouchString, errorTouch);
            Debug.Log("Register Sound:"  + _errorTouchString);
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
            Debug.Log("Start ColorChanger");
            AudioManager.Instance.PlaySound(_errorTouchString,1,false);
            GameObject bug = Instantiate(ITSABAGXAXAA,transform.position,Quaternion.identity);
            _spriteRenderer.color = touchColor;
            yield return new WaitForSeconds(0.3f);
            _spriteRenderer.color = baseColor;
        }
    }
}
