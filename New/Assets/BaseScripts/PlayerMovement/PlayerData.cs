using System;
using UnityEngine;

namespace BaseScripts.PlayerMovement
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
    public class PlayerData :  ScriptableObject
    {
        [SerializeField,Range(0,100f)] private float mindScore = 100;
        [field:SerializeField,Range(1,15)]  public float Speed { get; private set; } = 5f;
        [field:SerializeField,Range(1,10)] public float JumpForce { get; private set; } = 10f;
        [field:SerializeField] public bool IsGrounded { get; set; } = false;
        [field:SerializeField,Range(0,1.2f)] public float GroundCheckerDistance { get; set; } = 0.479f;
        [field:SerializeField,Range(0,1.2f)] public float GroundCheckerRadius {get; set;} = 0.67f;
        
        public float MindScore
        {
            get => mindScore;
            set
            {
                if (mindScore < 0)
                {
                    throw new ArgumentOutOfRangeException("mindScore");
                    mindScore = 0;
                }
                
                mindScore = value;
            }
        }
        
        
        public void ResetParameter()
        {
            // TODO -- ADD RESET
        }
        
    }
}
