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

    // Getter for gameManager
    protected GameManager GetGameManager()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
        return gameManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyHoop", hoopDestroyDelay);
        // Access gameManager through the getter method
        gameManager = GetGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //This is working as Inheritance for the Red, Green, and Blue Hoops
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
