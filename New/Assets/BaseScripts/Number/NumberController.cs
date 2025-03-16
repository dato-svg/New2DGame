using System.Collections.Generic;
using BaseScripts.Factory.NumberFactory;
using UnityEngine;

namespace BaseScripts.Number
{
    public class NumberController : MonoBehaviour
    {
        [SerializeField] private List<NumberSpawner>  numberSpawners;

        [SerializeField] private bool activeSpawner;
        private void Start()
        {
            if (activeSpawner)
            {
                foreach (var spawner in numberSpawners) 
                    spawner.StartSpawn();
            }
        }
        
        
    }
}
