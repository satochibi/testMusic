using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicNode : MonoBehaviour
{
    public GameObject m_textOBJ;
    public AudioSource SelectAudio;
    public void SetFileName()
    {
        GameSystem m_system =GameObject.Find("GameManager").GetComponent<GameSystem>();
        SelectAudio = GameObject.Find("SelectMusic").GetComponent<AudioSource>();
        m_system.SetMusicName(GetComponent<Text>().text);
        m_system.MusicTitleDisp(GetComponent<Text>().text);
        SelectAudio.clip = Resources.Load<AudioClip>("MusicF/"+GetComponent<Text>().text);
        SelectAudio.time = 10.0f;
        SelectAudio.Play();
    }
}
