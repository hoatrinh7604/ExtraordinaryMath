using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private int highScore = 0;
    private int currentLevel = 0;

    [SerializeField] float maxValueTime = 5;

    private float time;
    private float timeSystem;
    private float timeGame;
    private int currentCal;
    private UIController uiController;

    private int rightAnswer;
    private bool bothAnswerRight;

    private enum Cal
    {
        summation = 0,
        subtraction = 1,
        multiplication = 2,
        division = 3
    }

    [SerializeField] DataSaveController dataSaveController;
    [SerializeField] LevelDisplayController levelDisplayController;
    // Start is called before the first frame update
    void Start()
    {
        uiController = GetComponent<UIController>();

        Reset();
        StartNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        timeGame += Time.deltaTime;
        timeSystem += Time.deltaTime;
        time -= Time.deltaTime;

        UpdateSlider(time);

        if(time < 0)
        {
            time = 10000;
            // GameOver
            UserWrong();
        }

        if(timeSystem > 10)
        {
            UpLevel();
        }

        if(Input.GetMouseButtonDown(0))
        {
            SoundController.Instance.PlayAudio(SoundController.Instance.firing, 0.5f, false);
        }
    }

    int level = 0;
    public void UpLevel()
    {
        level++;
        levelDisplayController.ChangeBG();
        maxValueTime -= 0.5f;
        if (maxValueTime < 1f) maxValueTime = 1f;
        timeSystem = 0;
    }

    public int GetCurrentLevel()
    {
        return level;
    }

    public void Reset()
    {
        time = maxValueTime;
        timeSystem = 0;
        timeGame = 0;
        currentLevel = 0;
        highScore = PlayerPrefs.GetInt("highscore");
        uiController.SetSlider(maxValueTime);
        uiController.UpdateSlider(maxValueTime);
        uiController.ShowGameOver(false);
        bothAnswerRight = false;
        SetCalsBySetting();
        min = PlayerPrefs.GetInt("Min");
        max = PlayerPrefs.GetInt("Max");
        Time.timeScale = 1;
        if(max == 0)
        {
            max = 10;
            PlayerPrefs.SetInt("Max", 10);
        }

        SoundController.Instance.PlayAudio(SoundController.Instance.bg, 0.2f, true);

        adsPopup.SetActive(false);
    }

    public void UpdateLevel(int level)
    {
        GetComponent<UIController>().UpdateLevel(level);
    }

    int min = 1;
    int max = 10;
    public void StartNextLevel()
    {
        currentLevel++;
        UpdateLevel(currentLevel);

        if(highScore < currentLevel)
        {
            highScore = currentLevel;
            PlayerPrefs.SetInt("highscore", highScore);
        }

        int firstNum = UnityEngine.Random.Range(min, max + 1);
        int lastNum = UnityEngine.Random.Range(min, max + 1);
        while(!SetCal(firstNum, lastNum))
        {
            firstNum = UnityEngine.Random.Range(min, max + 1);
            lastNum = UnityEngine.Random.Range(min, max + 1);
        }
        UpdateLevelInfo(firstNum, lastNum);
        time = maxValueTime;
        bothAnswerRight = false;
    }

    List<int> listCals = new List<int>();
    public void SetCalsBySetting()
    {
        listCals.Clear();
        if (PlayerPrefs.GetInt("Summation") == 1)
            listCals.Add((int)Cal.summation);
        if (PlayerPrefs.GetInt("Subtraction") == 1)
            listCals.Add((int)Cal.subtraction);
        if (PlayerPrefs.GetInt("Multiplication") == 1)
            listCals.Add((int)Cal.multiplication);
        if (PlayerPrefs.GetInt("Division") == 1)
            listCals.Add((int)Cal.division);

        if (listCals.Count == 0)
        {
            listCals.Add(0);
            listCals.Add(1);
            listCals.Add(2);
            listCals.Add(3);
        }
    }
    public bool SetCal(int first, int last)
    {
        int random = listCals[UnityEngine.Random.Range(0, listCals.Count)];

        if (random == 3)
        {
            if(last == 0) return false;
            if (first % last == 0)
                currentCal = (int)(Cal.division);
            else
                return false;
        }
        else if (random == 1)
        {
            if(first > last)
                currentCal = (int)(Cal.subtraction);
            else
                return false ;
        }
        else if (random == 0)
            currentCal = (int)(Cal.summation);
        else
            currentCal = (int)(Cal.multiplication);

        return true;

    }

    public void UpdateLevelInfo(int firstNum, int lastnum)
    {
        string cal = "";
        if (currentCal == 3) cal = "/";
        else if(currentCal == 2) cal = "*";
        else if (currentCal == 1) cal = "-";
        else cal = "+";

        uiController.UpdateCalculation(firstNum, cal, lastnum);
        uiController.UpdateLevel(currentLevel);
        uiController.UpdateHighscore(highScore);
    }

    public void UpdateAnswer(int value, bool both = false)
    {
        rightAnswer = value;
        bothAnswerRight = both;
    }

    public void CheckAnswer(int value)
    {
        if(rightAnswer == value || bothAnswerRight)
        {
            StartNextLevel();
        }
        else
        {
            //Game over
            UserWrong();
        }
    }

    public void UpdateSlider(float value)
    {
        uiController.UpdateSlider(value);
    }

    public void PauseGame(bool isPause)
    {
        if(isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    [SerializeField] GameObject adsPopup;
    public void ShowAdsPopupToGainMoreLife()
    {
        Time.timeScale = 0;
        adsPopup.SetActive(true);
    }

    public void AdsCompleted()
    {
        Time.timeScale = 1;
        StartNextLevel();
    }

    private void UserWrong()
    {
        Time.timeScale = 0;
        SoundController.Instance.PlayAudio(SoundController.Instance.gameOver, 0.3f, false);
        ShowAdsPopupToGainMoreLife();
    }

    public void GameOver()
    {
        uiController.ShowGameOver(true);

        if(currentLevel > 0)
            SaveScore();
    }

    private void SaveScore()
    {
        var dataLocal = dataSaveController.ReadFile();
        var data = new HighScoreData();
        if(dataLocal != "")
            data = JsonUtility.FromJson<HighScoreData>(dataLocal);

        HighScoreInfo highScoreInfo = new HighScoreInfo();
        highScoreInfo.name = PlayerPrefs.GetString("username");
        highScoreInfo.score = currentLevel;
        highScoreInfo.date = DateTime.UtcNow.ToString();
        highScoreInfo.time = timeGame.ToString("0.00");

        if(data.Items == null)
        {
            data.Items = new List<HighScoreInfo>();
        }
        data.Items.Add(highScoreInfo);

        // Only save ~200
        if(data.Items.Count > 200)
        {
            List<HighScoreInfo> infos = data.Items.OrderByDescending(a => a.score).ToList();

            List<HighScoreInfo> newList = new List<HighScoreInfo>();
            data.Items.Clear();
            for(int i = 0; i < infos.Count; i++)
            {
                if (i > 199) break;
                newList.Add(infos[i]);
            }
            data.Items = newList; 
        }

        dataSaveController.WriteFile(JsonUtility.ToJson(data));
    }
}
