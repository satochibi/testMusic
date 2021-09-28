using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MultiTap : MonoBehaviour
{
    [SerializeField]
    GameObject tapPosGameObj;

    Text tapPosText;
    List<TouchInfo> touches = new List<TouchInfo>();

    // Start is called before the first frame update
    void Start()
    {
        tapPosText = tapPosGameObj.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //�e�L�X�g���Z�b�g
        tapPosText.text = GetTapInfoString();
    }

    /// <summary>
    /// �}���`�^�b�v���������擾
    /// </summary>
    /// <returns>���s��؂�̕�����</returns>
    string GetTapInfoString()
    {
        int touchCount = Input.touchCount;
        touches.Clear();

        //touches�ɂ��ꂼ��A�^�b�`�̏���ǉ�
        for (int i = 0; i < touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            touches.Add(new TouchInfo(touch));
        }

        return string.Join(Environment.NewLine, touches);
    }
}
