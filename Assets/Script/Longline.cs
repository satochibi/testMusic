using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Longline : MonoBehaviour
{
    //���C�������_���[�̒�`
    LineRenderer lineRenderer;
    Transform[] points;

    [SerializeField]
    float lineWidth = 1.4f;

    public void SetupLine(Transform[] points)
    {
        //�����O�m�[�c�ɔz���n��
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = points.Length;
        this.points = points;

        //�����O�m�[�c�̊e�_(���[���h���W�n)���擾
        Vector3[] positions = this.GetPositions();

        //�e�_��lineRenderer���W�n�ɕϊ�
        positions = Transformation(positions);

        //�e�_��lineRenderer�ɃZ�b�g
        lineRenderer.SetPositions(positions);

        //lineRenderer��Width��ύX
        lineRenderer.widthMultiplier = lineWidth;

        //�����O�m�[�c��lineRenderer��90�x��]������
        this.transform.Rotate(new Vector3(90f, 0, 0));

    }

    //Transform�z�񂩂�Vector3�z��ɕϊ�
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

    //Vector3�z��(���[���h���W�n)����Vector3�z��(�����O�m�[�c���W�n)�֕ϊ�
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
