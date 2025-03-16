using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace INFScenes
{


    public static int TargetCountOfFixedErrors
    {
        get
        {
            return _targetCount;
        }
        set
        {
            _targetCount = value;
            
            FindAnyObjectByType<Game>().OnCountOfFixedErrorChanged();
        }
    }

    private static int _targetCount;
    
    private List<GameObject> _errorMessages = new List<GameObject>();
    
    




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
