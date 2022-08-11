using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreInfo
{
    public string name;
    public int score;
    public string time;
    public string date;
}

[System.Serializable]
public class HighScoreData
{
    public List<HighScoreInfo> Items;
}
