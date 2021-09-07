using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SaveDataCreate : MonoBehaviour
{
    [SerializeField]
    public struct SaveData
    {
        public string name;
        public int highscore;
    }

    string plusname = "_Score";
    

    public static bool androidflag
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    return true;
                default:
                    return false;
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        TextAsset[] musicname = Resources.LoadAll<TextAsset>("NoteJson");
        TextAsset[] savename=Resources.LoadAll<TextAsset>("SaveJson");
        Debug.Log( savename.Length == 0);
        if (savename.Length==0)
        {
            for(int i=0;i<musicname.Length; i++)
            {
                var data = new SaveData();
                data.name = musicname[i].name + plusname;
                Debug.Log(data.name);
                
                
                data.highscore = 0;
                var Json = JsonUtility.ToJson(data);
                if (androidflag == true)
                {
                    var path = Application.persistentDataPath + "/" + "SaveJson";
                }
                else
                {
                    StreamWriter writer;

                    var path = Application.dataPath + "/Resources/SaveJson/";
                    writer = new StreamWriter(path + data.name + ".json", false);
                    writer.Write(Json);
                    writer.Flush();
                    writer.Close();
                }
            
            }
        }else if (savename.Length < musicname.Length)
        {
            for (int i = 0; i < musicname.Length; i++) {
                var data = new SaveData();
                data.name = musicname[i].name + plusname;
                data.highscore = 0;
                var Json = JsonUtility.ToJson(data);
                if (!System.IO.File.Exists(data.name))
                {

                    if (androidflag == true)
                    {

                    }
                    else
                    {

                    }
                }

            }
        }
        
    }

   
}
