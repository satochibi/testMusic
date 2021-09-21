using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class SaveDataManager : MonoBehaviour
{
    [SerializeField]
    
    public SaveDataPalam savedata;
    public List<string> musicnames;
    
    //�v���b�g�t�H�[���ʎQ�ƃp�X
    public static string Path
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

        if (!Directory.Exists(Path + m_name))
        {
            Directory.CreateDirectory(Path + m_name);
            Debug.Log(m_name + "�f�B���N�g�����쐬���܂����B");
        }
        for (int i = 0; i < (int)Difficulty.DifficaltyType; i++)
        {
            if (!File.Exists(Path + m_name + "/" + ((Difficulty)i).ToString() + ".json"))
            {
                var data = new SaveDataPalam();

                if (m_name == "�n���W�I��")
                {
                    data.highscore = 750000;
                }

                var Json = JsonUtility.ToJson(data);
                StreamWriter writer;
                writer = new StreamWriter(Path + m_name + "/" + ((Difficulty)i).ToString() + ".json", false);

                Debug.Log(m_name + "��" + ((Difficulty)i).ToString() + "�̃Z�[�u�f�[�^���쐬���܂����B");
                writer.Write(Json);
                writer.Flush();
                writer.Close();

            }
        }
    }

    public void SaveDataLoad(int difficulty)
    {
        GameSystem m_system = GameObject.Find("GameManager").GetComponent<GameSystem>();
        string savepath = Path + m_system.m_result.MusicTitle + "/" + ((Difficulty)difficulty).ToString() + ".json";
        Debug.Log(savepath);
        savedata = JsonUtility.FromJson<SaveDataPalam>(File.ReadAllText(savepath));
        m_system.m_result.difficulty = (Difficulty)difficulty;
        Debug.Log(m_system.m_result.MusicTitle + "\n" + difficulty + "�̃n�C�X�R�A" + savedata.highscore);
    }

    public void SetHighScore(Text m_text)
    {
        m_text.enabled = true;
        m_text.text = "�n�C�X�R�A�@" + savedata.highscore.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        //TextAsset[] musicname = Resources.LoadAll<TextAsset>("NoteJson");
        //TextAsset[] savename=Resources.LoadAll<TextAsset>("SaveJson");


        MusicSelect.SearchMusicNamesFromResources(musicnames);
        string[] musicname = musicnames.ToArray();
        for (int i = 0; i < musicname.Length; i++)
        {
            DirectoryOrFileCheck(musicname[i]);
        }
       

    }



}