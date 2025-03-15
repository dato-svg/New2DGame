using System;
using UnityEngine;

namespace BaseScripts.Factory.NumberFactory
{
    [CreateAssetMenu(menuName = "NumberFactory" , fileName = "Number/FactoryNumber")]
    public class NumberFactory : ScriptableObject
    {
        public ZeroNumber ZeroNumber;
        public OneNumber OneNumber;
        
        public Number Create(NumberType type)
        {
            switch (type)
            {
                case NumberType.Zero:
                    return Instantiate(ZeroNumber);
                    break;
                case NumberType.One:
                    return Instantiate(OneNumber);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}