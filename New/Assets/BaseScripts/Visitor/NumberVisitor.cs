using BaseScripts.Factory.NumberFactory;
using BaseScripts.PlayerMovement;
using UnityEngine;

namespace BaseScripts.Visitor
{
    public class NumberVisitor : INumberVisitor
    {
        private PlayerData _playerData;

        public NumberVisitor(PlayerData playerData)
        {
            _playerData = playerData;
        }

        public int Score { get; private set; }
        
        public void Visit(OneNumber oneNumber) => 
            ShowScore();

        public void Visit(ZeroNumber zeroNumber) => 
            ShowScore();

        private void ShowScore() => 
            Debug.Log("Do Nothing");
    }
}