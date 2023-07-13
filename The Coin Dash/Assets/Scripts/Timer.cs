using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float totalTime = 5f; 
    private float currentTime; 
    private Text timerText; 

    private bool isActive = false; 

    private void Start()
    {
        timerText = GetComponent<Text>(); 
        InitializeTimer();
    }

    private void OnEnable()
    {
        isActive = true;
        InitializeTimer();
    }

    private void OnDisable()
    {
        isActive = false;
        StopCoroutine(UpdateTimer());
    }

    private void InitializeTimer()
    {
        currentTime = totalTime; 
        timerText.text = currentTime.ToString("F0"); 

        StartCoroutine(UpdateTimer()); 
    }

    private void TimerExpired()
    {
        timerText.text = "0"; 
    }

    private System.Collections.IEnumerator UpdateTimer()
    {
        while (currentTime > 0f && isActive)
        {
            yield return new WaitForSeconds(1f); 
            currentTime -= 1f; 
            timerText.text = Mathf.RoundToInt(currentTime).ToString();
        }

        if (isActive)
        {
            TimerExpired(); 
        }
    }
}