using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public void StartRun()
    {
        animator.SetBool("Run", true);
    }
    public void StopRun()
    {
        animator.SetBool("Run", false);
    }

}
