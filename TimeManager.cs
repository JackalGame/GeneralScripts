using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{  
    [Header("Countdown Timer")]
    [SerializeField] float timeLeft = 20f;
    [SerializeField] float restoreTimeAmount = 20f;

    [Header("Canvas")]
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] Text countdownText;

    private static TimeManager _instance;

    private ColorGrading colorGradingLayer;
    private Grain grainLayer;

    public static TimeManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<TimeManager>();
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null) Destroy(gameObject);
        else _instance = this;

        PostProcessVolume camVol = Camera.main.GetComponent<PostProcessVolume>();
        camVol.profile.TryGetSettings(out colorGradingLayer);
        camVol.profile.TryGetSettings(out grainLayer);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseTime();
        ControlCameraEffect();
    }

    private void DecreaseTime()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft < 0)
        {
            countdownText.text = "Oh no! The Nothing has taken over.";
            timeLeft = 0;
            gameOverCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            countdownText.text = timeLeft.ToString();
        }
    }

    public void RestoreTime() // We can call this from a pickup.
    {
        timeLeft = restoreTimeAmount;
    }

    private void ControlCameraEffect() // Camera fades to GreyScale the closer the score gets to 0.
    {
        if (timeLeft <= 20)
        {
            colorGradingLayer.enabled.value = true;
            var saturation = timeLeft * 5 - 100;
            colorGradingLayer.saturation.value = saturation;
            
            if(timeLeft <= 1)
            {
                var grain = 1 - timeLeft;
                grainLayer.enabled.value = true;
                grainLayer.intensity.value = grain;
            }

        }
        else
        {
            colorGradingLayer.enabled.value = false;
            grainLayer.enabled.value = false;
        }
    }
}
