using System;
using UnityEngine;

public class CompilingSliderAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 startAnchoredPosition;
    [SerializeField] private Vector3 endAnchoredPosition;
    [SerializeField] private float speed;

    private RectTransform rectTransform;
    private float progress;

    private void Awake()
    {
        rectTransform = transform as RectTransform;
    }


    private void Update()
    {
        progress = (progress + speed * Time.unscaledDeltaTime) % 1;

        rectTransform.anchoredPosition = Vector3.Lerp(startAnchoredPosition, endAnchoredPosition, progress);
    }
}
