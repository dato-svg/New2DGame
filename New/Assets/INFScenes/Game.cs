using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private ErrorFixer _errorFixer;
    [SerializeField] private int nextSceneBuildIndex;
    [SerializeField] private GameObject OnFinishUI;
    [Header("Error Messages")]
    [SerializeField] private GameObject ErrorMessagePrefab;
    [SerializeField] private Transform ErrorMessagesContent;
    [SerializeField] private TextMeshProUGUI ErrorsCountBar;

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
    
    

    private void Awake()
    {
        _targetCount = 0;
    }

    [ContextMenu("Create null reference error")]
    public void TestError()
    {
        throw new NullReferenceException();
    }


    private void OnEnable()
    {
        _errorFixer.Changed += OnCountOfFixedErrorChanged;
    }

    private void OnDisable()
    {
        _errorFixer.Changed -= OnCountOfFixedErrorChanged;
    }

    private void OnCountOfFixedErrorChanged()
    {
        if (_errorFixer.CountOfFixedErrors >= TargetCountOfFixedErrors)
        {
            StopAllCoroutines();

            StartCoroutine(Finish());
        }

        UpdateErrorsMessages();
    }

    private void UpdateErrorsMessages()
    {
        var targetMessagesCount = Game.TargetCountOfFixedErrors - _errorFixer.CountOfFixedErrors;
        
        while (_errorMessages.Count < targetMessagesCount)
        {
            _errorMessages.Add( Instantiate(ErrorMessagePrefab, ErrorMessagesContent) );
        }

        while (_errorMessages.Count > targetMessagesCount)
        {
            Destroy(_errorMessages.FirstOrDefault());
            _errorMessages.RemoveAt(0);
        }
        
        ErrorsCountBar.text = targetMessagesCount.ToString();
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
