using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // GameOver
            GameOver();
        }

        if(timeSystem > 30)
        {
            maxValueTime--;
            if (maxValueTime < 2) maxValueTime = 2;
            timeSystem = 0;
        }
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
    }

    public void UpdateLevel(int level)
    {
        GetComponent<UIController>().UpdateLevel(level);
    }

    public void StartNextLevel()
    {
        currentLevel++;
        UpdateLevel(currentLevel);

        if(highScore < currentLevel)
        {
            highScore = currentLevel;
            PlayerPrefs.SetInt("highscore", highScore);
        }

        int firstNum = UnityEngine.Random.Range(1, 20);
        int lastNum = UnityEngine.Random.Range(1, 20);
        while(!SetCal(firstNum, lastNum))
        {
            firstNum = UnityEngine.Random.Range(1, 20);
            lastNum = UnityEngine.Random.Range(1, 20);
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
            GameOver();
        }
    }

    public void UpdateSlider(float value)
    {
        uiController.UpdateSlider(value);
    }

    public void GameOver()
    {
        uiController.ShowGameOver(true);

        SaveScore();
    }

    private void SaveScore()
    {
        var data = JsonUtility.FromJson<HighScoreData>(dataSaveController.ReadFile());
        HighScoreInfo highScoreInfo = new HighScoreInfo();
        highScoreInfo.name = PlayerPrefs.GetString("username");
        highScoreInfo.score = currentLevel;
        highScoreInfo.date = DateTime.UtcNow.ToString();
        highScoreInfo.time = timeGame.ToString("0.00");

        data.Items.Add(highScoreInfo);

        dataSaveController.WriteFile(JsonUtility.ToJson(data));
    }
}
