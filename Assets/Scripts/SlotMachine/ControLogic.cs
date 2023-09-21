using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControLogic : MonoBehaviour
{
    public GameObject PerfabColumn;
    public GameObject PerfabBingoLine;
    public GameObject ObjColunmRenge;
    public Button ObjHandButton;
    public Button ObjAutoButton;
    public Button ObjAddButton;
    public Button ObjReduceButton;
    public Text ObjAutoText;

    public float OneRoundTime = 15f;
    public int ColumnNumber;
    private int autoNumber = 0;

    private int WindowWidth;
    private float currentPosX;
    private float space;
    private int rowNumber;

    private const float cleanTime = 0;
    private float currentTime;
    private bool monitorHandReslutOn;
    private bool AutoResultVeiwOn;


    private List<GameObject> bingolinesList = new();
    private bool autoOff;


    private void Start()
    {
        ObjHandButton.onClick.AddListener(PlayHand);
        ObjAutoButton.onClick.AddListener(PlayAuto);
        ObjAddButton.onClick.AddListener(AddAutoNumber);
        ObjReduceButton.onClick.AddListener(ReduceAutoNumber);

        SetAutoText();
        CreateColunmReange();
        CreateBingoLine();
    }

    private void PlayHand()
    {
        CleanBingoLinesWidht();
        monitorHandReslutOn = true;
    }
    private void PlayAuto()
    {
        CleanBingoLinesWidht();
         AutoResultVeiwOn = true;
    }
    private void AddAutoNumber()
    {
        autoNumber++;
    }
    private void ReduceAutoNumber()
    {
        autoNumber--;
    }

    private void SetAutoText()
    {
        if (autoNumber <= 0) ObjAutoText.text = "Auto : Null";
        else ObjAutoText.text = $"Auto : {autoNumber}";
    }
    private void CleanBingoLinesWidht()
    {
        for (int i = 0; i < bingolinesList.Count; i++)
        {
            Vector2 initWidht = new()
            {
                x = 10,
                y = bingolinesList[i].GetComponent<RectTransform>().sizeDelta.y
            };
            bingolinesList[i].GetComponent<RectTransform>().sizeDelta = initWidht;
        }
    }
    private void CreateBingoLine()
    {
        for (int i = 1; i < 10; i += 2)
        {
            Vector2 newPos = new()
            {
                x = 0,
                y = -50 * i
            };

            GameObject newBingoLine = Instantiate(PerfabBingoLine, ObjColunmRenge.GetComponent<Transform>());
            newBingoLine.GetComponent<RectTransform>().anchoredPosition = newPos;
            bingolinesList.Add(newBingoLine);
        }
    }
    private void CreateColunmReange()
    {
        WindowWidth = ColumnNumber * 150;

        currentPosX = -(WindowWidth / 2);

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

            GameObject columnObj = Instantiate(PerfabColumn, ObjColunmRenge.GetComponent<Transform>());
            columnObj.name = $"column{i}";

            rowNumber = columnObj.GetComponent<ColunmAnimLogic>().PerfabTextList.Count;
            columnObj.GetComponent<ColunmAnimLogic>().AnimaTime = lastTime;
            lastTime += 2;

            columnObj.GetComponent<RectTransform>().anchoredPosition = currentPos;
        }

        RectTransform windowSlotRectObj = ObjColunmRenge.GetComponent<RectTransform>();
        windowSlotRectObj.sizeDelta = new Vector2(WindowWidth - space, windowSlotRectObj.sizeDelta.y);
    }




    void Update()
    {
        SetAutoText();

        if (monitorHandReslutOn)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= OneRoundTime)
            {
                //GameScore.ShowResult1(resultVeiwOn, rowNumber, ColumnNumber);
                for (int i = 0; i < rowNumber - 1; i++)
                {
                    SortRowNumber(i, out List<int> resultRowList);
                    AnalyzeResultList(resultRowList, out List<int> scoreList);

                    SetScaleBingoLines(i, scoreList);
                    GameScore.SlotMachineShowResult(scoreList, ColumnNumber);

                    scoreList.Clear();
                    resultRowList.Clear();
                }
                InitGameScoreSlotMachineValue();

                currentTime = cleanTime;
                monitorHandReslutOn = false;
            }
        }

        if (AutoResultVeiwOn)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= OneRoundTime)
            {
                if (AutoResultVeiwOn)
                {
                    for (int i = 0; i < rowNumber - 1; i++)
                    {
                        SortRowNumber(i, out List<int> resultRowList);
                        AnalyzeResultList(resultRowList, out List<int> scoreList);

                        SetScaleBingoLines(i, scoreList);
                        GameScore.SlotMachineShowResult(scoreList, ColumnNumber);

                        scoreList.Clear();
                        resultRowList.Clear();
                    }

                    for (int i = 0; i < bingolinesList.Count; i++)
                    {
                        if (bingolinesList[i].GetComponent<RectTransform>().sizeDelta.x > 10)
                        {
                            AutoResultVeiwOn = false;
                            autoOff = true;
                            break;
                        }
                        Invoke(nameof(RunAuto), 2f);
                    }

                    InitGameScoreSlotMachineValue();

                    currentTime = cleanTime;
                }
            }
        }
    }

    private  void InitGameScoreSlotMachineValue()
    {
        GameScore.SlotMachineColunmList.Clear();
        GameScore.SlotMachineUnlockOneRound = false;

        if (GameScore.SlotMachineKeepSevenNumber >= 3) GameScore.SlotMachineOnSevenColorRed = true;
        GameScore.SlotMachineKeepSevenNumber = 0;
    }

    private void SetScaleBingoLines(int indexRow, List<int> scoreList)
    {
        for (int i = 0; i < scoreList.Count; i++)
        {
            if (scoreList[i] == ColumnNumber)
            {
                Vector2 newPos = new()
                {
                    x = WindowWidth - space,
                    y = bingolinesList[indexRow].GetComponent<RectTransform>().sizeDelta.y
                };
                bingolinesList[indexRow].GetComponent<RectTransform>().sizeDelta = newPos;
            }
        }
    }

    private void AnalyzeResultList(List<int> resultRowList, out List<int> scoreList)
    {
        scoreList = new() { 0, 0, 0, 0, 0, 0 };
        for (int i = 0; i < resultRowList.Count; i++)
        {
            if (resultRowList[i] == 7) scoreList[0]++;
            else if (resultRowList[i] == 1) scoreList[1]++;
            else if (resultRowList[i] == 2) scoreList[2]++;
            else if (resultRowList[i] == 3) scoreList[3]++;
            else if (resultRowList[i] == 4) scoreList[4]++;
            else if (resultRowList[i] == 5) scoreList[5]++;
        }
    }

    private void SortRowNumber(int indexRow, out List<int> resultRowLists)
    {
        resultRowLists = new();
        for (int i = 0; i < GameScore.SlotMachineColunmList.Count; i++)
        {
            int colunmIndex = GameScore.SlotMachineColunmList[i][indexRow];
            resultRowLists.Add(colunmIndex);
        }
    }

    public void RunAuto()
    {
        if (!autoOff)
        {
            GameScore.SetSlotMachineAutoPlay(true);
        }
    }
}
