using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneSelector : MonoBehaviour
{
    [SerializeField] private SceneAsset sceneToLoad;  // ini yang bisa dipilih dari Inspector

    private string sceneName;

    private void Awake()
    {
#if UNITY_EDITOR
        if (sceneToLoad != null)
        {
            sceneName = sceneToLoad.name;
        }
#endif
    }

    public void LoadSelectedScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("Scene belum dipilih atau nama kosong!");
        }
    }
}
