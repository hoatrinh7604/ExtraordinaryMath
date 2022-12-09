using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingController : MonoBehaviour
{
    [SerializeField] Button[] listRanges;

    [SerializeField] SceneController sceneController;

    int min = 1;
    int max = 10;

    private void Start()
    {
        for(int i = 0; i < listRanges.Length; i++)
        {
            int j = i;
            listRanges[i].onClick.AddListener(delegate { SetRange(j); });
        }
        LoadSetting();
    }

    public void SetRange(int index)
    {
        for(int i = 0; i < listRanges.Length; i++)
        {
            listRanges[i].interactable = true;
        }
        listRanges[index].interactable = false;

        switch(index)
        {
            case 0:
                min = 1;
                max = 10;
                break;
            case 1:
                min = 1;
                max = 100;
                break;
            case 2:
                min = 1;
                max = 1000;
                break;
            case 3:
                min = 10;
                max = 100;
                break;
            case 4:
                min = 10;
                max = 1000;
                break;
            case 5:
                min = 100;
                max = 1000;
                break;
            default:
                index = 0;
                min = 1;
                max = 10;
                break;
        }

        PlayerPrefs.SetInt("Min", min);
        PlayerPrefs.SetInt("Max", max);
        PlayerPrefs.SetInt("RangeIndex", index);
    }

    int currentIndex = 0;
    public void LoadSetting()
    {
        currentIndex = PlayerPrefs.GetInt("RangeIndex");
        SetRange(currentIndex);
    }

    public void BackToMenu()
    {
        sceneController.BackToMenu();
    }
}
