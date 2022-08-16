using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] DataSaveController dataSaveController;
    public void ResetData()
    {
        PlayerPrefs.SetInt("Min", 0);
        PlayerPrefs.SetInt("Max", 0);

        PlayerPrefs.SetInt("Summation", 1);
        PlayerPrefs.SetInt("Subtraction", 1);
        PlayerPrefs.SetInt("Multiplication", 1);
        PlayerPrefs.SetInt("Division", 1);


        HighScoreData highScoreData = new HighScoreData();
        var saveFile = Application.streamingAssetsPath + "/Data.json";
        string jsonString = JsonUtility.ToJson(highScoreData);
        dataSaveController.WriteFile(jsonString);
    }
}
