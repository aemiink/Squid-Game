using UnityEngine;
public class NPCSpawner : MonoBehaviour
{
    public GameObject npcPrefab1;  // 1. NPC prefab'ı
    public GameObject npcPrefab2;  // 2. NPC prefab'ı
    public int npcCount = 50;      // Oluşturulacak NPC sayısı
    public float spawnRange = 10f; // NPC'lerin konumlanacağı rastgele alanın yarıçapı

    void Start()
    {
        SpawnNPCs();
    }

    // NPC'leri rastgele oluşturmak için fonksiyon
    void SpawnNPCs()
    {
        for (int i = 0; i < npcCount; i++)
        {
            // Rastgele bir pozisyon seç
            float randomX = Random.Range(-spawnRange, spawnRange);
            float randomZ = Random.Range(-spawnRange, spawnRange);
            Vector3 spawnPosition = new Vector3(randomX, 0f, randomZ); // Yükseklik 0 (sahnenin zemininde)

            // Rastgele bir prefab seç
            GameObject selectedPrefab = (Random.Range(0, 2) == 0) ? npcPrefab1 : npcPrefab2;

            // NPC'yi oluştur ve pozisyona yerleştir
            Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
