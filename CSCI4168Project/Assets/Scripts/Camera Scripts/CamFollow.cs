using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class used on player camera to follow player
 */
public class CamFollow : MonoBehaviour
{
    public Transform camPos;
    void Update()
    {
        // move camera to player
        transform.position = camPos.position;
    }
}
