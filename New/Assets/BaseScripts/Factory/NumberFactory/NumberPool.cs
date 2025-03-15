using System.Collections.Generic;
using UnityEngine;

namespace BaseScripts.Factory.NumberFactory
{
    public class NumberPool : MonoBehaviour
    {
        [SerializeField] private NumberFactory factory;
        [SerializeField] private int poolSize = 10;
        
        private readonly Dictionary<NumberType, Queue<Number>> _pool = new();
        
        private void Awake()
        {
            foreach (NumberType type in System.Enum.GetValues(typeof(NumberType)))
            {
                _pool[type] = new Queue<Number>();
                for (int i = 0; i < poolSize; i++)
                {
                    Number number = factory.Create(type);
                    number.gameObject.SetActive(false);
                    _pool[type].Enqueue(number);
                }
            }
        }

        public Number GetNumber(NumberType type)
        {
            if (_pool[type].Count > 0)
            {
                Number number = _pool[type].Dequeue();
                number.gameObject.SetActive(true);
                return number;
            }
            else
            {
                Number number = factory.Create(type);
                number.gameObject.SetActive(true);
                return number;
            }
        }

        public void ReturnNumber(Number number, NumberType type)
        {
            number.gameObject.SetActive(false);
            _pool[type].Enqueue(number);
        }
    }
}