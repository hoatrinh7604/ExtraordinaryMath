using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI highScore;
    [SerializeField] TextMeshProUGUI level;

    [SerializeField] TextMeshProUGUI firstNum;
    [SerializeField] TextMeshProUGUI calculation;
    [SerializeField] TextMeshProUGUI lastNum;

    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;

    [SerializeField] Slider slider;

    [SerializeField] GameObject gameOver;

    public void UpdateCalculation(int first, string calculation, int last)
    {
        firstNum.text = first.ToString();
        this.calculation.text = calculation;
        lastNum.text = last.ToString();

        int[] result = new int[2];
        if(calculation == "*")
        {
            int random = Random.Range(0, 2);
            result[random] = first * last;

            int random2 = Random.Range(0, 2);
            if(random2 == 0)
            {
                result[1 - random] = result[random] + GetRandom();
            }
            else
            {
                result[1 - random] = Mathf.Abs(result[random] - GetRandom());
            }

            bool both = result[random] == result[1 - random];

            GameController.Instance.UpdateAnswer(random, both);
        }
        else if (calculation == "/")
        {
            int random = Random.Range(0, 2);
            result[random] = first / last;

            int random2 = Random.Range(0, 2);
            if (random2 == 0)
            {
                result[1 - random] = result[random] + GetRandom();
            }
            else
            {
                result[1 - random] = Mathf.Abs(result[random] - GetRandom());
            }

            bool both = result[random] == result[1 - random];
            GameController.Instance.UpdateAnswer(random, both);
        }
        else if (calculation == "+")
        {
            int random = Random.Range(0, 2);
            result[random] = first + last;

            int random2 = Random.Range(0, 2);
            if (random2 == 0)
            {
                result[1 - random] = result[random] + GetRandom();
            }
            else
            {
                result[1 - random] = Mathf.Abs(result[random] - GetRandom());
            }

            bool both = result[random] == result[1 - random];
            GameController.Instance.UpdateAnswer(random, both);
        }
        else if (calculation == "-")
        {
            int random = Random.Range(0, 2);
            result[random] = first - last;

            int random2 = Random.Range(0, 2);
            if (random2 == 0)
            {
                result[1 - random] = result[random] + GetRandom();
            }
            else
            {
                result[1 - random] = Mathf.Abs(result[random] - GetRandom());
            }

            bool both = result[random] == result[1 - random];
            GameController.Instance.UpdateAnswer(random, both);
        }

        leftButton.GetComponentInChildren<TextMeshProUGUI>().text = result[0].ToString();
        rightButton.GetComponentInChildren<TextMeshProUGUI>().text = result[1].ToString();
    }

    public int GetRandom()
    {
        int tile = Random.Range(1, 10);
        if(tile < GameController.Instance.GetCurrentLevel())
        {
            return tile*10;
        }

        return tile * Random.Range(1,6);
    }

    public void SetSlider(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }

    public void UpdateSlider(float value)
    {
        slider.value = value;
    }

    public void UpdateHighscore(int value)
    {
        highScore.text = value.ToString();
    }

    public void UpdateLevel(int value)
    {
        level.text = value.ToString();
    }

    public void ShowGameOver(bool isShow)
    {
        gameOver.SetActive(isShow);
    }
}
