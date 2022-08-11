using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingController : MonoBehaviour
{
    [SerializeField] TMP_InputField minValue;
    [SerializeField] TMP_InputField maxValue;

    [SerializeField] SceneController sceneController;

    int min = 1;
    int max = 100;

    private void Start()
    {
        LoadSetting();

        minValue.onValueChanged.AddListener(delegate { OnMinValueChanged(); });
        maxValue.onValueChanged.AddListener(delegate { OnMaxValueChanged(); });
    }

    public void OnMinValueChanged()
    {
        PlayerPrefs.SetInt("Min", int.Parse(minValue.text));
    }
    
    public void OnMaxValueChanged()
    {
        PlayerPrefs.SetInt("Max", int.Parse(maxValue.text));
    }

    public void LoadSetting()
    {
        min = PlayerPrefs.GetInt("Min");
        max = PlayerPrefs.GetInt("Max");

        if(min == 0 && max == 0)
        {
            min = 1;
            max = 100;
            PlayerPrefs.SetInt("Min", 1);
            PlayerPrefs.SetInt("Max", 100);
        }

        minValue.text = min.ToString();
        maxValue.text = max.ToString();
    }

    public void BackToMenu()
    {
        sceneController.BackToMenu();
    }
}
