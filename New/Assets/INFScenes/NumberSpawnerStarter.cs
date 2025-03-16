using BaseScripts.Factory.NumberFactory;
using UnityEngine;


[RequireComponent(typeof(NumberSpawner))]
public class NumberSpawnerStarter : MonoBehaviour
{
    private void Start()
    {
        GetComponent<NumberSpawner>().StartSpawn();
    }
}
