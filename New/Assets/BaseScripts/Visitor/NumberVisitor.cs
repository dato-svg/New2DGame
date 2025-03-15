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
        
        public void Visit(OneNumber oneNumber)
        {
            _playerData.MindScore -= 1;
            ShowScore();
        }

        public void Visit(ZeroNumber zeroNumber)
        {
            _playerData.MindScore -= 2;
            ShowScore();
        }

        private void ShowScore() => 
            Debug.Log("MindScore : " + _playerData.MindScore);
    }
}