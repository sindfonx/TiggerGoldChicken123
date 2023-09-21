using System.Collections.Generic;
using UnityEngine;

public class ControLogic : MonoBehaviour
{
    public float lastTime = 15f;
    public GameObject SingleColumn;
    public GameObject SingleBingoLine;
    public GameObject WindowSlot;
    public int ColumnNumber;

    private int WindowWidth;
    private float currentPosX;
    private float space;
    private int rowNumber;

    private const float cleanTime = 0;
    private float currentTime;
    private bool handResultVeiwOn;
    private bool AutoResultVeiwOn;


    private List<GameObject> bingoObjList = new();
    private bool autoOff;


    private void Start()
    {
        SetWidth();

        for (int i = 1; i < 10; i+=2)
        {
            Vector2 currentPos = new()
            {
                x = 0,
                y = -50 * i
            };

            GameObject singleBingoLineObj = Instantiate(SingleBingoLine, WindowSlot.GetComponent<Transform>());
            singleBingoLineObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPos.x, currentPos.y);
            bingoObjList.Add(singleBingoLineObj);
        }

    }

    private void SetWidth()
    {
        //BG widht 
        WindowWidth = ColumnNumber * 150;

        //�_�l�Y 
        currentPosX = -(WindowWidth / 2);

        //�����Z��
        space = WindowWidth / (ColumnNumber + 1);

        int lastTime = 3;
        for (int i = 0; i < ColumnNumber; i++)
        {
            currentPosX += space;
            Vector3 currentPos = new()
            {
                x = currentPosX,
                y = 0,
                z = 0
            };

            GameObject columnObj = Instantiate(SingleColumn, WindowSlot.GetComponent<Transform>());
            columnObj.name = $"column{i}";

            rowNumber = columnObj.GetComponent<ColunmAnimLogic>().PerfabTextList.Count;
            columnObj.GetComponent<ColunmAnimLogic>().AnimaTime = lastTime;
            lastTime += 2;

            columnObj.GetComponent<RectTransform>().anchoredPosition = currentPos;
        }

        RectTransform windowSlotRectObj = WindowSlot.GetComponent<RectTransform>();
        windowSlotRectObj.sizeDelta = new Vector2(WindowWidth - space, windowSlotRectObj.sizeDelta.y);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            for (int i = 0; i < bingoObjList.Count; i++)
            { 
                bingoObjList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(10, bingoObjList[i].GetComponent<RectTransform>().sizeDelta.y);
            }
            handResultVeiwOn = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            for (int i = 0; i < bingoObjList.Count; i++)
            {
                bingoObjList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(10, bingoObjList[i].GetComponent<RectTransform>().sizeDelta.y);
            }
            autoOff = false;
            AutoResultVeiwOn = true;
        }

        if (handResultVeiwOn)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= lastTime)
            {
                //GameScore.ShowResult1(resultVeiwOn, rowNumber, ColumnNumber);
                if (handResultVeiwOn)
                {
                    for (int i = 0; i < rowNumber - 1; i++)
                    {
                        List<int> currentRowList = new();
                        for (int j = 0; j < GameScore.resultList.Count; j++)
                        {
                            int currentResultListIndex = GameScore.resultList[j][i];
                            currentRowList.Add(currentResultListIndex);
                        }

                        List<int> currentRowScoreboard = new() { 0, 0, 0, 0, 0, 0 };
                        for (int k = 0; k < currentRowList.Count; k++)
                        {
                            if (currentRowList[k] == 7) currentRowScoreboard[0]++;
                            else if (currentRowList[k] == 1) currentRowScoreboard[1]++;
                            else if (currentRowList[k] == 2) currentRowScoreboard[2]++;
                            else if (currentRowList[k] == 3) currentRowScoreboard[3]++;
                            else if (currentRowList[k] == 4) currentRowScoreboard[4]++;
                            else if (currentRowList[k] == 5) currentRowScoreboard[5]++;
                        }

                        for (int m = 0; m < currentRowScoreboard.Count; m++)
                        {
                            if (currentRowScoreboard[m] == ColumnNumber)
                            {
                                bingoObjList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(WindowWidth - space, bingoObjList[i].GetComponent<RectTransform>().sizeDelta.y);
                            }
                        }

                        GameScore.SlotMachineResult1(rowNumber, currentRowScoreboard, ColumnNumber);

                        currentRowScoreboard.Clear();
                        currentRowList.Clear();
                    }
                    GameScore.resultList.Clear();
                    GameScore.OneRound = false;

                    if (GameScore.KeepSevenNumber >= 3) GameScore.OnSevenColorRed = true;
                    GameScore.KeepSevenNumber = 0;

                    currentTime = cleanTime;
                    handResultVeiwOn = false;
                }
            }
        }

        if (AutoResultVeiwOn)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= lastTime)
            {
                if (AutoResultVeiwOn)
                {
                    for (int i = 0; i < rowNumber - 1; i++)
                    {
                        List<int> currentRowList = new();
                        for (int j = 0; j < GameScore.resultList.Count; j++)
                        {
                            int currentResultListIndex = GameScore.resultList[j][i];
                            currentRowList.Add(currentResultListIndex);
                        }

                        List<int> currentRowScoreboard = new() { 0, 0, 0, 0, 0, 0 };
                        for (int k = 0; k < currentRowList.Count; k++)
                        {
                            if (currentRowList[k] == 7) currentRowScoreboard[0]++;
                            else if (currentRowList[k] == 1) currentRowScoreboard[1]++;
                            else if (currentRowList[k] == 2) currentRowScoreboard[2]++;
                            else if (currentRowList[k] == 3) currentRowScoreboard[3]++;
                            else if (currentRowList[k] == 4) currentRowScoreboard[4]++;
                            else if (currentRowList[k] == 5) currentRowScoreboard[5]++;
                        }

                        for (int m = 0; m < currentRowScoreboard.Count; m++)
                        {
                            if (currentRowScoreboard[m] == ColumnNumber)
                            {
                                bingoObjList[i].GetComponent<RectTransform>().sizeDelta = new Vector2(WindowWidth - space, bingoObjList[i].GetComponent<RectTransform>().sizeDelta.y);
                            }
                        }

                        GameScore.SlotMachineResult1(rowNumber, currentRowScoreboard, ColumnNumber);

                        currentRowScoreboard.Clear();
                        currentRowList.Clear();
                    }

                    for (int i = 0; i < bingoObjList.Count; i++)
                    {
                        if (bingoObjList[i].GetComponent<RectTransform>().sizeDelta.x > 10)
                        {
                            autoOff = true;
                            AutoResultVeiwOn = false;
                            break;
                        }
                        Invoke("RunAuto", 2f);
                    }

                    GameScore.resultList.Clear();
                    GameScore.OneRound = false;

                    if (GameScore.KeepSevenNumber >= 3) GameScore.OnSevenColorRed = true;
                    GameScore.KeepSevenNumber = 0;

                    currentTime = cleanTime;
                }
            }
        }
    }
    public void RunAuto()
    {
        if (!autoOff)
        {
            GameScore.SetAutoPlay(true);
        }
    }
}
