using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    private GameObject playerInstance;
    public Grid gridPrefab;
    private Grid gridInstance;

    void Start()
    {
        gridInstance = Instantiate(gridPrefab, Vector3.zero, Quaternion.identity);
        gridInstance.GenerateLevel();
        playerInstance = Instantiate(player, Vector3.zero, Quaternion.identity);
        SpawnPlayer();
    }

    void Update()
    {
        if(gridInstance.end)
        {
            gridInstance.DeleteLevel();
            if(gridInstance.CellCount() == 0)
            {
                gridInstance.GenerateLevel();
                SpawnPlayer();
                gridInstance.end = false;
            }
        }
    }

    private void SpawnPlayer()
    {
        Cell spawn = gridInstance.GetPlayerSpawnCell();
        Vector2 coords = spawn.GetCoordinates();
        playerInstance.transform.position = new Vector2(coords.x, coords.y + 1);
    }
}
