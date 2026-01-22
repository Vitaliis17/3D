using System;
using UnityEngine;

public abstract class Resource
{
    private readonly int _minValue;

    private int _maxValue;
    private int _currentValue;

    public event Action<int> MaxValueChanged;
    public event Action<int> CurrentValueChanged;

    public event Action Expired;

    public Resource(int maxValue)
    {
        _minValue = 0;

        SetMax(maxValue);
    }

    public void SubtractValue(int value)
    {
        SetCurrent(_currentValue - value);

        if (IsCurrentExpere())
            Expired?.Invoke();
    }

    public void AddValue(int value)
        => SetCurrent(_currentValue + value);

    public void SetFullValue()
    {
        MaxValueChanged?.Invoke(_maxValue);

        SetCurrent(_maxValue);
    }

    public bool IsCurrentExpere()
        => _currentValue == _minValue;

    protected bool IsMax()
        => _currentValue == _maxValue;

    private void SetCurrent(int value)
    {
        _currentValue = Mathf.Clamp(value, _minValue, _maxValue);
        CurrentValueChanged?.Invoke(_currentValue);
    }

    private void SetMax(int maxValue)
    {
        _maxValue = maxValue;

        MaxValueChanged?.Invoke(_maxValue);
    }
}