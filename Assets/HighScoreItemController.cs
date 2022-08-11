using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreItemController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI order;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI date;

    public void UpdateInfo(HighScoreInfo data, int index)
    {
        int temp = index + 1;
        if (temp < 10)
        {
            order.text = "#0" + temp;
        }
        else
        {
            order.text = "#" + temp;
        }
        name.text = data.name;
        score.text = data.score.ToString();
        time.text = data.time;
        date.text = data.date;
    }
}
