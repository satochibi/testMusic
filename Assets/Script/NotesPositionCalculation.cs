using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotesPositionCalculation
{
   public static float CalcZPosition(int BPM, int offset, int samplingRate, int LPB, int num, float speed)
    {
        float result = 0;
        
        //4分音符1拍分が何秒かかるか(時間:秒)
        float secondsPerBeat = 60f / BPM;

        //LPB1回分が何秒かかるか(時間:秒)
        float secondsPerLPB = secondsPerBeat / LPB;

        //ノーツの時刻(秒)
        float notesTime = secondsPerLPB * num;

        //オフセット時間(秒)
        float offsetTimeSec = (float)offset / (float)samplingRate;

        //オフセットを加味したノーツの時刻(秒)
        float noteTimeWithOffset = offsetTimeSec + notesTime;


        //オフセットを加味したノーツの時刻とspeedから、transform.position.z(距離)の値を求める
        float distance = speed * noteTimeWithOffset;

        result = distance;

        return result;
    }
}
