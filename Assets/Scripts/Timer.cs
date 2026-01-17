using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    public event Action TimeOvered;

    public IEnumerator StartTimer(float time)
    {
        WaitForSeconds waiting = new(time);

        yield return waiting;

        TimeOvered?.Invoke();
    }
}