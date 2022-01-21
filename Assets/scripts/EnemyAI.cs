using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed=5;
    public Transform groundcheck;
    public int direction = 1;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        patrolling();
    }


    void patrolling()
    {
        rb.velocity = new Vector2(direction*speed,rb.velocity.y);
        RaycastHit2D ray = Physics2D.Raycast(groundcheck.position,Vector2.down);
        if (ray.collider == false)
        {
            if (direction==1) {
                direction = -1;
                transform.eulerAngles = new Vector3(0,-180,0);
            }
            else if (direction == -1)
            {
                direction = 1;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

}
