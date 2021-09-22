using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.IO;

public class MusicSelect : MonoBehaviour
{

   
    //public string[] names;
    //musicNodeのプレファブ
    [SerializeField]
    GameObject listItemPre;
    //生成先の親オブジェクト
    [SerializeField]
    GameObject contentOBJ;
    //nodeのリスト
    List<GameObject> nodeObjList =new List<GameObject>() ;

    //public List<Humen> musicList;
    List<string> musicnames =new List<string>();

    Vector3 pos =default;

    public void EraceList()
    {
        foreach(GameObject obj in nodeObjList)
        {
            Destroy(obj);
        }
    }
    //ResourceのNoteJson内に入ってる曲名をすべて取得。
    //＜注意＞NoteJson内には譜面データ以外のJsonファイルを入れないこと
    public static void SearchMusicNamesFromResources(List<string> names)
    {
         
        TextAsset[] textassets = Resources.LoadAll<TextAsset>("NoteJson/");
       
        names.Add(JsonUtility.FromJson<Humen>(textassets[0].ToString()).name);


        for (int i = 1; i < textassets.Length; i++)
        {

            if (!names.Contains(JsonUtility.FromJson<Humen>(textassets[i].ToString()).name))
            {

               names.Add(JsonUtility.FromJson<Humen>(textassets[i].ToString()).name);

            }

        }
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        
        SearchMusicNamesFromResources(musicnames);
        for (int i = 0; i < musicnames.ToArray().Length; i++)
        {

            //names[i] = Path.GetFileNameWithoutExtension(names[i]);
            GameObject m_obj = Instantiate(listItemPre, pos, Quaternion.identity);
            //m_obj.transform.parent = contentOBJ.transform;
            m_obj.transform.SetParent(contentOBJ.transform, false);
            nodeObjList.Add(m_obj);
            nodeObjList[i].transform.Find("Musicname").GetComponent<Text>().text = musicnames[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            EraceList();
        }
    }
}
