using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public TextMeshProUGUI scoretext;
    public float Score;

    // Start is called before the first frame update
    void Start()
    {
        scoretext = GetComponent<TextMeshProUGUI>();
    }

    

    public void updateScore(float change)
    {
        Score += change;
        scoretext.text = "SCORE: " + Score;
    }
}
