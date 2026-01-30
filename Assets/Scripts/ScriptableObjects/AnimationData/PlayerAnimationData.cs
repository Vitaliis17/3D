using UnityEngine;

[CreateAssetMenu(fileName = "New Player Animation Data", menuName = "AnimaitonData/Player")]
public class PlayerAnimationData : ScriptableObject
{
    public int Walking { get; private set; }
    public int Running { get; private set; }
    public int Idle { get; private set; }

    public int IsWalking { get; private set; }
    public int IsRunning { get; private set; }
    public int IsIdle { get; private set; }

    private void Awake()
    {
        Walking = Animator.StringToHash(nameof(Walking));
        Running = Animator.StringToHash(nameof(Running));
        Idle = Animator.StringToHash(nameof(Idle));

        IsWalking = Animator.StringToHash(nameof(IsWalking));
        IsRunning = Animator.StringToHash(nameof(IsRunning));
        IsIdle = Animator.StringToHash(nameof(IsIdle));
    }
}