using System;
using BaseScripts.BugSystem;
using UnityEngine;

public class Error : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent<IBlinkable>(out var blinkable))
        {
            blinkable.Deactivate();
        }
    }
}
