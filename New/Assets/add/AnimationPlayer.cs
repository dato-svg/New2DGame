using UnityEngine;

namespace add
{
    public class AnimationPlayer 
    {
        private static readonly int Run = Animator.StringToHash("run");
        private static readonly int Jump = Animator.StringToHash("jump");
        
         private readonly Animator _animator;

        public AnimationPlayer(Animator animator) => 
            _animator = animator;

        public void StartRun() => 
            _animator.SetBool(Run, true);

        public void StopRun() => 
            _animator.SetBool(Run, false);
        
        public void StartJump() => 
            _animator.SetTrigger(Jump);
    }
}
