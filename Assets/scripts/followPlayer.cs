using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    public Transform player;
    public bool shaking = false;

    private void Update()
    {
        if (!shaking) {
            transform.position = new Vector3(player.position.x, player.position.y, -10);
        }
    }
}
