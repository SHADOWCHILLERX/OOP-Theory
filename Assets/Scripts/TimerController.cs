using System.Collections;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float timerDuration = 30f; //300 seconds for 5 minutes or 180 seconds for 3 minutes
    public bool isTimerRunning = false;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // Start the timer when the game starts
        //StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        // You can add other game logic here if needed
    }

    // Coroutine to handle the timer
    IEnumerator TimerCoroutine()
    {
        isTimerRunning = true;

        float timer = timerDuration;

        while (timer > -1f)
        {
            UpdateTimerDisplay(timer);
            yield return new WaitForSeconds(1f);
            timer--;

            if (!gameManager.isGameActive)
            {
                isTimerRunning = false;
                yield break;
            }
        }

        // Timer has reached zero, you can perform any actions here
        TimerComplete();
        
        gameManager.EndGame();

    }

    // Start the timer
    public void StartTimer()
    {
        StartCoroutine(TimerCoroutine());
    }

    // Update the timer display
    void UpdateTimerDisplay(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Actions to perform when the timer completes
    void TimerComplete()
    {
        isTimerRunning = false;
        // Add any actions you want to perform when the timer reaches zero
        Debug.Log("Timer complete!");
    }


}
