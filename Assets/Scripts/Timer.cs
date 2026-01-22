using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private readonly float _minTime;
    private readonly float _maxTime;
    
    private float _currentTime;

    public event Action TimeOvered;
    public event Action CurrentTimeOvered;
    public event Action StartingWait;

    public Timer(float maxTime = 0f)
    {
        _minTime = 0f;

        _maxTime = maxTime;
        _currentTime = _maxTime;
    }

    public IEnumerator Wait()
    {
        StartingWait?.Invoke();

        float stepTime = Time.fixedDeltaTime;
        WaitForSeconds waiting = new(stepTime);
        Debug.Log("Wait " + _currentTime);
        while(_currentTime > _minTime)
        {
            Debug.Log(_currentTime);
            _currentTime -= stepTime;

            yield return waiting;
        }

        CurrentTimeOvered?.Invoke();
    }

    public IEnumerator ReplenishTime()
    {
        CurrentTimeOvered?.Invoke();
        float stepTime = Time.fixedDeltaTime;
        WaitForSeconds waiting = new(stepTime);

        while(_currentTime < _maxTime)
        {
            Debug.Log(_currentTime);
            _currentTime += stepTime;

            yield return waiting;
        }

        _currentTime = _maxTime;
    }

    public IEnumerator Wait(float time)
    {
        StartingWait?.Invoke();

        WaitForSeconds waiting = new(time);

        yield return waiting;

        TimeOvered?.Invoke();
    }
}