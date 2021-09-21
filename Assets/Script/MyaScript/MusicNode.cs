using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicNode : MonoBehaviour
{
    [SerializeField]
    GameObject m_textOBJ;
    [SerializeField]
    AudioSource SelectAudio;
    public void SetFileName()
    {
        GameSystem m_system =GameObject.Find("GameManager").GetComponent<GameSystem>();
        SelectAudio = GameObject.Find("SaveDataManager").GetComponent<AudioSource>();
        m_system.SetMusicName(GetComponent<Text>().text);
        m_system.MusicTitleDisp(GetComponent<Text>().text);
        SelectAudio.clip = Resources.Load<AudioClip>("MusicF/"+GetComponent<Text>().text);
        SelectAudio.time = 10.0f;
        SelectAudio.Play();
    }
}
