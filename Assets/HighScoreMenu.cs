using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HighScoreMenu : MonoBehaviour
{
    [SerializeField] TextAsset highScoreData;
    [SerializeField] GameObject highScorePrefab;

    [SerializeField] GameObject content;
    [SerializeField] float height;

    [SerializeField] DataSaveController dataSaveController;

    private void Start()
    {
        LoadData();
    }

    public void LoadData()
    {
        ClearContent();
        var data = JsonUtility.FromJson<HighScoreData>(dataSaveController.ReadFile());

        List<HighScoreInfo> infos = data.Items.OrderByDescending(a => a.score).ToList();

        for(int i = 0; i < infos.Count; i++)
        {
            var item = Instantiate(highScorePrefab, Vector2.zero, Quaternion.identity, content.transform);
            item.GetComponent<HighScoreItemController>().UpdateInfo(infos[i], i);
        }

        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, height * infos.Count);
    }

    public void ClearContent()
    {
        for(int i= 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }
}
