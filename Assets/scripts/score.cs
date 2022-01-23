using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class score : MonoBehaviour
{
    public TextMeshProUGUI scoretext;
    public float Score;
    public float textSizeIncriment;
    private float textSize;
    public float maxTextSize;
    public float timeBig;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        scoretext = GetComponent<TextMeshProUGUI>();
        textSize = scoretext.fontSize;


    }

    

    public void updateScore(float change)
    {
        Score += change;
        scoretext.text = "--SCORE--\n" + Score;
        textAnimation();
        if (scoretext.fontSize<maxTextSize&& scoretext.fontSize>15) {
            scoretext.fontSize = textSize + Random.Range(-textSizeIncriment, textSizeIncriment);
            
            timeElapsed = 0;
        }
        GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, Random.Range(-5, 5));
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed>timeBig&&textSize!= scoretext.fontSize)
        {
            GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
            scoretext.fontSize = textSize;
        }

    }

    void textAnimation()
    {
        
    }

}
