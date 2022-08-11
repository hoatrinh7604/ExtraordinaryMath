using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void ResetData()
    {
        PlayerPrefs.SetInt("Min", 0);
        PlayerPrefs.SetInt("Max", 0);

        PlayerPrefs.SetInt("Summation", 1);
        PlayerPrefs.SetInt("Subtraction", 1);
        PlayerPrefs.SetInt("Multiplication", 1);
        PlayerPrefs.SetInt("Division", 1);
    }
}
