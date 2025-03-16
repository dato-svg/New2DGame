using System.Collections.Generic;
using UnityEngine;

namespace BaseScripts.BugSystem
{
    public class BodyChanger : MonoBehaviour
    {
        [SerializeField] private List<GameObject> bodyPrefabs;

        private int _index;
        private Vector3 _lastPosition;

        private void Awake()
        { 
            bodyPrefabs.AddRange(GetChildObjects()); 
        }

       
        
        private void Start()
        {
            ActiveRandomObject();
        } 
        
        private void Update()
        {
            if (bodyPrefabs[_index].GetComponent<BlinkingObject>().BlinkWait == false)
                ActiveRandomObject();
        }
        
        [ContextMenu("ActiveRandomObject")]
        public void ActiveRandomObject()
        {
            Debug.Log("ActiveRandomObject");
            
            _lastPosition = bodyPrefabs[_index].transform.position;

            foreach (var obj in bodyPrefabs) 
                obj.SetActive(false);

            _index++;

            if (_index >= bodyPrefabs.Count) 
                _index = 0;
            
            bodyPrefabs[_index].transform.position = _lastPosition;
            bodyPrefabs[_index].SetActive(true);
            bodyPrefabs[_index].GetComponent<BlinkingObject>().Activate();
        }
        
        private List<GameObject> GetChildObjects()
        {
            List<GameObject> children = new List<GameObject>();
            foreach (Transform child in transform) 
            {
                children.Add(child.gameObject);
            }
            return children;
        }
    }
}