// ���b ���n����������
//Str
//Daytime



//�R�A�@�� �i�H�@�ά������奻 �S������N�q
//�ʺA�ͦ� �@�άۦP�}������i�ƥ�

//�̩��h���F��
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



    // �޿���������@��
    public static void MyLog(string printText)
    {
        if (viewText)
        {
            // �n��Ҥ@�U�A�i�_��Jstatic
            UnityEngine.Debug.Log(printText);
        }
    }
    public static void MyLog(string printText, bool isView)
    {
        // �ŤM���Y�� ��ӧP�_ return true false 
        if (isView)
        {
            // �n��Ҥ@�U�A�i�_��Jstatic
            UnityEngine.Debug.Log(printText);
        }
    }
    public static void MyLog2(string printText)
    {
        // �n��Ҥ@�U�A�i�_��Jstatic
        // �Ҧ��H�� MyLOG2 1�����
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
        if (zere == 3) UnityEngine.Debug.Log("�T��");
        else if (one == 3) UnityEngine.Debug.Log("�R17");
        else if (two == 3) UnityEngine.Debug.Log("�@��");
        else UnityEngine.Debug.Log("�L��");
    }

    public static void SlotMachineResult1(int rowNumber, List<int> scoreboard, int columnNumber)
    {
        // ���G�O�o�� �N�n!
        //TODO: �ɥi��^�� ���M�|�v�T�� GIT

            if (scoreboard[0] == columnNumber) UnityEngine.Debug.Log("������o : �̤j��");
        else if (scoreboard[1] == columnNumber) UnityEngine.Debug.Log("������o : �@��");
        else if(scoreboard[2] == columnNumber) UnityEngine.Debug.Log("������o : �G��");
        else if(scoreboard[3] == columnNumber) UnityEngine.Debug.Log("������o : �T��");
        else if(scoreboard[4] == columnNumber) UnityEngine.Debug.Log("������o : �|��");
        else if(scoreboard[5] == columnNumber) UnityEngine.Debug.Log("������o : ����");
            else UnityEngine.Debug.Log("������o : �L��");
        
    }
    public static void ShowResult1(bool resultOff, int rowNumber, int columnNumber)
    {
        //�k�ʪ�l
        if (slotMachineResultParser == null) slotMachineResultParser = new ResultParser();

        slotMachineResultParser.SlotMachineResult1(resultOff, rowNumber, columnNumber, ref resultList, ref OneRound, ref KeepSevenNumber, ref OnSevenColorRed);
    }

}
