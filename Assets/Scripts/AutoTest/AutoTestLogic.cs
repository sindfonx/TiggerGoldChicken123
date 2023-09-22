using System.Collections.Generic;
using UnityEngine;

public class AutoTestLogic : MonoBehaviour
{
    private List<int> strList = new() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7 };
    private bool autoTestOff;
    private bool NetxTimeOff;

    void Update()
    {
        if (!autoTestOff)
        {
            if (Input.GetMouseButtonDown(0))
            {
                autoTestOff = true;
            }
        }
        else
        {
            if (!NetxTimeOff)
            {
                Invoke("AnalyzeResult", 1f);
                NetxTimeOff = true;
            }
        }
    }

    private void AnalyzeResult()
    {
        int result = strList[Random.Range(0, strList.Count)];
        if (result == 7)
        {
            Debug.Log($"{result}");
            autoTestOff = false;
        }
        else
        {
            Debug.Log($"{result}");
        }
        NetxTimeOff = false;
    }
}