using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotesTimeAndPosCalc
{
    public static float CalcNotesTime(int BPM, int offset, int samplingRate, int LPB, int num)
    {
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

        return noteTimeWithOffset;
    }

   public static float CalcZPosition(int BPM, int offset, int samplingRate, int LPB, int num, float speed)
    {
        //オフセットを加味したノーツの時刻(秒)
        float noteTimeWithOffset = CalcNotesTime(BPM, offset, samplingRate, LPB, num);

        //オフセットを加味したノーツの時刻とspeedから、transform.position.z(距離)の値を求める
        float distance = speed * noteTimeWithOffset;

        return distance;
    }
}
