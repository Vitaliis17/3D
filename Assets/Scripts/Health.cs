using System;

public class Health
{
    private readonly int _minValue;
    
    private int _maxValue;
    private int _currentValue;

    public event Action<int> MaxValueChanged;
    public event Action<int> CurrentValueChanged;

    public event Action Died;

    public Health(int maxValue)
    {
        _minValue = 0;

        SetMax(maxValue);
    }

    public void TakeDamage(int damage)
    {
        SetCurrent(_currentValue - damage);

        if (IsAlive() == false)
            Died?.Invoke();
    }

    public void SetFullValue()
    {
        MaxValueChanged?.Invoke(_maxValue);
        SetCurrent(_maxValue);
    }

        private bool IsAlive()
        => _currentValue > _minValue;

    private void SetCurrent(int value)
    {
        _currentValue = value < _minValue ? _minValue : value;

        CurrentValueChanged?.Invoke(_currentValue);
    }

    private void SetMax(int maxValue)
    {
        _maxValue = maxValue;

        MaxValueChanged?.Invoke(_maxValue);
    }
}