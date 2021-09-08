using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    [SerializeField]
    static string plusname = "_Score";
    public SaveDataPalam savedata;

    //public static bool androidflag
    //{
    //    get
    //    {
    //        switch (Application.platform)
    //        {
    //            case RuntimePlatform.Android:
    //                return true;
    //            default:
    //                return false;
    //        }
    //    }

    //}
    //プラットフォーム別参照パス
    static string path
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    return Application.persistentDataPath;

                case RuntimePlatform.Android:
                    return Application.persistentDataPath;

                default:
                    return Application.dataPath + "/SaveData/";
            }
        }
    }
    public void DirectoryOrFileCheck(string m_name)
    {

        if (!Directory.Exists(path + m_name))
        {
            Directory.CreateDirectory(path + m_name);
            Debug.Log(m_name+"ディレクトリを作成しました。");
        }
        for (int i = 0; i < (int)Difficulty.DifficaltyType; i++)
        {
            if (!File.Exists(path + m_name + "/" + ((Difficulty)i).ToString() + ".json"))
            {
                var data = new SaveDataPalam();
                var Json = JsonUtility.ToJson(data);
                StreamWriter writer;
                writer = new StreamWriter(path + m_name + "/" + ((Difficulty)i).ToString() + ".json", false);
                Debug.Log(m_name +"の"+ ((Difficulty)i).ToString() +"のセーブデータを作成しました。");
                writer.Write(Json);
                writer.Flush();
                writer.Close();

            }
        }
    }

    public void SaveDataLoad(string difficulty)
    {
        GameSystem m_system = GameObject.Find("GameManager").GetComponent<GameSystem>();
        string savepath = path + m_system.m_result.MusicTitle + "/" + difficulty + ".json";

        savedata = JsonUtility.FromJson<SaveDataPalam>(File.ReadAllText(savepath));
        Debug.Log(m_system.m_result.MusicTitle + "\n" + difficulty + "のハイスコア" + savedata.highscore);
    }
    // Start is called before the first frame update
    void Start()
    {
        TextAsset[] musicname = Resources.LoadAll<TextAsset>("NoteJson");
        //TextAsset[] savename=Resources.LoadAll<TextAsset>("SaveJson");


        for (int i = 0; i < musicname.Length; i++)
        {
            DirectoryOrFileCheck(musicname[i].name);
        }
        //if (savename.Length==0)
        //{
        //    for(int i=0;i<musicname.Length; i++)
        //    {
        //        var data = new SaveDataPalam();
        //        data.musicname = musicname[i].name + plusname;
        //        Debug.Log(data.musicname);


        //        data.highscore = 0;
        //        var Json = JsonUtility.ToJson(data);
        //        if (androidflag == true)
        //        {
        //            var path = Application.persistentDataPath + "/" + "SaveJson";
        //        }
        //        else
        //        {
        //            StreamWriter writer;

        //            var path = Application.dataPath + "/Resources/SaveJson/";
        //            writer = new StreamWriter(path + data.musicname + ".json", false);
        //            writer.Write(Json);
        //            writer.Flush();
        //            writer.Close();
        //        }

        //    }
        //}else if (savename.Length < musicname.Length)
        //{
        //    for (int i = 0; i < musicname.Length; i++) {
        //        var data = new SaveDataPalam();
        //        data.musicname = musicname[i].name + plusname;
        //        data.highscore = 0;
        //        var Json = JsonUtility.ToJson(data);
        //        if (androidflag == true)
        //        {
        //            var path = Application.persistentDataPath + "/" + "SaveJson";
        //            if (!System.IO.File.Exists(path + data.musicname))
        //            {

        //            }
        //        }
        //        else
        //        {

        //            var path = Application.dataPath + "/Resources/SaveJson/";
        //            Debug.Log(path + data.musicname);

        //            if (!System.IO.File.Exists(path + data.musicname+ ".json"))
        //            {
        //                Debug.Log("point1");
        //                StreamWriter writer;
        //                writer = new StreamWriter(path + data.musicname + ".json", false);
        //                writer.Write(Json);
        //                writer.Flush();
        //                writer.Close();
        //            }
        //        }

        //    }
        //}

    }



}