using UnityEngine;
using System.Collections.Generic;

public class FloatingPlatformFromGround : MonoBehaviour
{
    // Prefab untuk platform biasa dan prefab yang hanya di-spawn satu kali
    public GameObject platformPrefab;
    public GameObject singleUsePrefab;  // Prefab tambahan yang hanya di-spawn satu kali
    public GroundGeneratorWithBarrier groundSource;

    // Pengaturan platform biasa
    public int jumlahPlatform = 10;
    public float platformWidthInTile = 3f;
    public float verticalSpacing = 2f;

    // Pengaturan ketinggian platform biasa
    [Header("Ketinggian (Y) random platform")]
    public float minY = 2f;
    public float maxY = 8f;

    // Pengaturan ketinggian untuk single-use prefab
    [Header("Ketinggian untuk single-use prefab")]
    public float singlePrefabHeight = 5f; // Ketinggian untuk prefab yang hanya di-spawn satu kali

    // Menyimpan posisi platform yang sudah di-spawn
    public List<Vector3> platformPositions = new List<Vector3>();

    private List<Vector2> usedSlots = new List<Vector2>();
    private bool singlePrefabSpawned = false;  // Menandai apakah singleUsePrefab sudah di-spawn

    void Start()
    {
        GeneratePlatforms();
    }

    void GeneratePlatforms()
    {
        // Pastikan prefab dan groundSource ada
        if (platformPrefab == null || groundSource == null) return;

        // Menghitung ukuran tile berdasarkan groundSource
        float tileSize = groundSource.jarakAntarTile;
        int totalTileCount = groundSource.jumlahTilePerArah * 2 + 1;
        float groundStartX = groundSource.transform.position.x - groundSource.jumlahTilePerArah * tileSize;

        // Menghitung jumlah slot untuk platform
        int slotCount = Mathf.FloorToInt(totalTileCount / platformWidthInTile);

        int maxAttempts = jumlahPlatform * 10;
        int attempts = 0;

        // Generate platform biasa
        while (platformPositions.Count < jumlahPlatform && attempts < maxAttempts)
        {
            int slotIndex = Random.Range(0, slotCount);
            float x = groundStartX + (slotIndex * platformWidthInTile + platformWidthInTile / 2f) * tileSize;
            float y = Random.Range(minY, maxY);

            Vector2 newSlot = new Vector2(slotIndex, Mathf.Round(y / verticalSpacing));

            if (!usedSlots.Contains(newSlot))
            {
                Vector3 spawnPos = new Vector3(x, y, 0);
                Instantiate(platformPrefab, spawnPos, Quaternion.identity);
                usedSlots.Add(newSlot);
                platformPositions.Add(spawnPos); // Simpan posisi platform
            }

            attempts++;
        }

        // Jika singleUsePrefab belum di-spawn, spawn satu kali di posisi tertentu
        if (!singlePrefabSpawned && singleUsePrefab != null)
        {
            Vector3 singlePrefabPosition = new Vector3(0, singlePrefabHeight, 0);  // Gunakan singlePrefabHeight untuk ketinggian
            Instantiate(singleUsePrefab, singlePrefabPosition, Quaternion.identity);
            singlePrefabSpawned = true;  // Tandai prefab telah di-spawn
        }
    }
}
