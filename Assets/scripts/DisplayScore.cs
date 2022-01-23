using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{

    public float speed=2;
    public float score;
    public float currentScore=0;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.FindGameObjectWithTag("score").GetComponent<score>().Score;
        //sounds
        FindObjectOfType<AudioManager>().play("desthSound");

        foreach (AudioSource sound in FindObjectOfType<AudioManager>().gameObject.GetComponents<AudioSource>())
        {
            

            if (sound.clip.name == "robot combat MUSIC Loop")
            {

                sound.Stop();
            }
        }

        //loads up menu with delay
        Invoke("extraStep",1.5f);
    }

    void extraStep()
    {
        StartCoroutine(display());
    }

    IEnumerator display()
    {
        
        while (currentScore<score)
        {
            
            currentScore += score/speed * Time.deltaTime;
            text.text = "--SCORE--\n"+currentScore.ToString("F0") ;
            yield return null;
        }
        currentScore = score;
        text.text = "--SCORE--\n" + currentScore.ToString("F0");
        
    }


}
