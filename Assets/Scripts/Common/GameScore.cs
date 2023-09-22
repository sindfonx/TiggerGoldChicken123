using Assets.scripts.AnimImg1;
using System.Collections.Generic;

public static class GameScore 
{
    public static int Score;
    public static bool  viewText;

    public static List<List<int>> SlotMachineColunmList = new();
    public static bool SlotMachineUnlockOneRound;
    public static bool SlotMachineOnSevenColorRed;
    public static int SlotMachineKeepSevenNumber;

    public static int SlotMachineSetAutoNumber;
    public static bool SlotMachineAutoPlay;
    public static ResultParser slotMachineResultParser;



    // 邏輯比較難做到共用
    public static void MyLog(string printText)
    {
        if (viewText)
        {
            // 要思考一下，可否放入static
            UnityEngine.Debug.Log(printText);
        }
    }
    public static void MyLog(string printText, bool isView)
    {
        // 剪刀石頭布 獲勝判斷 return true false 
        if (isView)
        {
            // 要思考一下，可否放入static
            UnityEngine.Debug.Log(printText);
        }
    }
    public static void MyLog2(string printText)
    {
        // 要思考一下，可否放入static
        // 所有人的 MyLOG2 1不能動
        return;
        UnityEngine.Debug.Log(printText);
    }
    public static void LogDef(bool off, string printText)
    {
        if (!off)
        {
            UnityEngine.Debug.Log(printText);
        }
    }

    public static void SlotMachiineResult(int zere, int one, int two)
    {
        if (zere == 3) UnityEngine.Debug.Log("三獎");
        else if (one == 3) UnityEngine.Debug.Log("愛17");
        else if (two == 3) UnityEngine.Debug.Log("一獎");
        else UnityEngine.Debug.Log("無獎");
    }

    public static void SlotMachineShowResult( List<int> scoreboard, int columnNumber)
    {
        if (scoreboard[0] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 最大獎");
        else if (scoreboard[1] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 一獎");
        else if (scoreboard[2] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 二獎");
        else if (scoreboard[3] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 三獎");
        else if (scoreboard[4] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 四獎");
        else if (scoreboard[5] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 五獎");
        else UnityEngine.Debug.Log("恭喜獲得 : 無獎");
    }

    public static void SetSlotMachineAutoPlay(bool off)
    {
        SlotMachineAutoPlay = off;
    }
    public static void ShowResult1(bool resultOff, int rowNumber, int columnNumber)
    {
        //惰性初始
        if (slotMachineResultParser == null) slotMachineResultParser = new ResultParser();

        slotMachineResultParser.SlotMachineResult1(resultOff, rowNumber, columnNumber, ref SlotMachineColunmList, ref SlotMachineUnlockOneRound, ref SlotMachineKeepSevenNumber, ref SlotMachineOnSevenColorRed);
    }

}
