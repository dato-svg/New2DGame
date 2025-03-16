using System;
using UnityEngine;

namespace BaseScripts
{
    public class FollowCamera : MonoBehaviour
    {
        [field: SerializeField] public Transform Target { get; private set; }
        [field: SerializeField] public float Intensity { get; private set; }

        private Vector3 _offset;


        private void Awake()
        {
            _offset = transform.position - Target.position;
        }


        private void LateUpdate()
        {
            transform.position = Vector3.Lerp( transform.position, Target.position + _offset, Intensity * Time.deltaTime );
        }
    }
}