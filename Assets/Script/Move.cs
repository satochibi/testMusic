using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Move : MonoBehaviour
{
    //ノーツを移動させるスクリプト
    [SerializeField]
    float speed = 10f;

    [SerializeField]
    GameObject audioGameObj;

    [SerializeField]
    AudioClip[] tapAudioClip;

    [SerializeField]
    bool autoPlay = true;

    public float Speed { get { return this.speed; } }

    public bool IsGameStart { get { return this.isGameStart; } }

    public float GameStartTime { get { return this.gameStartTime; } }

    Rigidbody rb;
    bool isGameStart;
    float gameStartTime;
    AudioSource tapAudioSource;

    List<GameObject> notesList;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        tapAudioSource = this.GetComponent<AudioSource>();
        this.isGameStart = false;
    }

    //時間でのノーツ判定
    void FixedUpdate()
    {
        if (!this.IsGameStart)
        {
            return;
        }
        float time = Time.fixedTime - this.GameStartTime;


        if (autoPlay)
        {
            AutoPlay(time);
        }
        else
        {

        }

        
    }

    void AutoPlay(float time)
    {
        for (int index = 0; index < notesList.Count; index++)
        {
            if (time >= notesList[index].GetComponent<NotesController>().NotesTime)
            {
                switch (notesList[index].GetComponent<NotesController>().NotesTrack)
                {
                    case Track.track1:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[0]);
                        break;
                    case Track.track2:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[1]);
                        break;
                    case Track.track3:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[2]);
                        break;
                    case Track.track4:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[3]);
                        break;
                    case Track.track5:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[4]);
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
        audioGameObj.SetActive(true);
        this.gameStartTime = Time.fixedTime;

        //tagが"note"のゲームオブジェクトをすべて検索
        GameObject[] notesGameObjArray = GameObject.FindGameObjectsWithTag("note");
        List<GameObject> notesGameObjList = new List<GameObject>(notesGameObjArray);

        //noteGameObjListをnoteTimeで昇順になるように並び替える
        notesList = notesGameObjList.OrderBy(x => x.GetComponent<NotesController>().NotesTime).ToList();

        
    }


}
