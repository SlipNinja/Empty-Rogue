using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHealth : MonoBehaviour
{

    public float fuelLoss = 5;
    public float coinScore=100;
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
        }
    }

}
