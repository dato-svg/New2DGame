using System;
using UnityEngine;

public class ErrorFixer : MonoBehaviour
{
    public int CountOfFixedErrors
    {
        get
        {
            return _count;
        }
        set
        {
            _count = value;

            Changed?.Invoke();
        }
    }
    public event Action Changed;

    private int _count;
    
    [SerializeField] private Rigidbody2D _commentedErrorPrefab;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private AudioSource _fixedSound;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.TryGetComponent<Error>(out var error))
        {
            var commentedError = Instantiate(_commentedErrorPrefab, error.transform.position, error.transform.rotation);

            commentedError.linearVelocity = error.GetComponent<Rigidbody2D>().linearVelocity;
            commentedError.angularVelocity = error.GetComponent<Rigidbody2D>().angularVelocity;

            Destroy(error.gameObject);
            Destroy(error);

            CountOfFixedErrors++;

            if (_fixedSound) _fixedSound.Play();

            _particles.Play();
        }
    }
}
