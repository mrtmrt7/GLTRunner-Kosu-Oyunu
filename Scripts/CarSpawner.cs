using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] carPrefabs;
    [SerializeField] private GameObject[] barikatPrefabs; 
[SerializeField] private GameObject[] coins;
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 3f;
    [SerializeField] private float coinsSpawnMin = 3f;
    [SerializeField] private float coinsSpawnMax = 10f;
    [SerializeField] private float barikatSpawnMin = 1f; 
    [SerializeField] private float barikatSpawnMax = 3f;
    
    [SerializeField] private KarakterKontrol _karakterKontrol;

    void Start()
    {
        StartCoroutine(SpawnCars());
        StartCoroutine(SpawnCoins());
        StartCoroutine(SpawnBarikats()); 
    }
    IEnumerator SpawnCars()
    {
        while (_karakterKontrol.isAlive)
        {
            float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomTime);

            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
        }
    }
    IEnumerator SpawnCoins()
    {
        while (_karakterKontrol.isAlive)
        {
            float randomTime = Random.Range(coinsSpawnMin, coinsSpawnMax);
            yield return new WaitForSeconds(randomTime);

            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];
            Vector3 yukseklik = new Vector3(0f, 0.2f, 0f);
            Instantiate(coins[0], spawnPoint.position + yukseklik, spawnPoint.rotation);
        }
    }
    IEnumerator SpawnBarikats()
    {
        while (_karakterKontrol.isAlive)
        {
            float randomTime = Random.Range(barikatSpawnMin, barikatSpawnMax);
            yield return new WaitForSeconds(randomTime);

            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];      
            Vector3 yukariKaydirma = new Vector3(0f, 0.5f, 0f);
            Vector3 ileriPozisyon = spawnPoint.position + new Vector3(0f, 0f, 2f);
            Instantiate(barikatPrefabs[0], ileriPozisyon, spawnPoint.rotation);
        }
    }
}