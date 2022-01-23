using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{

    public int levelToLoad;
    public bool loadThisLevel=false;

    public void loadLevel()
    {



        //play audio
        FindObjectOfType<AudioManager>().play("startGame");


        Invoke("loadLevelWithDelay", 2.5f);
        

        


        

    }

    public void loadLevelWithDelay()
    {
        if (!loadThisLevel)
        {
            SceneManager.LoadScene(levelToLoad);
            
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
        }
    }

}
