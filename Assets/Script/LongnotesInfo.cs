using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LongnotesInfo : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private GameObject longlinepre;

    GameObject obj;

    public void SetPoint(int arraynum, Transform point)
    {
        //�z��ɑ��
        points[arraynum] = point;
    }

    public void SetPointSize(int size)
    {
        //�z��̃T�C�Y�̌���
        Array.Resize(ref points, size);

    }
    //Start�̑���
    public void Init(GameObject parentGameObj)
    {
        //�����O���C������
        obj = Instantiate(longlinepre, Vector3.zero, Quaternion.identity);
        obj.transform.parent = parentGameObj.transform;
        // obj.GetComponent<Longline>().SetupLine(points);
    }

    public void DrawLine()
    {
        obj.GetComponent<Longline>().SetupLine(points);
    }


}
