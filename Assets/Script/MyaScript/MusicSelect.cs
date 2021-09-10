using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.IO;

public class MusicSelect : MonoBehaviour
{

    [SerializeField]
    public string[] names;
    public GameObject listItemPre;
    public List<GameObject> m_nameList;
    public GameObject contentOBJ;

    public List<Humen> musicList;
    public string[] musicnames;
    Vector3 pos;

    public void EraceList()
    {
        foreach(GameObject obj in m_nameList)
        {
            Destroy(obj);
        }
    }
    
    //public static string path
    //{
    //    get
    //    {
    //        switch (Application.platform)
    //        {
    //            case RuntimePlatform.IPhonePlayer:
    //                return Application.persistentDataPath+ "/Resources/NoteJson/";

    //            case RuntimePlatform.Android:
    //                return Application.temporaryCachePath+"/Resources/NoteJson/";

    //            case RuntimePlatform.LinuxPlayer:
    //                return Path.Combine(Directory.GetParent(Application.dataPath).FullName, "/Resources/NoteJson/");
    //            default:
    //                return Application.dataPath + "/Resources/NoteJson/";
    //        }
    //    }
    //}
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        //指定フォルダからjsonファイルを一括読み込み
        // string path = Application.dataPath + "/Resources/NoteJson/";
        //names = Directory.GetFiles(@path, "*.json",SearchOption.TopDirectoryOnly);

        TextAsset[] textassets =Resources.LoadAll<TextAsset>("NoteJson/");
        //DefaultAsset[] textassets =Resources.LoadAll<DefaultAsset>("NoteJson/");
        Array.Resize<string>(ref musicnames, 1);
        musicnames[0] = JsonUtility.FromJson<Humen>(textassets[0].ToString()).name;
       
        int typenum = 0;
        for (int i = 1; i < textassets.Length; i++)
        {

            if(musicnames[typenum]!= JsonUtility.FromJson<Humen>(textassets[i].ToString()).name)
            {
                typenum++;
                Array.Resize<string>(ref musicnames, musicnames.Length + 1);
                musicnames[typenum] = JsonUtility.FromJson<Humen>(textassets[i].ToString()).name;
            }
        }


        //Array.Resize<string>(ref names, textassets.Length);
        //以下textassets ->musicnames
        Array.Resize<string>(ref names, musicnames.Length);
        names = musicnames;
        Debug.Log(musicnames.Length);
        //for (int i =0;i<musicnames.Length;i++)
        //{
        //    musicList.Add(JsonUtility.FromJson<Humen>(musicnames[i].ToString()));
        //    names[i] = musicList[i].name;
        //    //textassets[i].name = musicList[i].name;
        //}
        
  
        for (int i = 0; i < names.Length; i++)
        {

            //names[i] = Path.GetFileNameWithoutExtension(names[i]);
            GameObject m_obj = Instantiate(listItemPre, pos, Quaternion.identity);
            //m_obj.transform.parent = contentOBJ.transform;
            m_obj.transform.SetParent(contentOBJ.transform, false);
            m_nameList.Add(m_obj);
            m_nameList[i].transform.Find("Musicname").GetComponent<Text>().text = names[i];
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
