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
    List<Ray> rays = new List<Ray>();
    void Start()
    {
        tapText = tapTextGameObj.GetComponent<Text>();
        
        //if (Physics.Raycast(ray, out hit, 10.0f))
        //{
        //    Debug.Log(hit.collider.gameObject.transform.position);
        //}
    }
    public void MousePointerRaycastTest()
    {
        Ray mouseray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit mhit = default;
        if (Physics.Raycast(mouseray, out mhit))
        {
            mhit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;

            Debug.Log(mhit.collider.gameObject.transform.position + Environment.NewLine);
            Debug.Log("マウス座標"+Input.mousePosition.ToString());
        }
        
    }

    public void SingleTouchTest()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPos = touch.position;
        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit =new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;

            //Debug.Log(hit.collider.gameObject.transform.position);
            //int hitNum = Physics.RaycastNonAlloc(ray,hit);
        }

        switch (touch.phase)
        {

            // Record initial touch position.
            case TouchPhase.Began:
                tapText.text = "開始地点" + touch.position.ToString() + Environment.NewLine;
                //Debug.Log("Began" + touch.position);
                break;

            // Determine direction by comparing the current touch position with the initial one.
            case TouchPhase.Moved:
                tapText.text = "移動中" + touch.position.ToString() + Environment.NewLine;
                //Debug.Log("Moved" + touch.position);
                break;

            // Report that a direction has been chosen when the finger is lifted.
            case TouchPhase.Ended:
                tapText.text = "離した" + touch.position.ToString() + Environment.NewLine;

                break;
        }
    }
    void Update()
    {
        // Track a single touch as a direction control.
        //タッチ取得
        var touchCount = Input.touchCount;

        
        if (touchCount > 0)
        {
            SingleTouchTest();
        }

        MousePointerRaycastTest();


        if (touchCount == 0)
        {
            touches.Clear();
            messages.Clear();
        }

        for (int i = 0; i < touchCount; i++)
        {
            touches.Add(Input.GetTouch(i));


        }

        //if (Physics.Raycast(ray, out hit, 40.0f))
        //{
        //    hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
        //    //Debug.Log(hit.collider.gameObject.transform.position);
        //    rays.Add(ray);
        //}
        //タッチ情報格納

        //for (int i = 0; i < touches.ToArray().Length; i++)
        //{
        //    RaycastHit hit;
        //    if (Physics.Raycast(rays[i], out hit, 40.0f))
        //    {
        //        hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;

        //    }
        //    Touch touch = touches[i];
        //    string aMessage = "";
        //    // Handle finger movements based on touch phase.
        //    switch (touch.phase)
        //    {

        //        // Record initial touch position.
        //        case TouchPhase.Began:
        //            aMessage = "開始地点: " + touch.position.ToString() + Environment.NewLine;
        //            //Debug.Log("Began" + touch.position);
        //            break;

        //        // Determine direction by comparing the current touch position with the initial one.
        //        case TouchPhase.Moved:
        //            aMessage = "移動中" + touch.position.ToString() + Environment.NewLine;
        //            //Debug.Log("Moved" + touch.position);
        //            break;

        //        // Report that a direction has been chosen when the finger is lifted.
        //        case TouchPhase.Ended:
        //            //aMessage = "離した" + touch.position.ToString() + Environment.NewLine;
        //            int index = touches.FindIndex((anotherTouch) => anotherTouch.fingerId == touch.fingerId);
        //            //touches.RemoveAt(index);
        //            // messages.RemoveAt(index);
        //            tapTextGameObj.transform.Translate(0.0f,2.0f,0.0f);
        //            //rays.RemoveAt(index);
        //            //Debug.Log("Ended" + touch.position);
        //            break;
        //    }

        //    messages.Add(aMessage);
        //}

        ////メッセージ表示
        //string resultMessage = "";
        //foreach(string message in messages)
        //{
        //    resultMessage += message;
        //}
        //tapText.text = resultMessage;




    }

    }
