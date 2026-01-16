using System;

public class Health
{
    private readonly int _maxValue;
    private readonly int _minValue;

    private int _currentValue;

    public event Action Died;

    public Health(int maxValue)
    {
        _minValue = 0;
        _maxValue = maxValue;
    }

    public void TakeDamage(int damage)
    {
        int nextValue = _currentValue - damage;

        _currentValue = nextValue < _minValue ? _minValue : nextValue;

        if (IsAlive() == false)
            Died?.Invoke();
    }

    public void SetFullValue()
        => _currentValue = _maxValue;

    private bool IsAlive()
        => _currentValue > _minValue;
}