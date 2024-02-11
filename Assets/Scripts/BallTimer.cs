using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTimer : MonoBehaviour
{
    private float ballDestroyDelay = 5f;
    private float hoopDestroyDelay = 10f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBall", ballDestroyDelay);
        Invoke("DestroyHoop", hoopDestroyDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyBall()
    {
        if(gameObject.CompareTag("Ball"))
        {
            Destroy(gameObject);
        } 
    }

    void DestroyHoop()
    {
        if(gameObject.CompareTag("Hoop"))
        {
            Destroy(gameObject);
        }
    }
    
}
