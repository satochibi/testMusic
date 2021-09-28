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
        //テキストをセット
        tapPosText.text = GetTapInfoString();
    }

    /// <summary>
    /// マルチタップした情報を取得
    /// </summary>
    /// <returns>改行区切りの文字列</returns>
    string GetTapInfoString()
    {
        int touchCount = Input.touchCount;
        touches.Clear();

        //touchesにそれぞれ、タッチの情報を追加
        for (int i = 0; i < touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            touches.Add(new TouchInfo(touch));
        }

        return string.Join(Environment.NewLine, touches);
    }
}
