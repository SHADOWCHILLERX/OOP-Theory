using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HoopClass : MonoBehaviour
{
    private float hoopDestroyDelay = 10f;
    protected private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyHoop", hoopDestroyDelay);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void HoopScore(int pointValue)
    {
       gameManager.UpdateScore(pointValue);
    }

    void DestroyHoop()
    {
        if (gameObject.CompareTag("Hoop"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        HoopScore(5);
        Destroy(gameObject);
    }
}
