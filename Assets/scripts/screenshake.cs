using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenshake : MonoBehaviour
{
    public Transform cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        
    }

    

    public IEnumerator Shake (float duration, float magnitude)
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<followPlayer>().shaking=true;
        Vector3 originalPos =(Vector3) cam.position;

        float elapsed = 0.0f;

        while (elapsed<duration)
        {
            float x = Random.Range(-1f,1f)*magnitude;
            float y = Random.Range(-1f,1f)*magnitude;

            cam.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        
        cam.localPosition = new Vector3(0,0,-10);
        
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<followPlayer>().shaking = false;
    }
}
