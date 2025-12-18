using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExtraSpawner : MonoBehaviour
{
    [Header("Script Generator Platform")]
    public FloatingPlatformFromGround platformGenerator;

    [Header("Prefab List (1 prefab untuk 1 platform)")]
    public List<GameObject> extraPrefabs;

    [Header("Offset Posisi")]
    public float verticalOffset = 1.5f;

    void Start()
    {
        StartCoroutine(WaitForPlatformAndSpawn());
    }

    IEnumerator WaitForPlatformAndSpawn()
    {
        // Tunggu sampai platformPositions tidak kosong
        while (platformGenerator.platformPositions.Count == 0)
        {
            yield return null; // tunggu 1 frame
        }

        SpawnExtras();
    }

    void SpawnExtras()
    {
        List<Vector3> platformPositions = platformGenerator.platformPositions;

        int jumlahPlatform = Mathf.Min(platformPositions.Count, extraPrefabs.Count);

        for (int i = 0; i < jumlahPlatform; i++)
        {
            if (extraPrefabs[i] == null)
            {
                Debug.LogWarning($"Prefab ke-{i} kosong/null.");
                continue;
            }

            Vector3 spawnPos = platformPositions[i] + Vector3.up * verticalOffset;
            Instantiate(extraPrefabs[i], spawnPos, Quaternion.identity);
        }

        if (extraPrefabs.Count > jumlahPlatform)
            Debug.LogWarning("Prefab lebih banyak dari platform, sisa prefab tidak dipakai.");
        else if (platformPositions.Count > extraPrefabs.Count)
            Debug.LogWarning("Platform lebih banyak dari prefab, sisa platform tidak dapat spawn extra.");
    }
}
