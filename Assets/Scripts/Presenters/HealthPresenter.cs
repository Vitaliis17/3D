using UnityEngine;

public class HealthPresenter : MonoBehaviour 
{
    [SerializeField] private Entity _entity;
    [SerializeField] private BarView _view;

    private void OnEnable()
    {
        _entity.Health.MaxValueChanged += _view.SetMax;
        _entity.Health.CurrentValueChanged += _view.SetCurrent;
    }

    private void OnDisable()
    {
        _entity.Health.MaxValueChanged -= _view.SetMax;
        _entity.Health.CurrentValueChanged -= _view.SetCurrent;
    }
}