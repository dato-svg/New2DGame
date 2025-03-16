using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace INFScenes
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private ErrorFixer _errorFixer;
        [SerializeField] private int nextSceneBuildIndex;
        [SerializeField] private GameObject OnFinishUI;
    
        public static int TargetCountOfFixedErrors { get; set; }


        private void Awake() => 
            TargetCountOfFixedErrors = 0;


        private void OnEnable() => 
            _errorFixer.Changed += OnCountOfFixedErrorChanged;

        private void OnDisable() => 
            _errorFixer.Changed -= OnCountOfFixedErrorChanged;

        private void OnCountOfFixedErrorChanged()
        {
            if (_errorFixer.CountOfFixedErrors >= TargetCountOfFixedErrors)
            {
                StopAllCoroutines();

                StartCoroutine(Finish());
            }
        }

        private IEnumerator Finish()
        {
            yield return new WaitForSecondsRealtime(1.5f);
        
            Time.timeScale = 0;

            yield return new WaitForSecondsRealtime(1f);

            OnFinishUI.SetActive(true);

            yield return new WaitForSecondsRealtime(5f);
        
            OnFinishUI.SetActive(false);

            Time.timeScale = 1f;

            SceneManager.LoadScene(nextSceneBuildIndex);
        }
    }
}
