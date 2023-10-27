using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed;
    public float boost;
    public float upSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step;
        if (Input.GetKey(KeyCode.LeftControl))
        {
            step = (speed * boost) * Time.deltaTime;
        }
        else
        {
            step = speed * Time.deltaTime;
        }
        
        float upStep = upSpeed * Time.deltaTime;
        
        transform.Translate( Input.GetAxis("Horizontal") * step,
            0,
            Input.GetAxis("Vertical") * step);

        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * upStep);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(Vector3.down * upStep);
        }
        
    }
}
