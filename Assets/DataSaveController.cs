using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveController : MonoBehaviour
{
    string saveFile = "";

    private void Awake()
    {
        saveFile = Application.streamingAssetsPath + "/Data.json";
    }

    public string ReadFile()
    {
        var result = File.ReadAllText(saveFile);
        return result;
    }

    public void WriteFile(string jsonString)
    {
        File.WriteAllText(saveFile, jsonString);
    }
}
