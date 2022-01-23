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
        if (!loadThisLevel)
        {
            SceneManager.LoadScene(levelToLoad);
        }
        else { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }
}
