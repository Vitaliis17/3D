using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthView : MonoBehaviour
{
    private Slider _slider;

    private void Awake()
        => _slider = GetComponent<Slider>();

    public void SetCurrent(int value)
        => _slider.value = value;

    public void SetMax(int value)
        => _slider.maxValue = value;
}
