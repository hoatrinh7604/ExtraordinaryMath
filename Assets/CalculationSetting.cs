using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalculationSetting : MonoBehaviour
{
    [SerializeField] Button[] listCalculations;

    private bool[] calculationEnable = {true, true, true, true};

    private void Start()
    {
        LoadSetting();
        RefreshDisplay();
        for (int i = 0; i < listCalculations.Length; i++)
        {
            int j = i;
            listCalculations[i].onClick.AddListener(delegate { EnableCalculation(j); });
        }
    }

    public void RefreshDisplay()
    {
        for(int i = 0; i < listCalculations.Length; i++)
        {
            listCalculations[i].transform.GetChild(0).gameObject.SetActive(calculationEnable[i]);
        }
    }

    public void SwitchModeButton(int id)
    {
        calculationEnable[id] = !calculationEnable[id];
        if(CountMath() == 0)
            calculationEnable[id] = !calculationEnable[id];
    }

    public int CountMath()
    {
        int count = 0;
        for(int i = 0; i < calculationEnable.Length; i++)
        {
            if (calculationEnable[i])
                count++;
        }

        return count;
    }

    public void LoadSetting()
    {
        calculationEnable[0] = PlayerPrefs.GetInt("Summation") == 1 ? true : false;
        calculationEnable[1] = PlayerPrefs.GetInt("Subtraction") == 1 ? true : false;
        calculationEnable[2] = PlayerPrefs.GetInt("Multiplication") == 1 ? true : false;
        calculationEnable[3] = PlayerPrefs.GetInt("Division") == 1 ? true : false;

        if (!calculationEnable[0] && !calculationEnable[1] && !calculationEnable[2] && !calculationEnable[3])
        {
            calculationEnable[0] = true;
            calculationEnable[1] = true;
            calculationEnable[2] = true;
            calculationEnable[3] = true;

            SetCalculation();
        }
    }

    public void SetCalculation()
    {
        PlayerPrefs.SetInt("Summation", calculationEnable[0] == true ? 1 : 0);
        PlayerPrefs.SetInt("Subtraction", calculationEnable[1] == true ? 1 : 0);
        PlayerPrefs.SetInt("Multiplication", calculationEnable[2] == true ? 1 : 0);
        PlayerPrefs.SetInt("Division", calculationEnable[3] == true ? 1 : 0);
    }

    public void EnableCalculation(int id)
    {
        SwitchModeButton(id);
        RefreshDisplay();
        SetCalculation();
    }

}
