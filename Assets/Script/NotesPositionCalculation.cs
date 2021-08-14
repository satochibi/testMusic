using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NotesPositionCalculation
{
   public static float CalcZPosition(int BPM, int offset, int samplingRate, int LPB, int num, float speed)
    {
        float result = 0;
        
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


        //�I�t�Z�b�g�����������m�[�c�̎�����speed����Atransform.position.z(����)�̒l�����߂�
        float distance = speed * noteTimeWithOffset;

        result = distance;

        return result;
    }
}
