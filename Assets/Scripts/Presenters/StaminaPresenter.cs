using UnityEngine;

public class StaminaPresenter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private BarView _view;

    private void OnEnable()
    {
        _player.Stamina.MaxValueChanged += _view.SetMax;
        _player.Stamina.CurrentValueChanged += _view.SetCurrent;
    }

    private void OnDisable()
    {
        _player.Stamina.MaxValueChanged -= _view.SetMax;
        _player.Stamina.CurrentValueChanged -= _view.SetCurrent;
    }
}