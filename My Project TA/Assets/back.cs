using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    [Header("Nama Scene yang ingin dipilih")]
    [SerializeField] private string menu; // Ganti dengan nama scene

    // Fungsi untuk memuat scene berdasarkan nama (string)
    public void LoadSelectedScene()
    {
        if (!string.IsNullOrEmpty(menu))
        {
            SceneManager.LoadScene(menu);
        }
        else
        {
            Debug.LogWarning("Scene belum dipilih atau nama kosong!");
        }
    }
}
