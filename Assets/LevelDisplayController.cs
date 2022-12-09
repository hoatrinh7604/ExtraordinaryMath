using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDisplayController : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Color[] colors;

    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Image imageSlider;
    [SerializeField] Button imageButton;

    int level = -1;
    private void Start()
    {
        ChangeBG();
    }

    public void ChangeBG()
    {
        level++;
        if(level > sprites.Length - 1) level = sprites.Length - 1;
        background.sprite = sprites[level];
        levelText.color = colors[level];
        imageSlider.color = colors[level];
        imageButton.image.color = colors[level];
    }
}
