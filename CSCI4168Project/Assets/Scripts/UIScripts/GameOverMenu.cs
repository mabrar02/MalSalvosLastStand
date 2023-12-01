using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Debug.Log("Game Over");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
}
