using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Scenes _nextScene;

    public void Load()
        => SceneManager.LoadSceneAsync((int)_nextScene);
}
