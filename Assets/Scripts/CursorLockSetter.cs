using UnityEngine;

public class CursorLockSetter : MonoBehaviour
{
    private void Awake()
        => Cursor.lockState = CursorLockMode.Locked;
}