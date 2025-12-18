using UnityEngine;

public class GroundGeneratorWithBarrier : MonoBehaviour
{
    public GameObject groundPrefab;
    public int jumlahTilePerArah = 16;
    public float jarakAntarTile = 1f;
    public float penghalangTinggi = 100f;
    public float penghalangLebar = 1f;

    void Start()
    {
        GenerateGround();
    }

    void GenerateGround()
    {
        if (groundPrefab == null) return;

        Vector3 posisiAwal = transform.position;

        // Generate tile tengah
        Instantiate(groundPrefab, posisiAwal, Quaternion.identity);

        // Ke kanan
        for (int i = 1; i <= jumlahTilePerArah; i++)
        {
            Vector3 posKanan = posisiAwal + Vector3.right * i * jarakAntarTile;
            Instantiate(groundPrefab, posKanan, Quaternion.identity);
        }

        // Ke kiri
        for (int i = 1; i <= jumlahTilePerArah; i++)
        {
            Vector3 posKiri = posisiAwal + Vector3.left * i * jarakAntarTile;
            Instantiate(groundPrefab, posKiri, Quaternion.identity);
        }

        // Tambahkan penghalang kiri
        Vector3 posisiPenghalangKiri = posisiAwal + Vector3.left * (jumlahTilePerArah + 1) * jarakAntarTile;
        BuatPenghalangTakTerlihat(posisiPenghalangKiri);

        // Tambahkan penghalang kanan
        Vector3 posisiPenghalangKanan = posisiAwal + Vector3.right * (jumlahTilePerArah + 1) * jarakAntarTile;
        BuatPenghalangTakTerlihat(posisiPenghalangKanan);
    }

    void BuatPenghalangTakTerlihat(Vector3 posisi)
    {
        GameObject barrier = new GameObject("InvisibleBarrier");
        barrier.transform.position = posisi;

        BoxCollider2D collider = barrier.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(penghalangLebar, penghalangTinggi);

        // Tambahkan Rigidbody2D agar tetap statis (opsional)
        Rigidbody2D rb = barrier.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }
}
