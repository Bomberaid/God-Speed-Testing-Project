using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static volatile GameManager instance = null;

    #region Singleton
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(instance);
        }
    }

    #endregion


    public GameObject player;
    public GameObject pauseScreen;

    public GameObject winScreen;
    Animator winScreenAnimator;
    public TextMeshProUGUI playerTime;
    public TextMeshProUGUI highscore;

    public GameObject loseScreen;

    public Slider staminaBar;
    public Image fill;

    private void Start()
    {
        winScreenAnimator = GetComponent<Animator>();

        winScreen.SetActive(false);
        //winScreenAnimator.SetTrigger("Default");
        loseScreen.SetActive(false);

        Time.timeScale = 1f;
        highscore.text = PlayerPrefs.GetFloat("HighScore").ToString("f3") + " sec";
    }

    public void PlayerWins()
    {
        winScreen.SetActive(true);
        //winScreenAnimator.SetTrigger("Start");
        Time.timeScale = 0f;

        float time = Time.timeSinceLevelLoad;
        string timeText = time.ToString("f3") + " sec";

        playerTime.text = timeText;

        if(time < PlayerPrefs.GetFloat("HighScore", 100))
        {
            PlayerPrefs.SetFloat("HighScore", time);
            highscore.text = timeText;
        }
    }

    public void PlayerLoses()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetHighscore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highscore.text = PlayerPrefs.GetFloat("HighScore", 0).ToString("f3") + " sec";
    }
}
