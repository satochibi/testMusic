using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MusicTitle : MonoBehaviour
{

    [SerializeField]
    public Image MusicIllust;
    public Text TitleText;
    public GameObject[] difficultyButtonOBJ;
    Sprite sprite;
    public GameSystem m_system;
    public void Disp(string name)
    {
        if (GetComponent<Image>().enabled == false)
        {
            GetComponent<Image>().enabled = true;
            MusicIllust.enabled = true;
            TitleText.enabled = true;
        }
        foreach(GameObject button in difficultyButtonOBJ)
        {
            button.SetActive(true);
        }
        TitleText.text = name;
        sprite = Resources.Load<Sprite>("MusicTitleIllust/"+name);
        Debug.Log("MusicTitleIllust/" + name);
        MusicIllust.sprite = sprite;
    }
    // Start is called before the first frame update
    void Start()
    {
        m_system = GameObject.Find("GameManager").GetComponent<GameSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
