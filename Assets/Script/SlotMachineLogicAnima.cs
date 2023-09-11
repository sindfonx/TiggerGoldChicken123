using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineLogicAnima : MonoBehaviour
{
    public Animator ObjectAnima;
    public AnimationClip Clip;
    public float LastTime = 3f;
    public List<Text> TextList = new();
    public bool LogOff;

    private const int CleanTime = 0;
    private const int CleanResultIndex = 0;

    private List<int> resultReference = new() { 1, 2, 3, 4, 5, 7 };
    private List<int> resultList;
    private int rowNumber;

    private bool runLoop;

    private bool monitorOneRoundLoopTime;
    private List<int> sevenNumberIndex;

    private float sightLoopTime;
    private float recordCurrentTime;

    private int resultIndex;



    void Start()
    {
        rowNumber = TextList.Count-1;
        for (int i = 0; i < TextList.Count; i++)
        {
            int randomIndex = Random.Range(0, TextList.Count);
            TextList[i].text = resultReference[randomIndex].ToString();
        }

        sightLoopTime = Clip.length;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameScore.OneRound)
            {
                RandomResult();
                GetSevenNumberIndex(7);
                monitorOneRoundLoopTime = true;
                GameScore.OnSevenColorRed = false;

                if (!runLoop) ObjectAnima.Play("slotAnim", 0, 0);
                Invoke(nameof(LoopSlot), sightLoopTime);
            }
           
        }

        if (monitorOneRoundLoopTime)
        {
            recordCurrentTime += Time.deltaTime;
            if (recordCurrentTime > LastTime)
            {
                recordCurrentTime = CleanTime;
                monitorOneRoundLoopTime = false;
            }
        }

        if (sevenNumberIndex != null && GameScore.OnSevenColorRed)
        {
            for (int i = 0; i < sevenNumberIndex.Count; i++) TextList[sevenNumberIndex[i]+1].color = Color.red;
            sevenNumberIndex.Clear();
        }
    }


    private void LoopSlot()
    {
        if (!monitorOneRoundLoopTime)
        {
            // 兩個IF
            // 兩個邏輯
            // 時間內想出來 時間超過 先跳過
            if (resultIndex == rowNumber)
            {
                for (int i = 0; i < TextList.Count; i++)
                {
                    if (i == 0) TextList[i].text = TextList[i + 1].text.ToString();
                    else TextList[i].text = TextList[i].text.ToString();
                }

                resultIndex = CleanResultIndex;
                runLoop = true;
            }
            else
            {
                ResultAnimation();
                Invoke(nameof(LoopSlot), sightLoopTime);
            }
        }
        else
        {
            GameScore.OneRound = true;
            NormalAnimation();
            Invoke(nameof(LoopSlot), sightLoopTime);
        }
    }
    private void RandomResult()
    {
        for (int i = 0; i < TextList.Count; i++)
        {
            TextList[i].color = Color.black;
        }


        resultList = new();
        for (int i = 0; i < rowNumber; i++) 
        {
            int RandomIndex = Random.Range(0, TextList.Count);
            resultList.Add(resultReference[RandomIndex]);
        }

        GameScore.resultList.Add(resultList);
        GameScore.LogDef(LogOff, string.Join(",", resultList));
    }
    private void GetSevenNumberIndex(int seven)
    {
        sevenNumberIndex = new();
        for (int i = 0; i < resultList.Count; i++) if (resultList[i] == seven) sevenNumberIndex.Add(i);
        GameScore.LogDef(LogOff, string.Join(",", sevenNumberIndex));
        if (sevenNumberIndex.Count == 0) sevenNumberIndex = null;
        else GameScore.KeepSevenNumber += sevenNumberIndex.Count;
    }

    private void ResultAnimation()
    {
        ObjectAnima.Play("slotAnim", 0, 0);
       
        for (int resultIndex = 0; resultIndex < TextList.Count; resultIndex++)
        {
            if (resultIndex == rowNumber)
            {
                TextList[resultIndex].text = resultList[this.resultIndex].ToString();
                this.resultIndex++;
            }
            else TextList[resultIndex].text = TextList[resultIndex + 1].text.ToString();
        }
    }

    private void NormalAnimation()
    {
        ObjectAnima.Play("slotAnim", 0, 0);
        for (int animaIndex = 0; animaIndex < TextList.Count; animaIndex++)
        {
            if (animaIndex == rowNumber) TextList[animaIndex].text = Random.Range(0, TextList.Count).ToString();
            else TextList[animaIndex].text = TextList[animaIndex + 1].text.ToString();
        }
    }
}
