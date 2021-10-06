using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Humen
{
    //Jsonファイル内の基礎データ
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Notes[] notes;

}
[Serializable]
public class Notes
{
    //Jsonファイル内のノーツデータ
    public int LPB;
    public int num;
    public int block;
    public int type;
    public Notes[] notes;
}

public class InputJson : MonoBehaviour
{
    //[SerializeField] private Transform[] notepoints;
    //[SerializeField] private Longline longline;
    [SerializeField]
    GameObject fumenGameObj;

    [SerializeField]
    GameObject notepref;
    
    [SerializeField]
    GameObject longnotespref;

    [SerializeField]
    Transform MainJPointTransform;

    [SerializeField]
    public int notesNum;

    [SerializeField]
    string musicName;
    [SerializeField]
    Difficulty difficultType;
    //public GameObject lineOBJ;
    // Start is called before the first frame update
    void Start()
    {
        GameSystem m_system = GameObject.Find("GameManager").GetComponent<GameSystem>();
        Fumen fumen = fumenGameObj.GetComponent<Fumen>();
        string m_name = m_system.GetResultPalam().musicTitle;
        if (string.IsNullOrEmpty(m_name))
        {
            m_name = musicName;
            m_system.result.difficulty = difficultType;
        }
        float fumenScrollSpeed = fumen.Speed;


        //Jsonファイルの読み出し
        string inputString = Resources.Load<TextAsset>("NoteJson/"+m_name+"/"+m_system.result.difficulty.ToString()).ToString();
        //Debug.Log(inputString);
        Humen inputJson = JsonUtility.FromJson<Humen>(inputString);
        int noteIndex =0;
        for (int a = 0; a < inputJson.notes.Length; a++)
        {


            //シングルノーツの生成及び配置
            //Instantiate(notepref, new Vector3(-4 + inputJson.notes[a].block * 2f, 0f, 40 + inputJson.notes[a].num * 60 / inputJson.BPM * 10f), Quaternion.identity, fumenGameObj.transform);
            
            float zPosition = NotesTimeAndPosCalc.CalcZPosition(inputJson.BPM, inputJson.offset, 44100, inputJson.notes[a].LPB, inputJson.notes[a].num, fumenScrollSpeed);

            zPosition += MainJPointTransform.position.z;
            //Debug.Log(zPosition);

            GameObject parentlong = Instantiate(notepref, new Vector3(-4 + inputJson.notes[a].block * 2f, 0.5f, zPosition), Quaternion.identity, fumenGameObj.transform);
            noteIndex++;

            parentlong.GetComponent<NotesController>().NotesTrack = (Track)(inputJson.notes[a].block + 1);
            parentlong.GetComponent<NotesController>().NotesTime = NotesTimeAndPosCalc.CalcNotesTime(inputJson.BPM, inputJson.offset, 44100, inputJson.notes[a].LPB, inputJson.notes[a].num);
            parentlong.GetComponent<NotesController>().Type = NotesType.Normal;

            //Debug.Log("Num:" + inputJson.notes[a].num + "　Block:" + inputJson.notes[a].block + "　A:" + "NoteType" + inputJson.notes[a].type.ToString() + "   " + a);
            if (inputJson.notes[a].type == 2)
            {
                
                GameObject longnotesGameObjList = new GameObject("LongNotes");
                longnotesGameObjList.transform.parent = fumenGameObj.transform;
                //ロングノーツであれば下記の処理を実行 ロングノーツの先頭のオブジェクトを生成
                parentlong.name = "ロングノーツ先頭";
                parentlong.GetComponent<NotesController>().Type = NotesType.Normal;
                parentlong.transform.parent = longnotesGameObjList.transform;
                GameObject longnote = Instantiate(longnotespref, Vector3.zero, Quaternion.identity, longnotesGameObjList.transform);
                
                longnote.GetComponent<LongnotesInfo>().Init(longnotesGameObjList);
                
                Notes[] m_note = inputJson.notes[a].notes;
                longnote.GetComponent<LongnotesInfo>().SetPointSize(m_note.Length + 1);
                longnote.GetComponent<LongnotesInfo>().SetPoint(0, parentlong.transform);

                for (int i = 0; i < m_note.Length; i++)
                {
                    


                    zPosition = NotesTimeAndPosCalc.CalcZPosition(inputJson.BPM, inputJson.offset, 44100, m_note[i].LPB, m_note[i].num, fumenScrollSpeed);
                    zPosition += MainJPointTransform.position.z;


                    GameObject notes = Instantiate(notepref, new Vector3(-4 + m_note[i].block * 2f, 0.5f, zPosition), Quaternion.identity, longnotesGameObjList.transform);
                    noteIndex++;
                    notes.GetComponent<NotesController>().NotesTrack = (Track)(m_note[i].block + 1);
                    notes.GetComponent<NotesController>().NotesTime = NotesTimeAndPosCalc.CalcNotesTime(inputJson.BPM, inputJson.offset, 44100, m_note[i].LPB, m_note[i].num);

                    notesNum++;

                    longnote.GetComponent<LongnotesInfo>().SetPoint(i + 1, notes.transform);
                    if (i == m_note.Length - 1)
                    {
                        notes.name = "ロングノーツ終端";
                        notes.GetComponent<NotesController>().Type = NotesType.LongEnd;


                    }
                    else
                    {
                        notes.name = "ロングノーツ";
                        notes.GetComponent<NotesController>().Type = NotesType.Long;

                     
                    }
                    //notepoints[i] = gameobject.transform;

                    //longline.SetupLine(notepoints);
                }

                //ロングノーツの線を引く
                longnote.GetComponent<LongnotesInfo>().DrawLine();


            }


        }
        //test53:シャイニングスター
        notesNum += inputJson.notes.Length;
        m_system.SetNotesNum(notesNum);
    }

}
