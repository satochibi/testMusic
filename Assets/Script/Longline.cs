using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longline : MonoBehaviour
{
    //ラインレンダラーの定義
    LineRenderer lineRenderer;
    Transform[] points;

    [SerializeField]
    float lineWidth = 1.4f;

    public void SetupLine(Transform[] points)
    {
        //ロングノーツに配列を渡す
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Length;
        this.points = points;

        //ロングノーツの各点(ワールド座標系)を取得
        Vector3[] positions = this.GetPositions();

        //各点をlineRenderer座標系に変換
        positions = Transformation(positions);

        //各点をlineRendererにセット
        lineRenderer.SetPositions(positions);

        //lineRendererのWidthを変更
        lineRenderer.widthMultiplier = lineWidth;

        //ロングノーツのlineRendererを90度回転させる
        this.transform.Rotate(new Vector3(90f, 0, 0));

    }

    //Transform配列からVector3配列に変換
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

    //Vector3配列(ワールド座標系)からVector3配列(ロングノーツ座標系)へ変換
    Vector3[] Transformation(Vector3[] positions)
    {
        Vector3[] result = new Vector3[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            Vector3 vector = positions[i];
            result[i] = new Vector3(vector.x, vector.z, -(vector.y + 0.01f));
        }

        return result;
    }

}
