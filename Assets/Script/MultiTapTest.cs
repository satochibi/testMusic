using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MultiTapTest : MonoBehaviour
{
    [SerializeField]
    GameObject tapTextGameObj;


    Text tapText;
    List<Touch> touches = new List<Touch>();
    List<string> messages = new List<string>();

    void Start()
    {
        tapText = tapTextGameObj.GetComponent<Text>();

    }
    void Update()
    {
        // Track a single touch as a direction control.
        //�^�b�`�擾
        var touchCount = Input.touchCount;

        if (touchCount == 0)
        {
            touches.Clear();
            messages.Clear();
        }

        for (int i = 0; i < touchCount; i++)
        {
            touches.Add(Input.GetTouch(i));
        }

        //�^�b�`���i�[

        for (int i = 0; i < touches.ToArray().Length; i++)
        {
            Touch touch = touches[i];
            string aMessage = "";
            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    aMessage = "�J�n�n�_: " + touch.position.ToString() + Environment.NewLine;
                    //Debug.Log("Began" + touch.position);
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    aMessage = "�ړ���" + touch.position.ToString() + Environment.NewLine;
                    //Debug.Log("Moved" + touch.position);
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    //aMessage = "������" + touch.position.ToString() + Environment.NewLine;
                    int index = touches.FindIndex((anotherTouch) => anotherTouch.fingerId == touch.fingerId);
                    touches.RemoveAt(index);
                    messages.RemoveAt(index);

                    //Debug.Log("Ended" + touch.position);
                    break;
            }

            messages.Add(aMessage);
        }

        //���b�Z�[�W�\��
        string resultMessage = "";
        foreach(string message in messages)
        {
            resultMessage += message;
        }
        tapText.text = resultMessage;
        



    }

}
