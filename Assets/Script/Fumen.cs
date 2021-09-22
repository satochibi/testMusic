using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Fumen : MonoBehaviour
{
    //ノーツを移動させるスクリプト
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    GameObject audioGameObj;

    [SerializeField]
    AudioClip[] tapAudioClip;

    [SerializeField]
    AudioClip normalTapAudioClip;
    [SerializeField]
    bool autoPlay = true;

    [SerializeField]
    TapL[] tapLList;

    [SerializeField]
    float Playtime;
    GameSystem system;
    public float Speed { get { return this.speed; } }

    public bool IsGameStart { get { return this.isGameStart; } }

    public float GameStartTime { get { return this.gameStartTime; } }

    Rigidbody rb;
    bool isGameStart;
    float gameStartTime;
    AudioSource tapAudioSource;

    List<GameObject> notesList;
    //判定の間隔（単位（秒））
    readonly float[] judgeStep =
    {
        0.05f,  //パーフェクト判定間隔
        0.1f,   //グレイト判定間隔
        0.15f,  //グッド判定間隔
        0.2f    //バッド判定間隔
    
    };
    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tapAudioSource = this.GetComponent<AudioSource>();
        this.isGameStart = false;
        audioGameObj.GetComponent<AudioSource>().Stop();
        system = GameObject.Find("GameManager").GetComponentInChildren<GameSystem>();
    }

    //時間でのノーツ判定
    void FixedUpdate()
    {
        if (!this.IsGameStart)
        {
            return;
        }
        Playtime = Time.fixedTime - this.GameStartTime;
        CheckOverNotes();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 1; i < 6; i++)
            {
                Judge(i);
            }
       }
        Debug.Log(Input.touches.Length);
        if (autoPlay)
        {
            AutoPlay(Playtime);

        }
        else
        {


        }


    }

    public void CheckOverNotes()
    {

        for (int index = 0; index < notesList.Count; index++)
        {
            float n_time = notesList[index].GetComponent<NotesController>().NotesTime;
            if (n_time - Playtime < -0.2f)
            {

                if (!notesList[index].GetComponent<NotesController>().IsTapped)
                {
                    Destroy(notesList[index]);
                    system.AddResultPalam(JudgementType.Miss);
                    notesList.RemoveAt(0);
                    continue;
                }
                else
                {
                    Destroy(notesList[index]);
                    notesList.RemoveAt(0);
                    continue;
                }
            }

        }

    }
    public void Judge(int track)
    {
        //Debug.Log("Judge:" + Playtime);


        for (int index = 0; index < notesList.Count; index++)
        {
            NotesController noteCon = notesList[index].GetComponent<NotesController>();

            if (noteCon.NotesTrack == (Track)track)
            {
                float n_time = noteCon.NotesTime;
                //ノーツの判定時間と現在の経過時間の差
                float timediff = Mathf.Abs(n_time - Playtime);
                if (timediff <= judgeStep[(int)JudgementType.Bad])
                {
                    notesList[index].GetComponent<MeshRenderer>().enabled = false;
                    noteCon.IsTapped = true;

                    switch (timediff)
                    {
                        //誤差0.05秒
                        case float i when i < judgeStep[(int)JudgementType.Perfect]:

                            system.AddResultPalam(JudgementType.Perfect);
                            tapAudioSource.PlayOneShot(this.normalTapAudioClip);

                            //notesList.RemoveAt(0);
                            //Destroy(notesList[index]);
                            return;
                        //誤差0.1秒
                        case float i when i < judgeStep[(int)JudgementType.Great]:
                            system.AddResultPalam(JudgementType.Great);
                            tapAudioSource.PlayOneShot(this.normalTapAudioClip);
                            //Destroy(notesList[index]);
                            //notesList.RemoveAt(0);
                            return;
                        //誤差0.15秒
                        case float i when i < judgeStep[(int)JudgementType.Good]:
                            system.AddResultPalam(JudgementType.Good);
                            tapAudioSource.PlayOneShot(this.normalTapAudioClip);
                            //Destroy(notesList[index]);
                            //notesList.RemoveAt(0);

                            return;
                        //誤差0.2秒
                        case float i when i < judgeStep[(int)JudgementType.Bad]:
                            system.AddResultPalam(JudgementType.Bad);
                            tapAudioSource.PlayOneShot(this.normalTapAudioClip);
                            // Destroy(notesList[index]);
                            //notesList.RemoveAt(0);

                            return;

                        default:
                            // system.AddResultPalam(JudgementType.Miss);

                            break;

                    }
                }
            }
        }
    }
    void AutoPlay(float time)
    {
        for (int index = 0; index < notesList.Count; index++)
        {
            if (time >= notesList[index].GetComponent<NotesController>().NotesTime)
            {
                Debug.Log(notesList[index].transform.position.z);
                switch (notesList[index].GetComponent<NotesController>().NotesTrack)
                {
                    case Track.track1:

                        tapAudioSource.PlayOneShot(this.tapAudioClip[0]);
                        this.tapLList[0].Tap();
                        system.AddResultPalam(JudgementType.Perfect);
                        this.tapLList[0].Exit();
                        break;
                    case Track.track2:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[1]);
                        this.tapLList[1].Tap();
                        this.tapLList[1].Exit();
                        system.AddResultPalam(JudgementType.Perfect);
                        break;
                    case Track.track3:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[2]);
                        this.tapLList[2].Tap();
                        this.tapLList[2].Exit();
                        system.AddResultPalam(JudgementType.Perfect);
                        break;
                    case Track.track4:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[3]);
                        this.tapLList[3].Tap();
                        this.tapLList[3].Exit();
                        system.AddResultPalam(JudgementType.Perfect);
                        break;
                    case Track.track5:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[4]);
                        this.tapLList[4].Tap();
                        this.tapLList[4].Exit();
                        system.AddResultPalam(JudgementType.Perfect);
                        break;
                    default:
                        break;
                }

                //removeAt(0)で先頭の要素を削除
                notesList.RemoveAt(0);
                index = 0;
                continue;
            }
            else
            {
                break;
            }
        }
    }

    public void GameStart(GameObject sender)
    {
        this.isGameStart = true;
        sender.SetActive(false);
        rb.AddForce(transform.forward * -speed, ForceMode.VelocityChange);
        audioGameObj.GetComponent<AudioSource>().Play();
        this.gameStartTime = Time.fixedTime;

        //tagが"note"のゲームオブジェクトをすべて検索
        GameObject[] notesGameObjArray = GameObject.FindGameObjectsWithTag("note");
        List<GameObject> notesGameObjList = new List<GameObject>(notesGameObjArray);

        //noteGameObjListをnoteTimeで昇順になるように並び替える
        notesList = notesGameObjList.OrderBy(x => x.GetComponent<NotesController>().NotesTime).ToList();


    }


}
