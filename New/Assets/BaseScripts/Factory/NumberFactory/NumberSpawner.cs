using System;
using System.Collections;
using System.Collections.Generic;
using BaseScripts.PlayerMovement;
using BaseScripts.Visitor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BaseScripts.Factory.NumberFactory
{
    public class NumberSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> spawnPoints;
        [SerializeField] private float spawnCooldown;
        [SerializeField] private NumberFactory factory;
        
        [SerializeField] private List<Number> enemyNumber; // TODO -- CHANGE VISIBLE

        private Character _character;
        private Coroutine _spawnCoroutine;
        private NumberVisitor  visitor;
        
        private void Awake() => 
            _character = FindObjectOfType<Character>(); //  BEST SOLUTION :)

        private void Start() => 
            visitor = new NumberVisitor(_character.playerData);

        
        [ContextMenu("KillRandomNumber")]
        public void KillRandomNumber()
        {
            if (enemyNumber.Count == 0)
            {
                Debug.LogWarning("Нет объектов для удаления!");
                return;
            }
            
            int randomIndex = Random.Range(0, enemyNumber.Count);
            Number removedNumber = enemyNumber[randomIndex];
            
            removedNumber.Accept(visitor); // TODO -- Call Method when Player touches numbers -- not here
            
            enemyNumber.Remove(removedNumber);
        }
        
        
        [ContextMenu("StartSpawn")]
        public void StartSpawn()
        {
            StopSpawn();
            _spawnCoroutine = StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            while (true)
            {
                NumberType type = (NumberType)Random.Range(0,Enum.GetValues(typeof(NumberType)).Length);
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                
                Number enemy = factory.Create(type);
                enemyNumber.Add(enemy);
                enemy.MoveTo(spawnPoint.position);
                yield return new WaitForSeconds(spawnCooldown);
            }
            // ReSharper disable once IteratorNeverReturns
        }

        [ContextMenu("StopSpawn")]
        public void StopSpawn()
        {
            if (_spawnCoroutine == null)
                return;
            
            StopCoroutine(_spawnCoroutine);
        }
        
    }
}