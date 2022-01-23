using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{

    public float fuelLoss = 5;
    public float coinScore=100;
    public float weaponPickupScore = 1000;
    public float invincibilityTime=1;
    bool invincible = false;
    // Start is called before the first frame update



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //we take damage
            if (!invincible) {
                GameObject.FindGameObjectWithTag("fuel").GetComponent<fuelSlider>().changeFuel(fuelLoss);
                invincible = true;
                StartCoroutine(ExampleCoroutine());
                FindObjectOfType<AudioManager>().play("takeDamage");
            }
            Destroy(collision.gameObject);

            if (GameObject.FindGameObjectWithTag("fuel").GetComponent<fuelSlider>().fuel+fuelLoss<0- GameObject.FindGameObjectWithTag("fuel").GetComponent<fuelSlider>().pityFuel)
            {
                GetComponent<SpriteRenderer>().sprite = null;
                GetComponent<PlayerMovement>().enabled= false;
                gameObject.tag = "";
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].GetComponent<EnemyAI>().takeDamage(null);
                }
                GameObject.FindGameObjectWithTag("fuel").GetComponent<fuelSlider>().fuel -= 9999;
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            GameObject.FindGameObjectWithTag("score").GetComponent<score>().updateScore(coinScore);
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().play("coinCollect");

        } else if (collision.gameObject.tag == "weapon")
        {
            //equip new weapon
            foreach(Transform child in transform)
            {
                if (child.gameObject.tag=="weapon")
                {
                    Destroy(child.gameObject);
                }
            }
            GameObject go = Instantiate(collision.gameObject,transform.position,Quaternion.identity,transform);
            go.GetComponent<weapon>().enabled = true;
            go.GetComponent<weapon>().playerControlled = true;

            GameObject.FindGameObjectWithTag("score").GetComponent<score>().updateScore(weaponPickupScore);

            Destroy(collision.gameObject);
        }
    }

    IEnumerator ExampleCoroutine()
    {
        //Print the time of when the function is first called.
        

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(invincibilityTime);

        //After we have waited 5 seconds print the time again.
        
        invincible = false;
    }

}
