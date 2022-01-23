using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public bool playerControlled=false;
    [Space]
    public GameObject bulletPrefab;
    public float launchForce;
    float launchForce2;
    public Transform shotPoint;
    public float Firerate;
    float timeSinceLastShot;
    [Header("sound")]
    public string[] sounds;
    public bool loop = false;
    private AudioClip clip;

    [Header("aiming trajectory values")]
    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    Vector2 direction;

    [Header("AI Stuff")]
    GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        launchForce2 = launchForce;
        if (playerControlled)
        {


            points = new GameObject[numberOfPoints];
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i] = Instantiate(point, shotPoint.position, Quaternion.identity,shotPoint);
            }
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if the player controls the weapon
        if (playerControlled) {
            Vector2 weaponPos = transform.position;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (transform.parent.localScale.x > 0)
            {
                direction = mousePos - weaponPos;
                
                transform.right = direction;
                launchForce = launchForce2;
            }
            else
            {

                //direction = weaponPos - mousePos;
                direction = mousePos - weaponPos;
                transform.right = direction*-1;
                
                launchForce = -launchForce2;
                
            }
            
            


            if (Input.GetMouseButton(0))
            {
                shoot();
            }

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
        }
        else if (!playerControlled)
        {
            Vector2 weaponPos = transform.position;
            Vector2 playerPos = player.transform.position;
            direction = playerPos - weaponPos;
            transform.right = direction;
        }

        timeSinceLastShot += Time.deltaTime;
    }

    public void shoot()
    {
        

        if (loop)
        {
            
            foreach (AudioSource sound in FindObjectOfType<AudioManager>().gameObject.GetComponents<AudioSource>())
            {
                
                
                if (sound.clip.name == "Ray-Gun loop")
                {
                    
                    if (!sound.isPlaying)
                    {
                        FindObjectOfType<AudioManager>().play(sounds[0]);
                    }
                }
            }
        }

        if (Firerate<timeSinceLastShot) {
            //we have shot
            GameObject newBullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
            newBullet.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
            timeSinceLastShot = 0;
            
            //plays attack sound defined in inspector
            if (sounds.Length>0&&!loop) {
                int r = Random.Range(0, sounds.Length);
                FindObjectOfType<AudioManager>().play(sounds[r]);
            }
        }
    }

    Vector2 PointPosition(float t)
    {
        Vector2 pos;
        if (launchForce>0) {
            pos = (Vector2)shotPoint.position + ((direction).normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        }
        else
        {
            pos = (Vector2)shotPoint.position + ((direction*-1).normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t);
        }
        return pos;
    }
}
