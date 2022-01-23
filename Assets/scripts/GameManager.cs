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
    public GameObject coin;
    public List<GameObject> weapons;
    public List<GameObject> ennemies;
    public int spawnCoinDelayInSeconds;
    public int spawnWeaponDelayInSeconds;
    public int spawnEnnemyDelayInSeconds;

    private List<Cell> spawns;


    void Start()
    {
        gridInstance = Instantiate(gridPrefab, Vector3.zero, Quaternion.identity);
        gridInstance.GenerateLevel();
        spawns = gridInstance.GetPossibleSpawns();
        InvokeStart();
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
                CancelInvoke();

                gridInstance.GenerateLevel();
                spawns = gridInstance.GetPossibleSpawns();
                InvokeStart();
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

    private Cell GetRandomSpawn()
    {
        return spawns.ElementAt(Random.Range(0, spawns.Count));
    }

    private void InvokeStart()
    {
        InvokeRepeating("SpawnCoin", 0f, spawnCoinDelayInSeconds);
        InvokeRepeating("SpawnWeapon", 0f, spawnWeaponDelayInSeconds);
        InvokeRepeating("SpawnEnnemy", 0f, spawnEnnemyDelayInSeconds);
    }

    private void SpawnCoin()
    {
        Cell spawn = GetRandomSpawn();
        Vector2Int coords = spawn.GetCoordinates();

        GameObject newCoin = Instantiate(coin, new Vector3(coords.x, coords.y + 1, 0), Quaternion.identity);
        newCoin.transform.SetParent(transform);
    }

    private void SpawnWeapon()
    {
        Cell spawn = GetRandomSpawn();
        Vector2Int coords = spawn.GetCoordinates();

        GameObject weapon = weapons.ElementAt(Random.Range(0, weapons.Count));
        GameObject newWeapon = Instantiate(weapon, new Vector3(coords.x, coords.y + 1, 0), Quaternion.identity);
        newWeapon.transform.SetParent(transform);
    }

    private void SpawnEnnemy()
    {
        Cell spawn = GetRandomSpawn();
        Vector2Int coords = spawn.GetCoordinates();

        GameObject ennemy = ennemies.ElementAt(Random.Range(0, ennemies.Count));
        GameObject newEnnemy = Instantiate(ennemy, new Vector3(coords.x, coords.y + 1, 0), Quaternion.identity);
        newEnnemy.transform.SetParent(transform);
    }
}
