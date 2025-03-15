using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BaseScripts.Factory.NumberFactory
{
    [CreateAssetMenu(menuName = "NumberFactory" , fileName = "Number/FactoryNumber")]
    public class NumberFactory : ScriptableObject
    { 
        [SerializeField] private ZeroNumber zeroNumber;
        [SerializeField] private OneNumber oneNumber;
        
        public Number Create(NumberType type)
        {
            switch (type)
            {
                case NumberType.Zero:
                    return Instantiate(zeroNumber);
                    break;
                case NumberType.One:
                    return Instantiate(oneNumber);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}