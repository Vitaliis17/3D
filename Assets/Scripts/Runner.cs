using UnityEngine;
using System.Collections;

public class Runner
{
    private readonly int _wastingStamina;

    public Runner(int wastingStamina, float speedMultiplier, float maxTime)
    {
        _wastingStamina = wastingStamina;

        SpeedMultiplier = speedMultiplier;
        DeactivateRunning();
    }

    public float SpeedMultiplier { get; private set; }
    public bool IsRunning { get; private set; }

    public void DeactivateRunning()
        => IsRunning = false;

    public IEnumerator WasteStamina(Stamina stamina)
    {
        IsRunning = true;

        WaitForFixedUpdate waiting = new();

        while (stamina.IsCurrentExpere() == false)
        {
            stamina.SubtractValue(_wastingStamina);

            yield return waiting;
        }

        DeactivateRunning();
    }
}