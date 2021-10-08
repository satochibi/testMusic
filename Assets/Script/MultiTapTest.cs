using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiTapTest : MonoBehaviour
{
    [SerializeField]
    GameObject tapTextGameObj;
    [SerializeField]
    GameObject tapInfoTextObj;
    [SerializeField]
    GameObject fumenObj;
    Text tapText;
    Text tapInfoText;
    Fumen fumen;
    [SerializeField]
    bool IsDebug;
    List<string> tapsLaneNames = new List<string>();
    readonly string[] laneNames =
    {
        "Lane1",
        "Lane2",
        "Lane3",
        "Lane4",
        "Lane5"

    };
    readonly Color[] tapColors =
    {
        Color.red,
        Color.blue,
        Color.yellow,
        Color.green,
        Color.black
    };

    //List<Touch> touches = new List<Touch>();
    //List<string> messages = new List<string>();
    //List<Ray> rays = new List<Ray>();


    void Start()
    {
        if (IsDebug)
        {
            tapText = tapTextGameObj.GetComponent<Text>();
            tapInfoText = tapInfoTextObj.GetComponent<Text>();
        }
        fumen = fumenObj.GetComponent<Fumen>();
        //Time.timeScale *= 0.5f;
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
            GameObject mhitOBJ = mhit.collider.gameObject;
            if (mhit.collider.gameObject.tag == "lane")
            {
                mhit.collider.gameObject.GetComponent<LaneController>().ChangeColor(Color.black);
                tapText.text = mhit.collider.gameObject.name;
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    //for (int j = 0; j < laneNames.Length; j++)
                    //{
                    //    if (mhitOBJ.name == laneNames[j])
                    //    {
                    //        fumen.Judge(j + 1);
                    //        //tapInfoText.text = "judge" + Time.deltaTime;
                    //    }
                    //}
                    mhitOBJ.GetComponent<LaneController>().GoJudge(NotesType.Normal);

                }
                
                if ( Input.GetMouseButtonUp(0))
                {
                    //for (int j = 0; j < laneNames.Length; j++)
                    //{
                    //    if (mhitOBJ.name == laneNames[j])
                    //    {
                    //        fumen.LongNoteJudge(j + 1);

                    //        //tapInfoText.text = "judge" + Time.deltaTime;
                    //    }
                    //}
                    mhitOBJ.GetComponent<LaneController>().GoJudge(NotesType.LongEnd);

                }
                if (Input.GetMouseButton(0))
                {
                    //for (int j = 0; j < laneNames.Length; j++)
                    //{
                    //    if (mhitOBJ.name == laneNames[j])
                    //    {
                    //        fumen.LongNotesEventJudge(j + 1);
                    //        //tapInfoText.text = "judge" + Time.deltaTime;
                    //    }
                    //}
                    mhitOBJ.GetComponent<LaneController>().GoJudge(NotesType.Long);

                }
            }

            //mhit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
            //mhit.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
            //Debug.Log(mhit.collider.gameObject.transform.position + Environment.NewLine);
            //Debug.Log("マウス座標" + Input.mousePosition.ToString());
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
            hit.collider.gameObject.GetComponent<LaneController>().ChangeColor(Color.black);

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
        if (IsDebug)
        {
            tapText.text = default;
        }
        
        for (int touchIndex = 0; touchIndex < count; touchIndex++)
        {

            Touch multiTap = Input.GetTouch(touchIndex);
            Ray ray = Camera.main.ScreenPointToRay(multiTap.position);
            RaycastHit hit = new RaycastHit();


            if (Physics.Raycast(ray, out hit))
            {

                GameObject hitOBJ = hit.collider.gameObject;
                if (hitOBJ.tag == "lane")
                {
                    tapsLaneNames.Add(hitOBJ.name + Environment.NewLine);
                    hitOBJ.GetComponent<LaneController>().ChangeColor(tapColors[touchIndex % 5]);
                    switch (multiTap.phase)
                    {
                        // Record initial touch position.
                        case TouchPhase.Began:

                            //for (int j = 0; j < laneNames.Length; j++)
                            //{
                            //    if (hitOBJ.name == laneNames[j])
                            //    {
                            //        fumen.Judge(j + 1);
                            //    }
                            //}
                            hitOBJ.GetComponent<LaneController>().GoJudge(NotesType.Normal);
                            break;

                        // Determine direction by comparing the current touch position with the initial one.
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            hitOBJ.GetComponent<LaneController>().GoJudge(NotesType.Long);
                            break;
                        // Report that a direction has been chosen when the finger is lifted.
                        case TouchPhase.Ended:

                            //for (int j = 0; j < laneNames.Length; j++)
                            //{
                            //    if (hitOBJ.name == laneNames[j])
                            //    {
                            //        //ここに離したかを判別するフラグをtrueにするコード書く。
                            //        fumen.LongNoteJudge(j+1);
                            //    }
                            //}
                            hitOBJ.GetComponent<LaneController>().GoJudge(NotesType.LongEnd);
                            break;
                    }



                }

            }

        }
        foreach (string names in tapsLaneNames)
        {
            if (IsDebug)
            {
                tapText.text += names;
            }
        }

        tapsLaneNames.Clear();

    }

    public void ReLoad(string nextSceneName)
    {
        GameSystem game = GameObject.Find("GameManager").GetComponent<GameSystem>();
        if (nextSceneName == "SampleScene" || nextSceneName == "MultiTapTest")
        {
            string titlename = game.result.musicTitle;
            Difficulty difficulty = game.result.difficulty;
            game.InitializedPalam();
            game.result.musicTitle = titlename;
            game.result.difficulty = difficulty;
        }
        if (nextSceneName == "Result")
        {
            game.IsEnd = true;
        }
        else
        {
            game.ChangeScene(nextSceneName);
        }
    }
    void Update()
    {
        // Track a single touch as a direction control.
        //タッチ取得
        var touchCount = Input.touchCount;


        if (touchCount > 0)
        {
            MultiTapDebugDisp(touchCount);
            //SingleTouchTest();
        }


        //MousePointerRaycastTest();

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
