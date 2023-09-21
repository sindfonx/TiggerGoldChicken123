using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColunmAnimLogic : MonoBehaviour
{
    public Animator AnimaObj;
    public AnimationClip AnimaClip;
    public float AnimaTime = 3f;
    public List<Text> PerfabTextList = new();
    public bool LogOff;


    private List<int> resultRef = new() { 1, 2, 3, 4, 5, 7 };
    private List<int> resultList;
    private int rowNumber;

    private bool sutureLoop;
    private bool autoPaly;

    private bool monitorOneRoundLoopTime;
    private List<int> sevenNumberIndex;

    private float loopTime;
    private float keepCurrentTime;
    private const int cleanTime = 0;

    private int resultIndex;
    private const int cleanResultIndex = 0;

    void Start()
    {
        loopTime = AnimaClip.length;
        rowNumber = PerfabTextList.Count - 1;
        for (int i = 0; i < PerfabTextList.Count; i++)
        {
            int randomIndex = Random.Range(0, PerfabTextList.Count);
            PerfabTextList[i].text = resultRef[randomIndex].ToString();
        }
    }

    void Update()
    {
        //Hand play
        if (Input.GetMouseButtonDown(0))
        {
            if (!GameScore.OneRound)
            {
                RandomResult();
                GetSevenNumberIndex(7);
                monitorOneRoundLoopTime = true;
                GameScore.OnSevenColorRed = false;

                if (!sutureLoop) AnimaObj.Play("slowAnim", 0, 0);
                Invoke(nameof(HandLoopSlot), loopTime);
            }
        }

        //Auto play
        if (!GameScore.autoPlay)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!GameScore.OneRound)
                {
                    RandomResult();
                    GetSevenNumberIndex(7);
                    monitorOneRoundLoopTime = true;
                    GameScore.OnSevenColorRed = false;

                    if (!sutureLoop) AnimaObj.Play("slowAnim", 0, 0);
                    Invoke(nameof(AutoLoopSlot), loopTime);
                }
            }
        }
        else
        {
            RandomResult();
            GetSevenNumberIndex(7);
            monitorOneRoundLoopTime = true;
            GameScore.OnSevenColorRed = false;

            if (!sutureLoop) AnimaObj.Play("slowAnim", 0, 0);
            Invoke(nameof(AutoLoopSlot), loopTime);
            Invoke("OnAutoPlay", Time.deltaTime);

        }




        if (monitorOneRoundLoopTime)
        {
            keepCurrentTime += Time.deltaTime;
            if (keepCurrentTime > AnimaTime)
            {
                keepCurrentTime = cleanTime;
                monitorOneRoundLoopTime = false;
            }
        }

        if (sevenNumberIndex != null && GameScore.OnSevenColorRed)
        {
            for (int i = 0; i < sevenNumberIndex.Count; i++) PerfabTextList[sevenNumberIndex[i]+1].color = Color.red;
            sevenNumberIndex.Clear();
        }
    }


    private void HandLoopSlot()
    {
        if (!monitorOneRoundLoopTime)
        {
            // 兩個IF
            // 兩個邏輯
            // 時間內想出來 時間超過 先跳過
            if (resultIndex == rowNumber)
            {
                for (int i = 0; i < PerfabTextList.Count; i++)
                {
                    if (i == 0) PerfabTextList[i].text = PerfabTextList[i + 1].text.ToString();
                    else PerfabTextList[i].text = PerfabTextList[i].text.ToString();
                }

                resultIndex = cleanResultIndex;
                sutureLoop = true;
            }
            else
            {
                ResultAnimation();
                Invoke(nameof(HandLoopSlot), loopTime);
            }
        }
        else
        {
            GameScore.OneRound = true;
            NormalAnimation();
            Invoke(nameof(HandLoopSlot), loopTime);
        }
    }
    private void AutoLoopSlot()
    {
        if (!monitorOneRoundLoopTime)
        {
            if (resultIndex == rowNumber)
            {
                for (int i = 0; i < PerfabTextList.Count; i++)
                {
                    if (i == 0) PerfabTextList[i].text = PerfabTextList[i + 1].text.ToString();
                    else PerfabTextList[i].text = PerfabTextList[i].text.ToString();
                }

                resultIndex = cleanResultIndex;
                sutureLoop = true;
            }
            else
            {
                ResultAnimation();
                Invoke(nameof(AutoLoopSlot), loopTime);
            }
        }
        else
        {
            GameScore.OneRound = true;
            NormalAnimation();
            Invoke(nameof(AutoLoopSlot), loopTime);
        }
    }


    private void OnAutoPlay()
    { 
        GameScore.SetAutoPlay(false);
    }


    private void RandomResult()
    {
        for (int i = 0; i < PerfabTextList.Count; i++)
        {
            PerfabTextList[i].color = Color.black;
        }


        resultList = new();
        for (int i = 0; i < rowNumber; i++) 
        {
            int RandomIndex = Random.Range(0, PerfabTextList.Count);
            resultList.Add(resultRef[RandomIndex]);
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
        AnimaObj.Play("slowAnim", 0, 0);
       
        for (int resultIndex = 0; resultIndex < PerfabTextList.Count; resultIndex++)
        {
            if (resultIndex == rowNumber)
            {
                PerfabTextList[resultIndex].text = resultList[this.resultIndex].ToString();
                this.resultIndex++;
            }
            else PerfabTextList[resultIndex].text = PerfabTextList[resultIndex + 1].text.ToString();
        }
    }

    private void NormalAnimation()
    {
        AnimaObj.Play("slowAnim", 0, 0);
        for (int animaIndex = 0; animaIndex < PerfabTextList.Count; animaIndex++)
        {
            if (animaIndex == rowNumber) PerfabTextList[animaIndex].text = Random.Range(0, PerfabTextList.Count).ToString();
            else PerfabTextList[animaIndex].text = PerfabTextList[animaIndex + 1].text.ToString();
        }
    }
}
