using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SessionTimeHandler : MonoBehaviour
{

    #region Singleton
    public static SessionTimeHandler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    private LootlockerManager lootlockerManagerInstance;
    private float sessionTime = 60f;
    private bool isSessionTime = false;
    public bool isPlayable = true;
    [SerializeField] private TMP_Text sessionTimeText;
    [SerializeField] private GameObject timeConteiner;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject gameOverContainer;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameObject countdownContainer;

    private void Start()
    {
        lootlockerManagerInstance = LootlockerManager.instance;
        sessionTime = PlayerPrefs.GetFloat("SessionTime", 60f);
        Time.timeScale = 0;
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = (i).ToString();
            yield return new WaitForSecondsRealtime(1f);
        }
        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);
        StartTimer();
        countdownContainer.SetActive(false);
        pauseButton.SetActive(true);
    }

    void Update()
    {
        if (isSessionTime)
        {
            sessionTime -= Time.deltaTime;
            UpdateTimer(sessionTime);
            if (sessionTime < 0)
            {
                sessionTime = 0;
                isSessionTime = false;
                PlayerController.instance.GetComponent<InputManager>().enabled = false;
                PlayerController.instance.isPlayable = false;
                EndGame();
            }
        }
    }

    void UpdateTimer(float currentTime)
    {

        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        sessionTimeText.text = "Time Left: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public IEnumerator GameOverRoutine()
    {
        yield return lootlockerManagerInstance.SubmitScoreRoutine(ScoreHandler.instance.score);
        yield return lootlockerManagerInstance.FetchTopHighScoresRoutine();
        Time.timeScale = 0;
    }

    public void StartTimer()
    {
        isSessionTime = true;
        Time.timeScale = 1;
    }

    public void PauseTimer()
    {
        if (isSessionTime)
        {
            isSessionTime = false;
            Time.timeScale = 0;
        }
        else
        {
            isSessionTime = true;
            Time.timeScale = 1;
        }
    }

    public void EndGame()
    {
        isPlayable = false;
        gameOverContainer.SetActive(true);
        timeConteiner.SetActive(false);
        pauseButton.SetActive(false);
        StartCoroutine(GameOverRoutine());
    }

}
