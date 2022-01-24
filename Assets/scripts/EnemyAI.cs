using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public enum State : int { Patrolling, Chasing, Shooting, Running }
    public State EnemyState;
    [Header("Stats")]
    public float speed = 5;
    public float jumpHeight = 10;
    public float jumpCooldown = 5;
    public float health=3;
    public float fuelOnDeath=10;
    public float scoreOnDeath = 500;
    float timeSinceLastJumped;
    public float AttackDistance;
    public Transform groundcheck;
    public Transform LOSpoint;
    private int direction = 1;
    private Rigidbody2D rb;
    [Header("weapon")]
    public GameObject weapon;
    public weapon weaponscript;
    [Header("Chasing")]
    bool lostLOS;
    private float timeSinceLastSeen;
    //how long without los before we stop chasing the player
    public float timeToStopChasing;
    [Header("Enemy Health")]
    public string[] deathSounds;
    public string[] damageSounds;
    public Slider healthSlider;
    public GameObject explosion;
    public float screenshakeAmount;
    public float screenshakeTime;
    [Space]
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (weapon != null)
        {
            equipWeapon(weapon);
        }
        timeSinceLastJumped = jumpCooldown;

        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        //raycast from ontop of the enemy
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 Direction = playerPos - (Vector2)LOSpoint.position;
        RaycastHit2D ray = Physics2D.Raycast(LOSpoint.position, Direction, 20f);

        //if we see the player then start chasing him
        if (ray.collider != false)
        {
            if (ray.collider.gameObject.tag == "Player"&&EnemyState==State.Patrolling)
            {
                EnemyState = State.Chasing;
            }
        } 

        switch (EnemyState)
        {
            case State.Patrolling:
                patrolling();
                break;
            case State.Chasing:
                Chasing(ray);
                break;
            case State.Shooting:
                shooting(ray);
                break;
            case State.Running:

                break;

        }

        
    }


    void patrolling()
    {
        //patrols the area of a platform
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
        RaycastHit2D ray = Physics2D.Raycast(groundcheck.position, Vector2.down,1);
        RaycastHit2D raySide = Physics2D.Raycast(groundcheck.position, Vector2.right, 0.5f);
        if (ray.collider == false&&raySide==false)
        {
            if (direction == 1)
            {
                direction = -1;
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else if (direction == -1)
            {
                direction = 1;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }//jump
        else {
            if(timeSinceLastJumped > jumpCooldown)
            {
                rb.AddForce(Vector2.up * jumpHeight);
                timeSinceLastJumped = 0;
            }
            else { timeSinceLastJumped += Time.deltaTime; }
        }
    }

    void Chasing(RaycastHit2D ray)
    {
        //runs closer to the player
        //first checks if there is LOS to the player
        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        
        //if we are close to the player we can start attacking
        if (Vector2.Distance(transform.position, playerPos) < AttackDistance)
        {
            
            EnemyState = State.Shooting;
        }

        if (ray.collider != false)
        {
            if (ray.collider.gameObject.tag == "Player")
            {

                timeSinceLastSeen = 0;

                
            }


            else if (ray.collider.gameObject.tag != "Player")
            {
                timeSinceLastSeen += Time.deltaTime;
                if (timeSinceLastSeen >= timeToStopChasing)
                {
                    //starts patrolling again if we havent seen player for some time
                    EnemyState = State.Patrolling;
                }
            }


        }
        runAfterPlayer(playerPos);



    }

    //runs towards player
    public void runAfterPlayer(Vector2 playerPos)
    {
        //determines what direction the player is in
        //float runDirection = 1;
        if (playerPos.x<transform.position.x)
        {
            direction = -1;
            
        }
        else {
            direction = 1;
            
        }
        transform.localScale = new Vector2(direction, transform.localScale.y);
        foreach(Transform child in transform)
        {
            if (child.gameObject.tag!="ui") {
                child.transform.localScale = new Vector2(direction, child.transform.localScale.y);
            }
        }

        RaycastHit2D ray2 = Physics2D.Raycast(groundcheck.position, Vector2.down,0.5f);

        
        if (timeSinceLastJumped>jumpCooldown&&ray2.collider == true && shouldJump(playerPos))
        {
            
            //we are standing on the edge of something so we jump
            rb.AddForce(Vector2.up* jumpHeight);
            timeSinceLastJumped = 0;
            
        }
        //we just move from side to side
        else
        {
            rb.velocity = new Vector2(direction * speed, rb.velocity.y);
            timeSinceLastJumped += Time.deltaTime;
            
        }
    }

    bool shouldJump(Vector2 playerPos)
    {
        if (playerPos.y>transform.position.y+2)
        {
            return true;
            
        }

        
        return false;
    }

    //equips a weapon
    void equipWeapon(GameObject newWeapon)
    {
        weapon = Instantiate(newWeapon,transform.position,newWeapon.transform.rotation, transform.parent);
        weapon.transform.SetParent(transform);
        weaponscript = weapon.GetComponent<weapon>();
        weapon.GetComponent<Collider2D>().enabled = false;
        
    }

    void shooting(RaycastHit2D ray)
    {
        //stands still 
        rb.velocity = new Vector2(0,rb.velocity.y);

        //shoots
        weaponscript.shoot();
        //plays animation
        animator.SetBool("shooting", true);

        Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        //if we are not close to the player we can stop attacking
        if (ray.collider.gameObject.tag!="Player"||Vector2.Distance(playerPos, transform.position) > AttackDistance)
        {
            EnemyState = State.Chasing;
            animator.SetBool("shooting", false);

        }
    }


    //check for collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Bullet")
        {
            takeDamage(collision.gameObject);
        }
    }

    public void takeDamage(GameObject bullet)
    {
        if (bullet!=null) {
            health -= bullet.GetComponent<bulletScript>().damage;
        }
        else { health -= 99999; }
        healthSlider.value = health;
        
        if (health <= 0)
        {
            //enemy dies
            rb.velocity = Vector2.zero;
            StartCoroutine(GameObject.FindGameObjectWithTag("screenshake").GetComponent<screenshake>().Shake(screenshakeTime, screenshakeAmount));
            Instantiate(explosion, transform.position, explosion.transform.rotation);
            //plays sound
            int r = Random.Range(0, deathSounds.Length);
            FindObjectOfType<AudioManager>().play(deathSounds[r]);
            //handles stuff 
            GetComponent<SpriteRenderer>().sprite = null;
            healthSlider.gameObject.SetActive(false);
            weapon.gameObject.SetActive(false);
            GetComponent<EnemyAI>().enabled = false;
            GameObject.FindGameObjectWithTag("fuel").GetComponent<fuelSlider>().changeFuel(fuelOnDeath);
            GameObject.FindGameObjectWithTag("score").GetComponent<score>().updateScore(scoreOnDeath);
            
            Destroy(gameObject,0.1f);
        }
        else {
            StartCoroutine(GameObject.FindGameObjectWithTag("screenshake").GetComponent<screenshake>().Shake(screenshakeTime / 2, screenshakeAmount / 2));
            int r = Random.Range(0, damageSounds.Length);
            FindObjectOfType<AudioManager>().play(damageSounds[r]);
        }
    }

}
