using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicNode : MonoBehaviour
{
    public GameObject m_textOBJ;
    public void SetFileName()
    {
        GameSystem m_system =GameObject.Find("GameManager").GetComponent<GameSystem>();
        m_system.SetMusicName(GetComponent<Text>().text);
        m_system.MusicTitleDisp(GetComponent<Text>().text);
    }
}
