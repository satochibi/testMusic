using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longline : MonoBehaviour
{
    //ラインレンダラーの定義
    private LineRenderer lineRenderer;
    private Transform[] points;
    
    private void Start()
    {
        //lineRenderer = GetComponent<LineRenderer>();

    }
    public void SetupLine(Transform[] points)
    {
        //ロングノーツに配列を渡す
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Length;
        this.points = points;

        Vector3[] positions = this.GetPositions();
        lineRenderer.SetPositions(positions);
        
    }


    Vector3[] GetPositions()
    {
        Vector3[] result = new Vector3[this.points.Length];
        for (int i = 0; i < this.points.Length; i++)
        {
            Vector3 vector = this.points[i].position;
            result[i] = vector;
        }

        return result;
    }
    
}
