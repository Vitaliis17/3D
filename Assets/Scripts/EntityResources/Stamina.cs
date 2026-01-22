using UnityEngine;
using System.Collections;

public class Stamina : Resource
{
    private readonly int _returningValue;

    public Stamina(int maxValue, int returningValue) : base(maxValue) 
        => _returningValue = returningValue;

    public IEnumerator Restore()
    {
        WaitForFixedUpdate waiting = new();

        while(IsMax() == false)
        {
            AddValue(_returningValue);

            yield return waiting;
        }
    }
}