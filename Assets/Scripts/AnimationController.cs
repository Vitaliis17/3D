using UnityEngine;

public class AnimationController
{
    private readonly PlayerAnimationData _data;
    private readonly Animator _animator;

    private int _currentParameter;

    public AnimationController(Animator animator, PlayerAnimationData data)
    {
        _animator = animator;
        _data = data;
        PlayDefault();
    }

    public void PlayWalking()
    {
        Debug.Log("WalkAnim");
        SwitchBool(_data.IsWalking);
        _animator.Play(_data.Walking);
    }

    public void PlayRunning()
    {
        SwitchBool(_data.IsRunning);
        _animator.Play(_data.Running);
    }

    public void PlayIdle()
    {
        SwitchBool(_data.IsIdle);
        _animator.Play(_data.Idle);
    }

    private void PlayDefault()
    {
        _currentParameter = _data.IsIdle;

        _animator.SetBool(_currentParameter, true);
        _animator.Play(_data.Idle);
    }

    private void SwitchBool(int parameterHash)
    {
        _animator.SetBool(_currentParameter, false);
        
        _currentParameter = parameterHash;
        _animator.SetBool(_currentParameter, true);
    }
}