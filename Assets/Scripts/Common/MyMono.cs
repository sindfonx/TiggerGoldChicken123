using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 小工具 可共用
public class MyMono : MonoBehaviour 
{
    public bool onOffDebug;

    public void MyLog(string printText)
    {
        if (onOffDebug)
        {
            // 要思考一下，可否放入static
            GameScore.MyLog2(printText);
            return;
            UnityEngine.Debug.Log(printText);
        }
    }
    private void MyLog123(string printText)
    {
        if (onOffDebug)
        {
            // 要思考一下，可否放入static
            GameScore.MyLog2(printText);
            return;
            UnityEngine.Debug.Log(printText);
        }
    }
    //protected
}
