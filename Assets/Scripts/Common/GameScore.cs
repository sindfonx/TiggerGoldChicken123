// 乾淨 不要有任何雜質
//Str
//Daytime



//靜態共用 可以共用紀錄的文本 沒有任何意義
//動態生成 共用相同腳本物件可複用

//最底層的東西
using Assets.scripts.AnimImg1;
using System.Collections.Generic;

public static class GameScore 
{
    public static int Score;
    public static bool  viewText;

    public static List<List<int>> resultList = new();
    public static bool OneRound;
    public static bool OnSevenColorRed;
    public static int KeepSevenNumber;

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

    public static void SlotMachineResult1(int rowNumber, List<int> scoreboard, int columnNumber)
    {
        // 結果是這個 就好!
        //TODO: 盡可能英文 不然會影響到 GIT

            if (scoreboard[0] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 最大獎");
        else if (scoreboard[1] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 一獎");
        else if(scoreboard[2] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 二獎");
        else if(scoreboard[3] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 三獎");
        else if(scoreboard[4] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 四獎");
        else if(scoreboard[5] == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 五獎");
            else UnityEngine.Debug.Log("恭喜獲得 : 無獎");
        
    }
    public static void ShowResult1(bool resultOff, int rowNumber, int columnNumber)
    {
        //惰性初始
        if (slotMachineResultParser == null) slotMachineResultParser = new ResultParser();

        slotMachineResultParser.SlotMachineResult1(resultOff, rowNumber, columnNumber, ref resultList, ref OneRound, ref KeepSevenNumber, ref OnSevenColorRed);
    }

}
