using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColunmAnimLogic : MonoBehaviour
{
    public Animator AnimaObj;
    public AnimationClip AnimaClip;
    public float AnimaTime = 3f;
    public List<Text> PerfabTextList = new();
    public Button ObjHandButton;
    public Button ObjAutoButton;
    public bool LogOff;


    private List<int> resultRef = new() { 1, 2, 3, 4, 5, 7 };
    private List<int> resultList;
    private int rowNumber;

    private bool sutureLoopOff;
    private bool autoPlayOff;

    private bool monitorOneRoundLoopTimeOff;
    private List<int> sevenNumberIndex;

    private float AnimaClipLenght;
    private float keepCurrentTime;
    private const int cleanTime = 0;

    private int resultIndex;
    private const int cleanResultIndex = 0;

    void Start()
    {
        ObjHandButton = GameObject.Find("Canvas/ButtonHandPlay").GetComponent<Button>();
        ObjHandButton.onClick.AddListener(PlayHand);

        ObjAutoButton = GameObject.Find("Canvas/ButtonAutoPlay").GetComponent<Button>();
        ObjAutoButton.onClick.AddListener(PlayAuto);

        AnimaClipLenght = AnimaClip.length;
        rowNumber = PerfabTextList.Count - 1;
        CreateRandomText();
    }

    private void PlayHand()
    {
        if (!GameScore.SlotMachineUnlockOneRound)
        {
            RoundOneAnima("HandLoopSlot");
        }
    }


    private void PlayAuto()
    {
        if (GameScore.SlotMachineSetAutoNumber > 0)
        {
            if (!GameScore.SlotMachineAutoPlay)
            {
                if (!GameScore.SlotMachineUnlockOneRound)
                {
                    RoundOneAnima("AutoLoopSlot");
                }
            }
        }
    }
    private void RoundOneAnima(string method)
    {
        RandomResult();
        GetSevenNumberIndex(7);
        monitorOneRoundLoopTimeOff = true;
        GameScore.SlotMachineOnSevenColorRed = false;

        if (!sutureLoopOff) AnimaObj.Play("slowAnim", 0, 0);
        Invoke($"{method}", AnimaClipLenght);
    }
    private void CreateRandomText()
    {
        for (int i = 0; i < PerfabTextList.Count; i++)
        {
            int randomIndex = Random.Range(0, PerfabTextList.Count);
            PerfabTextList[i].text = resultRef[randomIndex].ToString();
        }
    }



    void Update()
    {
        if (GameScore.SlotMachineAutoPlay)
        {
            RoundOneAnima("AutoLoopSlot");
            Invoke("OnAutoPlay", Time.deltaTime);
        }


        if (monitorOneRoundLoopTimeOff)
        {
            keepCurrentTime += Time.deltaTime;
            if (keepCurrentTime > AnimaTime)
            {
                keepCurrentTime = cleanTime;
                monitorOneRoundLoopTimeOff = false;
            }
        }

        if (sevenNumberIndex != null && GameScore.SlotMachineOnSevenColorRed)
        {
            for (int i = 0; i < sevenNumberIndex.Count; i++) PerfabTextList[sevenNumberIndex[i]+1].color = Color.red;
            sevenNumberIndex.Clear();
        }
    }
    private void OnAutoPlay()
    { 
        GameScore.SetSlotMachineAutoPlay(false);
    }

    private void HandLoopSlot()
    {
        if (!monitorOneRoundLoopTimeOff)
        {
            if (resultIndex == rowNumber)
            {
                for (int i = 0; i < PerfabTextList.Count; i++)
                {
                    if (i == 0) PerfabTextList[i].text = PerfabTextList[i + 1].text.ToString();
                    else PerfabTextList[i].text = PerfabTextList[i].text.ToString();
                }

                resultIndex = cleanResultIndex;
                sutureLoopOff = true;
            }
            else
            {
                ResultAnimation();
                Invoke(nameof(HandLoopSlot), AnimaClipLenght);
            }
        }
        else
        {
            GameScore.SlotMachineUnlockOneRound = true;
            NormalAnimation();
            Invoke(nameof(HandLoopSlot), AnimaClipLenght);
        }
    }
    private void AutoLoopSlot()
    {
        if (!monitorOneRoundLoopTimeOff)
        {
            if (resultIndex == rowNumber)
            {
                for (int i = 0; i < PerfabTextList.Count; i++)
                {
                    if (i == 0) PerfabTextList[i].text = PerfabTextList[i + 1].text.ToString();
                    else PerfabTextList[i].text = PerfabTextList[i].text.ToString();
                }

                resultIndex = cleanResultIndex;
                sutureLoopOff = true;
            }
            else
            {
                ResultAnimation();
                Invoke(nameof(AutoLoopSlot), AnimaClipLenght);
            }
        }
        else
        {
            GameScore.SlotMachineUnlockOneRound = true;
            NormalAnimation();
            Invoke(nameof(AutoLoopSlot), AnimaClipLenght);
        }
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

        GameScore.SlotMachineColunmList.Add(resultList);
        GameScore.LogDef(LogOff, string.Join(",", resultList));
    }
    private void GetSevenNumberIndex(int seven)
    {
        sevenNumberIndex = new();
        for (int i = 0; i < resultList.Count; i++) if (resultList[i] == seven) sevenNumberIndex.Add(i);
        GameScore.LogDef(LogOff, string.Join(",", sevenNumberIndex));
        if (sevenNumberIndex.Count == 0) sevenNumberIndex = null;
        else GameScore.SlotMachineKeepSevenNumber += sevenNumberIndex.Count;
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
