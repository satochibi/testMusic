using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MultiTapTest : MonoBehaviour
{
    [SerializeField]
    GameObject tapTextGameObj;
    [SerializeField]
    GameObject tapLaneTextObj;

    Text tapText;
    readonly string[] laneNames =
    {
        "Lane1",
        "Lane2",
        "Lane3",
        "Lane4",
        "Lane5"

    };

    //List<Touch> touches = new List<Touch>();
    //List<string> messages = new List<string>();
    //List<Ray> rays = new List<Ray>();


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
        Debug.DrawRay(mouseray.origin, mouseray.direction * 50.0f, Color.red);

        if (Physics.Raycast(mouseray, out mhit))
        {
            if (mhit.collider.gameObject.tag == "lane")
            {
                mhit.collider.gameObject.GetComponent<ShaderColorChange>().ChangeColor(Color.black);
                tapText.text = mhit.collider.gameObject.name;
            }
            
            //mhit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
            //mhit.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
            //Debug.Log(mhit.collider.gameObject.transform.position + Environment.NewLine);
            Debug.Log("マウス座標" + Input.mousePosition.ToString());
        }
        else
        {
            tapText.text = default;
        }

    }

    public void SingleTouchTest()
    {
        Touch touch = Input.GetTouch(0);
        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            hit.collider.gameObject.GetComponent<ShaderColorChange>().ChangeColor(Color.black);

        }
        //tapText.text = "カメラ座標:" + Camera.main.transform.position + Environment.NewLine;
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
    public void MultiTapDebugDisp(int count)
    {
        tapText.text = default;
        for (int i = 0; i < count; i++)
        {
            Touch multiTap = Input.GetTouch(i);
            Ray ray = Camera.main.ScreenPointToRay(multiTap.position);

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitOBJ = hit.collider.gameObject;
                if (hitOBJ.tag == "lane")
                {
                    switch (multiTap.phase)
                    {

                        // Record initial touch position.
                        case TouchPhase.Began:
                            tapText.text += "開始地点" + multiTap.position.ToString() + Environment.NewLine;
                            for (int j = 0; j < laneNames.Length; j++)
                            {
                                if (hitOBJ.name == laneNames[j])
                                {
                                    hitOBJ.transform.parent.gameObject.GetComponent<LaneTap>().TapPress(i);
                                    tapText.text += multiTap.fingerId + hitOBJ.name + Time.fixedTime.ToString() + Environment.NewLine;
                                }
                            }
                            break;

                        // Determine direction by comparing the current touch position with the initial one.
                        case TouchPhase.Moved:
                            tapText.text += "移動中" + Environment.NewLine;
                            for (int j = 0; j < laneNames.Length; j++)
                            {
                                if (hitOBJ.name == laneNames[j])
                                {
                                    //hitOBJ.transform.parent.gameObject.GetComponent<LaneTap>().TapPress(i, Time.fixedTime);
                                    tapText.text += multiTap.fingerId + hitOBJ.name + Time.fixedTime.ToString() + Environment.NewLine;
                                }

                            }
                            break;
                        case TouchPhase.Stationary:
                            tapText.text += "静止中" + Environment.NewLine;
                            tapText.text += multiTap.fingerId + hitOBJ.name + Time.fixedTime.ToString() + Environment.NewLine;

                            break;
                        // Report that a direction has been chosen when the finger is lifted.
                        case TouchPhase.Ended:
                            tapText.text += "離した" + Environment.NewLine;
                            for (int j = 0; j < laneNames.Length; j++)
                            {
                                if (hitOBJ.name == laneNames[j])
                                {
                                    hitOBJ.transform.parent.gameObject.GetComponent<LaneTap>().TapRelease(i);
                                    tapText.text += multiTap.fingerId + hitOBJ.name + Time.fixedTime.ToString() + Environment.NewLine;
                                }

                            }
                            break;
                    }
                    //switch (hitOBJ.name)
                    //{
                    //    case "Lane1":
                    //        hitOBJ.transform.parent.gameObject.GetComponent<LaneTap>().TapPress(1);
                    //        tapText.text += multiTap.fingerId + ":Lane1:" + Time.fixedTime.ToString() + Environment.NewLine;
                    //        break;
                    //    case "Lane2":
                    //        hitOBJ.transform.parent.gameObject.GetComponent<LaneTap>().TapPress(2);
                    //        tapText.text += multiTap.fingerId + ":Lane2:" + Time.fixedTime.ToString() + Environment.NewLine;
                    //        break;
                    //    case "Lane3":
                    //        hitOBJ.transform.parent.gameObject.GetComponent<LaneTap>().TapPress(3);
                    //        tapText.text += multiTap.fingerId + ":Lane3:" + Time.fixedTime.ToString() + Environment.NewLine;
                    //        break;
                    //    case "Lane4":
                    //        hitOBJ.transform.parent.gameObject.GetComponent<LaneTap>().TapPress(4);
                    //        tapText.text += multiTap.fingerId + ":Lane4:" + Time.fixedTime.ToString() + Environment.NewLine;
                    //        break;
                    //    case "Lane5":
                    //        hitOBJ.transform.parent.gameObject.GetComponent<LaneTap>().TapPress(5);
                    //        tapText.text += multiTap.fingerId + ":Lane5:" + Time.fixedTime.ToString() + Environment.NewLine;
                    //        break;
                    //}
                    hitOBJ.GetComponent<ShaderColorChange>().ChangeColor(Color.black);


                }

            }

        }


    }
    void Update()
    {
        // Track a single touch as a direction control.
        //タッチ取得
        var touchCount = Input.touchCount;


        if (touchCount > 0)
        {
            //MultiTapDebugDisp(touchCount);
            SingleTouchTest();
        }
        

        MousePointerRaycastTest();

        #region errorCode(2021.9.27) 
        //if (touchCount == 0)
        //{
        //    touches.Clear();
        //    messages.Clear();
        //}

        //for (int i = 0; i < touchCount; i++)
        //{
        //    touches.Add(Input.GetTouch(i));


        //}

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

        #endregion


    }

}
