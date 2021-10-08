using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LongNotesEvent
{
    public float EveTime { get; set; }
    public Track EveTrack { get; set; }
    public int Index { get; set; }

    public LongNotesEvent(float time, Track track, int index)
    {
        EveTime = time;
        EveTrack = track;
        Index = index;
    }
}
public class Fumen : MonoBehaviour
{
    //�m�[�c���ړ�������X�N���v�g
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
    TapLightController[] tapLList;


    Dictionary<string, string> longNoteTypeName = new Dictionary<string, string>()
    {
        { "longNotesStart","�����O�m�[�c�擪"},
        { "longNotes","�����O�m�[�c" },
        { "longNotesEnd","�����O�m�[�c�I�["}
    };

    public Dictionary<string, string> LongNoteTypeName { get { return this.longNoteTypeName; } }
    float playTime;
    public float PlayTime { get { return this.playTime; } }

    GameSystem system;
    public float Speed { get { return this.speed; } }

    public bool IsGameStart { get { return this.isGameStart; } }

    public float GameStartTime { get { return this.gameStartTime; } }

    Rigidbody rb;
    bool isGameStart;
    float gameStartTime;
    AudioSource tapAudioSource;

    List<GameObject> notesList;

    [SerializeField]
    List<LongNotesEvent> longNotesEvent = new List<LongNotesEvent>();


    public void AddLongNotesEvent(LongNotesEvent @event)
    {
        longNotesEvent.Add(@event);
    }
    //����̊Ԋu�i�P�ʁi�b�j�j
    readonly float[] judgeStep =
    {
        0.05f,  //�p�[�t�F�N�g����Ԋu
        0.1f,   //�O���C�g����Ԋu
        0.15f,  //�O�b�h����Ԋu
        0.2f    //�o�b�h����Ԋu
    
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

    //���Ԃł̃m�[�c����
    void FixedUpdate()
    {
        if (!this.IsGameStart)
        {
            return;
        }
        playTime = Time.fixedTime - this.GameStartTime;
        //Playtime = loadtime - this.GameStartTime;
        KeyBoardTap();
        CheckOverNotes();
        //LongNotesEventJudge(1);


        if (autoPlay)
        {
            AutoPlay(playTime);

        }
    


    }

   
    /// <summary>
    /// �f�o�b�O�p�L�[�{�[�h�^�b�v����
    /// </summary>
    public void KeyBoardTap()
    {
        //if (Input.GetKeyDown(KeyCode.G))
        //{

        //    Judge((int)Track.track1);
        //    Debug.Log(longNotesEvent);


        //}
        //if (Input.GetKeyDown(KeyCode.B))
        //{

        //    Judge((int)Track.track2);
        //}
        //if (Input.GetKeyDown(KeyCode.N))
        //{

        //    Judge((int)Track.track3);
        //}
        //if (Input.GetKeyDown(KeyCode.M))
        //{

        //    Judge((int)Track.track4);
        //}
        //if (Input.GetKeyDown(KeyCode.K))
        //{

        //    Judge((int)Track.track5);
        //}
    }
    /// <summary>
    /// ��莞�Ԍo�����m�[�c���폜���鏈��
    /// </summary>
    public void CheckOverNotes()
    {

        for (int index = 0; index < notesList.Count; index++)
        {
            float n_time = notesList[index].GetComponent<NotesController>().NotesTime;
            if (n_time - playTime < -judgeStep[(int)JudgementType.Bad])
            {
                //���肳��Ă��Ȃ�������Miss��������ď�������B
                if (!notesList[index].GetComponent<NotesController>().IsTapped)
                {
                    Destroy(notesList[index]);
                    system.AddResultPalam(JudgementType.Miss);
                    notesList.RemoveAt(0);
                    continue;
                }
                //���肳��Ă����炻�̂܂܏���
                else
                {
                    Destroy(notesList[index]);
                    notesList.RemoveAt(0);
                    continue;
                }
            }

        }

    }
    /// <summary>
    /// �m�[�}���^�b�v���菈��(��Bad�`Perfect�`�xBad)
    ///|0.2�b   |0.15�b  |0.1�b   |0.05�b�`-0.05�b|-0.1�b   |-0.15�b |0.2�b   |
    ///|Bad  �� |Good�@��|Great�� |�@ Perfect�@�� |Great��  |Good��  |Bad     |
    /// </summary>
    /// <param name="track">���̓g���b�N�ԍ�</param>
    public void Judge(int track,NotesType type)
    {
        if (this.isGameStart)
        {
            for (int index = 0; index < notesList.Count; index++)
            {
                NotesController noteCon = notesList[index].GetComponent<NotesController>();
                if(noteCon.Type ==type)
                //if (noteCon.Type != NotesType.LongNormal && noteCon.Type != NotesType.LongEnd)
                //if (notesList[index].name != LongNoteTypeName["longNotes"] && notesList[index].name != LongNoteTypeName["longNotesEnd"])
                {
                    
                    
                    if (noteCon.NotesTrack == (Track)track)
                    {
                        float n_time = noteCon.NotesTime;
                        //�m�[�c�̔��莞�Ԃƌ��݂̌o�ߎ��Ԃ̍�(��Βl)
                        float timediff = Mathf.Abs(n_time - playTime);

                        //����Bad�̔���Ԋu�ȓ��i0.2�b�ȓ��j���Ώۃm�[�c��������̎� 
                        if (timediff <= judgeStep[(int)JudgementType.Bad] && !noteCon.IsTapped)
                        {
                            noteCon.IsTapped = true;
                            notesList[index].GetComponent<MeshRenderer>().enabled = false;
                            //�����������P�[�X����(Perfect�`>...Bad)�������s���֐����I������B
                            switch (timediff)
                            {
                                //��0.05�b�@��Perfect
                                case float i when i <= judgeStep[(int)JudgementType.Perfect]:
                                    system.AddResultPalam(JudgementType.Perfect);
                                    tapAudioSource.PlayOneShot(this.normalTapAudioClip);
                                    return;
                                //��0.1�b    ��Great
                                case float i when i <= judgeStep[(int)JudgementType.Great]:
                                    system.AddResultPalam(JudgementType.Great);
                                    tapAudioSource.PlayOneShot(this.normalTapAudioClip);
                                    return;
                                //��0.15�b   ��Good
                                case float i when i <= judgeStep[(int)JudgementType.Good]:
                                    system.AddResultPalam(JudgementType.Good);
                                    tapAudioSource.PlayOneShot(this.normalTapAudioClip);
                                    return;
                                //��0.2�b    ��Bad
                                case float i when i <= judgeStep[(int)JudgementType.Bad]:
                                    system.AddResultPalam(JudgementType.Bad);
                                    tapAudioSource.PlayOneShot(this.normalTapAudioClip);
                                    return;

                                default:

                                    break;

                            }


                        }
                    }
              
                }


            }
        }
    }
    
  
    //�����O�m�[�c�i�I�[�m�[�c�j
    //public void LongNoteJudge(int track)
    //{
    //    if (this.isGameStart)
    //    {
    //        for (int index = 0; index < notesList.Count; index++)
    //        {
    //            NotesController noteCon = notesList[index].GetComponent<NotesController>();
    //            if (notesList[index].name == "�����O�m�[�c�I�[")
    //            {
    //                if (noteCon.NotesTrack == (Track)track)
    //                {
    //                    float n_time = noteCon.NotesTime;
    //                    //�m�[�c�̔��莞�Ԃƌ��݂̌o�ߎ��Ԃ̍�(��Βl)
    //                    float timediff = Mathf.Abs(n_time - playTime);

    //                    //����Bad�̔���Ԋu�ȓ��i0.2�b�ȓ��j���Ώۃm�[�c��������̎� 
    //                    if (timediff <= judgeStep[(int)JudgementType.Bad] && !noteCon.IsTapped)
    //                    {
    //                        noteCon.IsTapped = true;
    //                        notesList[index].GetComponent<MeshRenderer>().enabled = false;
    //                        //�����������P�[�X����(Perfect�`>...Bad)�������s���֐����I������B
    //                        switch (timediff)
    //                        {
    //                            //��0.05�b�@��Perfect
    //                            case float i when i <= judgeStep[(int)JudgementType.Perfect]:
    //                                system.AddResultPalam(JudgementType.Perfect);
    //                                tapAudioSource.PlayOneShot(this.normalTapAudioClip);
    //                                return;
    //                            //��0.1�b    ��Great
    //                            case float i when i <= judgeStep[(int)JudgementType.Great]:
    //                                system.AddResultPalam(JudgementType.Great);
    //                                tapAudioSource.PlayOneShot(this.normalTapAudioClip);
    //                                return;
    //                            //��0.15�b   ��Good
    //                            case float i when i <= judgeStep[(int)JudgementType.Good]:
    //                                system.AddResultPalam(JudgementType.Good);
    //                                tapAudioSource.PlayOneShot(this.normalTapAudioClip);
    //                                return;
    //                            //��0.2�b    ��Bad
    //                            case float i when i <= judgeStep[(int)JudgementType.Bad]:
    //                                system.AddResultPalam(JudgementType.Bad);
    //                                tapAudioSource.PlayOneShot(this.normalTapAudioClip);
    //                                return;

    //                            default:

    //                                break;

    //                        }


    //                    }
    //                }
    //            }


    //        }
    //    }
    //}
    public void LongNotesEventJudge(int track)
    {
        
        if (this.isGameStart)
        {
            for (int index = 0; index < notesList.Count; index++)
            {
                var noteCon = notesList[index].GetComponent<NotesController>();
                if (noteCon.NotesTrack == (Track)track)
                {
                    if (noteCon.Type == NotesType.Long && !noteCon.IsTapped)
                    {
                        if (playTime >= noteCon.NotesTime)
                        {
                            notesList[index].GetComponent<MeshRenderer>().enabled = false;
                            system.AddResultPalam(JudgementType.Perfect);
                            noteCon.IsTapped = true;
                        }

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
                        this.tapLList[0].LightUp();
                        system.AddResultPalam(JudgementType.Perfect);
                        this.tapLList[0].LightDown();
                        break;
                    case Track.track2:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[1]);
                        this.tapLList[1].LightUp();
                        this.tapLList[1].LightDown();
                        system.AddResultPalam(JudgementType.Perfect);
                        break;
                    case Track.track3:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[2]);
                        this.tapLList[2].LightUp();
                        this.tapLList[2].LightDown();
                        system.AddResultPalam(JudgementType.Perfect);
                        break;
                    case Track.track4:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[3]);
                        this.tapLList[3].LightUp();
                        this.tapLList[3].LightDown();
                        system.AddResultPalam(JudgementType.Perfect);
                        break;
                    case Track.track5:
                        tapAudioSource.PlayOneShot(this.tapAudioClip[4]);
                        this.tapLList[4].LightUp();
                        this.tapLList[4].LightDown();
                        system.AddResultPalam(JudgementType.Perfect);
                        break;
                    default:
                        break;
                }

                //removeAt(0)�Ő擪�̗v�f���폜
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

        //tag��"note"�̃Q�[���I�u�W�F�N�g�����ׂČ���
        GameObject[] notesGameObjArray = GameObject.FindGameObjectsWithTag("note");
        List<GameObject> notesGameObjList = new List<GameObject>(notesGameObjArray);

        //noteGameObjList��noteTime�ŏ����ɂȂ�悤�ɕ��ёւ���
        notesList = notesGameObjList.OrderBy(x => x.GetComponent<NotesController>().NotesTime).ToList();


    }


}
