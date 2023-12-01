using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ErrorText : MonoBehaviour
{
    public TextMeshProUGUI errorText;
    public float displayTime;
    private float timer;

    void Start()
    {
        errorText.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0) {
            timer -= Time.deltaTime;
            if(timer <= 0) {
                errorText.text = string.Empty;
            }
        }
    }

    public void ShowErrorMessage(string message) {
        AudioManager.Instance.Play("Error");
        errorText.text = message;
        timer = displayTime;
    }
}
