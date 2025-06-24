using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Roads = new List<GameObject>();
    [SerializeField] private Transform playerPrefab;
    [SerializeField] Transform carSpawn;
    private float previousPlayerZ;
    [SerializeField] private int visibleRoadCount = 12; 

    private float roadLength = 2.80f;
    private float spawnZ = 0f;
    private List<GameObject> activeRoads = new List<GameObject>();

    void Start()
    {
        
        for (int i = 0; i < visibleRoadCount; i++)
        {
            CreateRoad(i == 0 ? 0 : Random.Range(0, Roads.Count)); 
        }
    }
    void Update()
    {    
        if (playerPrefab.position.z > spawnZ - (visibleRoadCount * roadLength))
        {
            CreateRoad(Random.Range(0, Roads.Count));
        }
    }
   private void FixedUpdate()
    {
        float deltaZ= playerPrefab.position.z-previousPlayerZ;

        carSpawn.position+= new Vector3(0,0,deltaZ); // Araba spawn noktasını oyuncuyla birlikte ileriye taşıyoruz
        previousPlayerZ=playerPrefab.position.z;
    }
    void CreateRoad(int index)
    {
        GameObject newRoad = Instantiate(Roads[index], Vector3.forward * spawnZ, Quaternion.identity);
        activeRoads.Add(newRoad);
        spawnZ += roadLength;
    }

    public void RestartGame(){

        SceneManager.LoadScene(1);
    }

    public void BackToMenu(){
        SceneManager.LoadScene(0);
    }
}
