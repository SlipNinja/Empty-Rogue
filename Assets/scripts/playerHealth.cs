using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{

    public float fuelLoss = 5;
    public float coinScore=100;
    public float weaponPickupScore = 1000;
    // Start is called before the first frame update



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //we take damage
            GameObject.FindGameObjectWithTag("fuel").GetComponent<fuelSlider>().changeFuel(fuelLoss);

        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            GameObject.FindGameObjectWithTag("score").GetComponent<score>().updateScore(coinScore);
            Destroy(collision.gameObject);
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

}
