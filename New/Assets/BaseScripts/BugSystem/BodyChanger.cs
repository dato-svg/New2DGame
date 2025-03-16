using System.Collections.Generic;
using UnityEngine;

namespace BaseScripts.BugSystem
{
    public class BodyChanger : MonoBehaviour
    {
        [SerializeField] private List<GameObject> bodyPrefabs;


        private void Awake()
        {
            ActiveRandomObject();
        }
        
        
        [ContextMenu("ActiveRandomObject")]
        public void ActiveRandomObject()
        {
            Debug.Log("ActiveRandomObject");
            foreach (var obj in bodyPrefabs) 
                obj.SetActive(false);
            
            int index = Random.Range(0, bodyPrefabs.Count);
            bodyPrefabs[index].SetActive(true);
        }
    }
}
