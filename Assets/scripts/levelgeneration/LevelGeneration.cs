using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    public int[] openingDirection= new int[4];
    public Transform[] doors;
    // 1 --> need bottom door
    // 2 --> need top door
    // 3 --> need left door
    // 4 --> need right door

    [Space]
    private roomTemplates templates;
    public int entranceDirection;
    private int rand;
    public bool spawned = false;

    public float waitTime = 4f;
    [Space]
    public LayerMask roomLayer;

    void Start()
    {
        
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<roomTemplates>();
        checkIfCanSpawn();
    }

    void checkIfCanSpawn()
    {
        
        //if can go this direction
        if (CorrectOpeningDirection(1)&&!CorrectOpeningDirection(entranceDirection))
        {
            if (Physics2D.OverlapCircle(doors[0].position, 0.5f, roomLayer))
            {
                //but there is a room in the way
                print("wow");
            }
        }
        //if can go this direction
        if (CorrectOpeningDirection(2) && !CorrectOpeningDirection(entranceDirection))
        {
            if (Physics2D.OverlapCircle(doors[1].position, 0.5f, roomLayer))
            {
                //but there is a room in the way
                print("wow");
            }
        }
        //if can go this direction
        if (CorrectOpeningDirection(3) && !CorrectOpeningDirection(entranceDirection))
        {
            if (Physics2D.OverlapCircle(doors[2].position, 0.5f, roomLayer))
            {
                //but there is a room in the way
                print("wow");
            }
        }
        //if can go this direction
        if (CorrectOpeningDirection(4) && !CorrectOpeningDirection(entranceDirection))
        {
            if (Physics2D.OverlapCircle(doors[3].position, 0.5f, roomLayer))
            {
                //but there is a room in the way
                print("wow");
            }
        }
        //if there are 2 or more bad rooms
        
    }


    void SpawnRooms(bool reset)
    {
        //spawn Bottom
        if (CorrectOpeningDirection(1)&&!Physics2D.OverlapCircle(doors[0].position,0.5f,roomLayer))
        {
            BottomRoom(reset);
        }
        //spawn Top
        if (CorrectOpeningDirection(2) && !Physics2D.OverlapCircle(doors[1].position, 0.5f, roomLayer))
        {
            TopRoom(reset);
        }
        //spawn left
        if (CorrectOpeningDirection(3) && !Physics2D.OverlapCircle(doors[2].position, 0.5f, roomLayer))
        {
            LeftRoom(reset);
        }
        //spawn right
        if (CorrectOpeningDirection(4) && !Physics2D.OverlapCircle(doors[3].position, 0.5f, roomLayer))
        {
            RightRoom(reset);
        }
        
    }
    
    bool CorrectOpeningDirection(int checking)
    {
        for (int i = 0; i < openingDirection.Length; i++)
        {
            if (openingDirection[i]==checking)
            {
                
                return true;
            }
        }
        return false;
    }

    

    public void BottomRoom(bool reset)
    {
        

            //checks if there alredy is a room so we dont spawn 2 on top of each other
            
            
            
            // Need to spawn a room with a BOTTOM door.
            rand = Random.Range(0, templates.bottomRooms.Length);
            GameObject go = Instantiate(templates.bottomRooms[rand], doors[0].position, templates.bottomRooms[rand].transform.rotation);
            go.GetComponent<LevelGeneration>().entranceDirection = 1;
        
    }
    public void TopRoom(bool reset)
    {
        
        

            //checks if there alredy is a room so we dont spawn 2 on top of each other
            
            
                // Need to spawn a room with a TOP door.
                rand = Random.Range(0, templates.topRooms.Length);
        GameObject go = Instantiate(templates.topRooms[rand], doors[1].position, templates.topRooms[rand].transform.rotation);
        go.GetComponent<LevelGeneration>().entranceDirection = 1;

    }
    public void LeftRoom(bool reset)
    {
        
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
        GameObject go = Instantiate(templates.leftRooms[rand], doors[2].position, templates.leftRooms[rand].transform.rotation);
        go.GetComponent<LevelGeneration>().entranceDirection = 1;
    }
    public void RightRoom(bool reset)
    {
        
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
        GameObject go = Instantiate(templates.rightRooms[rand], doors[3].position, templates.rightRooms[rand].transform.rotation);
        go.GetComponent<LevelGeneration>().entranceDirection = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    if (spawned == false&&collision.tag=="Player")
    {

            SpawnRooms(false);
           
            spawned = true;
    }
        /*if (other.CompareTag("SpawnPoint"))
        {
            if (other.GetComponent<LevelGeneration>().spawned == false && spawned == false)
            {
                Instantiate(templates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }*/
    }
}
