using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    public float TimeLeft;
    public bool _activeTimer = false;
    [SerializeField] private TMPro.TMP_Text _timerText;
    [SerializeField] private GameObject _buttonTimer;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void StartCounter(float timeLeft)
    {
        _buttonTimer.SetActive(false);
        _timerText.gameObject.SetActive(true);
        Debug.Log("Start Timer: " + timeLeft);
        _activeTimer = true;
        TimeLeft = timeLeft;

    }

    // Update is called once per frame
    void Update()
    {
        _timerText.text = TimeLeft.ToString();
        if (_activeTimer)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                _activeTimer = false;
                _timerText.gameObject.SetActive(false);
         

            }



        }

        void updateTimer(float currentTime) {
            currentTime += 1;
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);
            if (minutes > 0)
            {
                _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            else
            {
                _timerText.text = seconds.ToString();
            }

        }
    
    }
}
