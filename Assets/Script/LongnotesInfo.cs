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
        //配列に代入
        points[arraynum] = point;
    }

    public void SetPointSize(int size)
    {
        //配列のサイズの決定
        Array.Resize(ref points, size);

    }
    //Startの代わり
    public void Init(GameObject parentGameObj)
    {
        //ロングライン生成
        obj = Instantiate(longlinepre, Vector3.zero, Quaternion.identity);
        obj.transform.parent = parentGameObj.transform;
        // obj.GetComponent<Longline>().SetupLine(points);
    }

    public void DrawLine()
    {
        obj.GetComponent<Longline>().SetupLine(points);
    }


}
