using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UsernameController : MonoBehaviour
{
    [SerializeField] TMP_InputField username;

    private void Start()
    {
        if(PlayerPrefs.GetString("username") != "")
        {
            username.text = PlayerPrefs.GetString("username");
        }
    }

    public void UpdateCurrentUsername()
    {
        if(username.text != "")
        {
            PlayerPrefs.SetString("username", username.text);
        }
        else
        {
            PlayerPrefs.SetString("username", "Anonymous");
        }
    }
}
