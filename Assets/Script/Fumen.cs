using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Fumen : MonoBehaviour
{
    //ÉmÅ[ÉcÇà⁄ìÆÇ≥ÇπÇÈÉXÉNÉäÉvÉg
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
    public float Speed { get { return this.speed; }  }

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
        audioGameObj.GetComponent<AudioSource>().Stop();
        system = GameObject.Find("GameManager").GetComponentInChildren<GameSystem>();
    }

    //éûä‘Ç≈ÇÃÉmÅ[ÉcîªíË
    void FixedUpdate()
    {
        if (!this.IsGameStart)
        {
            return;
        }
        Playtime = Time.fixedTime - this.GameStartTime;
        CheckOverNotes();
        
        if(Input.GetKeyDown(KeyCode.Space))
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
            if(n_time - Playtime<-0.2f)
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
                
                if (n_time - Playtime <= 0.05)
                {   
                    notesList[index].GetComponent<MeshRenderer>().enabled =false;
                    noteCon.IsTapped = true;

                    //ç∑ÇOÅDÇSÇRÇÃèÍçá8Ç™îªíË
                    switch ((int)(Mathf.Abs(n_time - Playtime) * 20))
                    {
                        //åÎç∑0.05ïb
                        case 0:
                            
                            system.AddResultPalam(JudgementType.Perfect);
                            tapAudioSource.PlayOneShot(this.normalTapAudioClip);

                            //notesList.RemoveAt(0);
                            //Destroy(notesList[index]);
                            return;
                        //åÎç∑0.1ïb
                        case 1:
                            system.AddResultPalam(JudgementType.Great);
                            tapAudioSource.PlayOneShot(this.normalTapAudioClip);
                            //Destroy(notesList[index]);
                            //notesList.RemoveAt(0);
                            return;
                        //åÎç∑0.15ïb
                        case 2:
                            system.AddResultPalam(JudgementType.Good);
                            tapAudioSource.PlayOneShot(this.normalTapAudioClip);
                            //Destroy(notesList[index]);
                            //notesList.RemoveAt(0);

                            return;
                        //åÎç∑0.2ïb
                        case 3:
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

                //removeAt(0)Ç≈êÊì™ÇÃóvëfÇçÌèú
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

        //tagÇ™"note"ÇÃÉQÅ[ÉÄÉIÉuÉWÉFÉNÉgÇÇ∑Ç◊Çƒåüçı
        GameObject[] notesGameObjArray = GameObject.FindGameObjectsWithTag("note");
        List<GameObject> notesGameObjList = new List<GameObject>(notesGameObjArray);

        //noteGameObjListÇnoteTimeÇ≈è∏èáÇ…Ç»ÇÈÇÊÇ§Ç…ï¿Ç—ë÷Ç¶ÇÈ
        notesList = notesGameObjList.OrderBy(x => x.GetComponent<NotesController>().NotesTime).ToList();

        
    }


}
