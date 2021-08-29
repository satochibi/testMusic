using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotesTimeAndPosCalc
{
    public static float CalcNotesTime(int BPM, int offset, int samplingRate, int LPB, int num)
    {
        //4������1���������b�����邩(����:�b)
        float secondsPerBeat = 60f / BPM;

        //LPB1�񕪂����b�����邩(����:�b)
        float secondsPerLPB = secondsPerBeat / LPB;

        //�m�[�c�̎���(�b)
        float notesTime = secondsPerLPB * num;

        //�I�t�Z�b�g����(�b)
        float offsetTimeSec = (float)offset / (float)samplingRate;

        //�I�t�Z�b�g�����������m�[�c�̎���(�b)
        float noteTimeWithOffset = offsetTimeSec + notesTime;

        return noteTimeWithOffset;
    }

   public static float CalcZPosition(int BPM, int offset, int samplingRate, int LPB, int num, float speed)
    {
        //�I�t�Z�b�g�����������m�[�c�̎���(�b)
        float noteTimeWithOffset = CalcNotesTime(BPM, offset, samplingRate, LPB, num);

        //�I�t�Z�b�g�����������m�[�c�̎�����speed����Atransform.position.z(����)�̒l�����߂�
        float distance = speed * noteTimeWithOffset;

        return distance;
    }
}
