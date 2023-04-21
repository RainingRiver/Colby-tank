using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public int mapSeed;
    public float roomWidth = 50;
    public float roomHeight = 50;
    public Room[,] grid;
    public enum MapType { mapOfTheDay, random, custom };
    public MapType mapType;
    public List<Transform> playerSpawnPoints = new List<Transform>();
    public List<Transform> playerTwoSpawnPoints = new List<Transform>();
    public List<Transform> enemySpawnPoints = new List<Transform>();

    // Start is called before the first frame update
    void Awake()
    {
       mapType = (MapType)GameManager.instance.mapType;
       switch(mapType)
        {
            case MapType.mapOfTheDay:
                mapSeed = DateToInt(DateTime.Now.Date);
                break;

            case MapType.random:
                mapSeed = System.Environment.TickCount;
                break;

            case MapType.custom:
                mapSeed = GameManager.instance.customSeed;
                break;
        }
        GenerateMap();

        // Get all player spawn points
        FindAllPlayerSpawnPoints();

        // Get all player spawn points
        FindAllPlayerTwoSpawnPoints();

        // Get all enemy spawn points
        FindAllEnemySpawnPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Return a random room from our array
    /// </summary>
    /// <returns></returns>
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[Random.Range(0,gridPrefabs.Length)];
    }

    public void GenerateMap()
    {
        //Set Map Seed
        Random.InitState(mapSeed);

        //clear out the grid
        grid = new Room[rows, cols];

        //For each grid row
        for(int currentRow = 0;currentRow < rows;currentRow++) 
        {
            
            //for each colum in that row
            for (int currentCol = 0; currentCol < cols;currentCol++) 
            {
                #region Generation

                float xPosotion = roomWidth* currentCol;
                float zPosotion = roomWidth * currentRow;
                Vector3 newPosotion = new Vector3(xPosotion, 0.0f,zPosotion);
                
                //Create a map tile a that postion
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosotion, Quaternion.identity) as GameObject;

                //Sets the maps tiles parent
                tempRoomObj.transform.parent = transform;

                //Give it a meaningful name
                tempRoomObj.name = "Room " + currentCol + ", " + currentRow;

                //Get room object reference
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                //Save to Grid array
                grid[currentRow,currentCol] = tempRoom;

                #endregion Generation

                #region Door
                //open the doors
                //If we're on the bottom row,open the north door
                if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1) 
                { 
                    //otherwise, if we're on the top row, open the south door
                    tempRoom.doorSouth.SetActive(false);
                }
                else
                {
                    tempRoom.doorNorth.SetActive(false);
                    tempRoom.doorSouth.SetActive(false);
                }
                if (currentCol == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (currentCol == rows - 1)
                {
                    //otherwise, if we're on the top row, open the south door
                    tempRoom.doorWest.SetActive(false);
                }
                else
                {
                    tempRoom.doorEast.SetActive(false);
                    tempRoom.doorWest.SetActive(false);
                }
                #endregion Door
            }
            


        }
    }

    public int DateToInt(DateTime dateToUse)
    {
        return dateToUse.Year + dateToUse.Month+ dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    // Find all objects with the player spawn point script
    public void FindAllPlayerSpawnPoints()
    {
        PlayerSpawnPoint[] PlayerSpawnPoints = FindObjectsOfType<PlayerSpawnPoint>();
        foreach (PlayerSpawnPoint PlayerSpawnPoint in PlayerSpawnPoints)
        {
            // Add its transform to spawnPoints list
            playerSpawnPoints.Add(PlayerSpawnPoint.transform);
        }
    }
    public void FindAllPlayerTwoSpawnPoints()
    {
        PlayerTwoSpawnPoint[] PlayerTwoSpawnPoints = FindObjectsOfType<PlayerTwoSpawnPoint>();
        foreach (PlayerTwoSpawnPoint PlayerTwoSpawnPoint in PlayerTwoSpawnPoints)
        {
            // Add its transform to spawnPoints list
            playerSpawnPoints.Add(PlayerTwoSpawnPoint.transform);
        }
    }

    // Find all enemy spawn places
    public void FindAllEnemySpawnPoints()
    {
        EnemySpawnPoint[] EnemySpawnPoints = FindObjectsOfType<EnemySpawnPoint>();
        foreach (EnemySpawnPoint EnemySpawnPoint in EnemySpawnPoints)
        {
            // Add the transform to list
            enemySpawnPoints.Add(EnemySpawnPoint.transform);
        }
    }
}
