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
        [SerializeField] private Transform spawnPoints;
        [SerializeField] private float spawnCooldown;
        [SerializeField] private NumberPool pool;

        [SerializeField] private List<Number> enemyNumber; // TODO -- CHANGE VISIBLE
        
        
        private Character _character;
        private Coroutine _spawnCoroutine;
        private NumberVisitor visitor;

        private void Awake() =>
            _character = FindObjectOfType<Character>(); //  BEST SOLUTION :)

        private void Start()
        {
            pool = FindObjectOfType<NumberPool>(); //  :(
            visitor = new NumberVisitor(_character.playerData);
        }
           


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

           

            enemyNumber.Remove(removedNumber);
        }

        public void KillCurrentNumber(Number number, NumberType type)
        {
            enemyNumber.Remove(number);
            pool.ReturnNumber(number, type);
        }

        public void TouchPlayerNumber(Number number)
        {
            enemyNumber.Remove(number);
            Destroy(number.gameObject);
            number.Accept(visitor); 
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
                
                Number enemy = pool.GetNumber(type);
                enemy.Initialize(this);
                enemyNumber.Add(enemy);
                enemy.MoveTo(spawnPoints.position);
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