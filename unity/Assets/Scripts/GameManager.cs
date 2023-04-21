using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static GameManager instance;

    //Prefabs
    //tank 1
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public Transform playerSpawnTransform;
    public List<PlayerController> players;

    //tank 2
    public GameObject tankPawnTwoPrefab;
    public Transform playerTwoSpawnTransform;
    public List<PlayerController> playerTwo;

    //AI
    public bool doSpawn = true;
    public List<GameObject> aiTankPawnPrefabs = new List<GameObject>();
    public Transform aiSpawnTransform;
    public List<AiController> ai;

    public bool doGen = true;
    public GameObject mapGeneratorPrefab;
    public enum MapType { mapOfTheDay, random, custom };
    public MapType mapType;
    public int customSeed = 0;
    public MapGenerator mapGenerator;
    #endregion Variables

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        players= new List<PlayerController>();

        playerTwo= new List<PlayerController>();

        ai= new List<AiController>();
    }

    // Update is called once per frame
    void Start()
    {
        SpawnPlayer();
        SpawnPlayerTwo();
        SpawnEnemy();
    }

    public void SpawnPlayer()
    {
        // Check if player transform is null, then randomly set a spawn
        if (playerSpawnTransform == null)
        {

            // Get a random player spawn transform from the map generator
            Transform spawnPoint = mapGenerator.playerSpawnPoints[Random.Range(0, mapGenerator.playerSpawnPoints.Count)];
            playerSpawnTransform = spawnPoint;
        }

        // Then spawn player
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }

    public void SpawnPlayerTwo()
    {
        // Check if player transform is null, then randomly set a spawn
        if (playerTwoSpawnTransform == null)
        {

            // Get a random player spawn transform from the map generator
            Transform spawnPoint = mapGenerator.playerTwoSpawnPoints[Random.Range(0, mapGenerator.playerTwoSpawnPoints.Count)];
            playerTwoSpawnTransform = spawnPoint;
        }

        // Then spawn player
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerTwoSpawnTransform.position, playerTwoSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }

    public void SpawnEnemy()
    {
        if (doSpawn == true)
        {
            if (aiSpawnTransform == null)
            {
                // Keep spawning enemies for the amount of spawns for them
                for (int i = 0; i < mapGenerator.enemySpawnPoints.Count; i++)
                {
                    // Get a random player spawn transform from the map generator
                    Transform spawnPoint = mapGenerator.enemySpawnPoints[Random.Range(0, mapGenerator.enemySpawnPoints.Count)];
                    aiSpawnTransform = spawnPoint;

                    GameObject newAiPawn = Instantiate(aiTankPawnPrefabs[Random.Range(0, aiTankPawnPrefabs.Count)], aiSpawnTransform.position, Quaternion.identity);
                }
            }
        }
    }
}
