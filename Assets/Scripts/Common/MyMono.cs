using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �p�u�� �i�@��
public class MyMono : MonoBehaviour 
{
    public bool onOffDebug;

    public void MyLog(string printText)
    {
        if (onOffDebug)
        {
            // �n��Ҥ@�U�A�i�_��Jstatic
            GameScore.MyLog2(printText);
            return;
            UnityEngine.Debug.Log(printText);
        }
    }
    private void MyLog123(string printText)
    {
        if (onOffDebug)
        {
            // �n��Ҥ@�U�A�i�_��Jstatic
            GameScore.MyLog2(printText);
            return;
            UnityEngine.Debug.Log(printText);
        }
    }
    //protected
}
