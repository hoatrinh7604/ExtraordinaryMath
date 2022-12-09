using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaveController : MonoBehaviour
{
    string saveFile = "/Data.json";

    public string ReadFile()
    {
        string data = "";
        if (File.Exists(Application.persistentDataPath + saveFile))
        {
            StreamReader reader = new StreamReader(Application.persistentDataPath + saveFile);
            data = reader.ReadToEnd();
            reader.Close();
        }
        return data;
    }

    public void WriteFile(string jsonString)
    {
        StreamWriter writer = new StreamWriter(Application.persistentDataPath + saveFile);
        writer.Write(jsonString); //Creates editable data in form of a string.
        writer.Close();
    }

}
