using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Humen
{
    //Json�t�@�C�����̊�b�f�[�^
    public string name;
    public int maxBlock;
    public int BPM;
    public int offset;
    public Notes[] notes;

}
[Serializable]
public class Notes
{
    //Json�t�@�C�����̃m�[�c�f�[�^
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

    

    //public GameObject lineOBJ;
    // Start is called before the first frame update
    void Start()
    {
        

        float fumenScrollSpeed = fumenGameObj.GetComponent<Fumen>().speed;

        //Json�t�@�C���̓ǂݏo��
        string inputString = Resources.Load<TextAsset>("NoteJson/test1").ToString();
        //Debug.Log(inputString);
        Humen inputJson = JsonUtility.FromJson<Humen>(inputString);

        for (int a = 0; a < inputJson.notes.Length; a++)
        {


            //�V���O���m�[�c�̐����y�єz�u
            //Instantiate(notepref, new Vector3(-4 + inputJson.notes[a].block * 2f, 0f, 40 + inputJson.notes[a].num * 60 / inputJson.BPM * 10f), Quaternion.identity, fumenGameObj.transform);
            notepref.GetComponent<NotesController>().SetTrack((Track)inputJson.notes[a].block + 1);

            float zPosition = NotesPositionCalculation.CalcZPosition(inputJson.BPM, inputJson.offset, 44100, inputJson.notes[a].LPB, inputJson.notes[a].num, fumenScrollSpeed);

            zPosition += MainJPointTransform.position.z;
            //Debug.Log(zPosition);

            GameObject parentlong = Instantiate(notepref, new Vector3(-4 + inputJson.notes[a].block * 2f, 0.5f, zPosition), Quaternion.identity, fumenGameObj.transform);

            //Debug.Log("Num:" + inputJson.notes[a].num + "�@Block:" + inputJson.notes[a].block + "�@A:" + "NoteType" + inputJson.notes[a].type.ToString() + "   " + a);
            if (inputJson.notes[a].type == 2)
            {
                GameObject longnotesGameObjList = new GameObject("LongNotes");
                longnotesGameObjList.transform.parent = fumenGameObj.transform;
                //�����O�m�[�c�ł���Ή��L�̏��������s �����O�m�[�c�̐擪�̃I�u�W�F�N�g�𐶐�
                parentlong.name = "�����O�m�[�c�擪";
                parentlong.transform.parent = longnotesGameObjList.transform;
                GameObject longnote = Instantiate(longnotespref, Vector3.zero, Quaternion.identity, longnotesGameObjList.transform);

                longnote.GetComponent<test1>().Init(longnotesGameObjList);
                
                Notes[] m_note = inputJson.notes[a].notes;
                longnote.GetComponent<test1>().SetPointSize(m_note.Length + 1);
                longnote.GetComponent<test1>().SetPoint(0, parentlong.transform);

                for (int i = 0; i < m_note.Length; i++)
                {
                    //�擪�ȊO�̃����O�m�[�c�̐���
                    //Debug.Log("long" + i);
                    // notepref.transform.parent = Notes.transform;

                    notepref.GetComponent<NotesController>().SetTrack((Track)inputJson.notes[a].block + 1);

                    zPosition = NotesPositionCalculation.CalcZPosition(inputJson.BPM, inputJson.offset, 44100, m_note[i].LPB, m_note[i].num, fumenScrollSpeed);
                    zPosition += MainJPointTransform.position.z;


                    GameObject notes = Instantiate(notepref, new Vector3(-4 + m_note[i].block * 2f, 0.5f, zPosition), Quaternion.identity, longnotesGameObjList.transform);
                    longnote.GetComponent<test1>().SetPoint(i + 1, notes.transform);
                    if (i == m_note.Length - 1)
                    {
                        notes.name = "�����O�m�[�c�I�[";
                    }
                    else
                    {
                        notes.name = "�����O�m�[�c" + i;
                    }
                    //notepoints[i] = gameobject.transform;

                    //longline.SetupLine(notepoints);
                }

                //�����O�m�[�c�̐�������
                longnote.GetComponent<test1>().DrawLine();


            }


        }





    }

}
