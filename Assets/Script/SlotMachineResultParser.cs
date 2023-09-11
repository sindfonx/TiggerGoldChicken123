using System.Collections.Generic;

namespace Assets.scripts.AnimImg1
{
    // 初始是 private
    public class SlotMachineResultParser
    {
        public  void SlotMachineResult1(bool resultOff, int rowNumber, int columnNumber, ref List<List<int>> resultList, ref bool OneRound, ref int KeepSevenNumber, ref bool OnSevenColorRed)
        {
            if (resultOff)
            {
                for (int i = 0; i < rowNumber - 1; i++)
                {
                    int zeroScore = 0;
                    int oneScore = 0;
                    int twoScore = 0;
                    int threeScore = 0;
                    int fourScore = 0;
                    int fiveScore = 0;


                    List<int> currentList = new();
                    for (int j = 0; j < resultList.Count; j++)
                    {
                        int currentResultListIndex = resultList[j][i];
                        currentList.Add(currentResultListIndex);
                    }

                    for (int k = 0; k < currentList.Count; k++)
                    {
                        if (currentList[k] == 7) zeroScore++;
                        else if (currentList[k] == 1) oneScore++;
                        else if (currentList[k] == 2) twoScore++;
                        else if (currentList[k] == 3) threeScore++;
                        else if (currentList[k] == 4) fourScore++;
                        else if (currentList[k] == 5) fiveScore++;

                    }

                    // 結果是這個 就好!
                    if (zeroScore == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 最大獎");
                    else if (oneScore == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 一獎");
                    else if (twoScore == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 二獎");
                    else if (threeScore == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 三獎");
                    else if (fourScore == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 四獎");
                    else if (fiveScore == columnNumber) UnityEngine.Debug.Log("恭喜獲得 : 五獎");


                    else UnityEngine.Debug.Log("恭喜獲得 : 無獎");

                    currentList.Clear();
                }
                resultList.Clear();

                //下下解
                //GameScore.OneRound = false;

                //if (KeepSevenNumber >= 3) GameScore.OnSevenColorRed = true;
                //GameScore.KeepSevenNumber = 0;

                //ref解 
                OneRound = false;

                if (KeepSevenNumber >= 3) OnSevenColorRed = true;
                KeepSevenNumber = 0;

                //TODO: 盡可能英文 不然會影響到 GIT
            }
        }
    }
}
