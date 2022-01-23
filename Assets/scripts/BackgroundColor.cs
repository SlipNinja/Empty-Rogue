using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    private Camera cam;
    public AudioClip backgroundMusic;
    public Color backgroundColor;
    public float intensity = 10;
    // Start is called before the first frame update
    void Start()
    {
        

        //camera get
        cam = GetComponent<Camera>();

        //color set
        backgroundColor = cam.backgroundColor;

        //gets bacground music
        foreach (AudioSource sound in FindObjectOfType<AudioManager>().gameObject.GetComponents<AudioSource>())
        {


            if (sound.clip.name == "robot combat MUSIC Loop")
            {
                
                backgroundMusic = sound.clip;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
