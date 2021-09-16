using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterType
{
    kerotama =0,
    Cheerleader,
    typenum
}


public class GalleryManager : MonoBehaviour
{

     string[] charaname =
    {
        "‚¯‚ë‚½‚Ü",
        "ƒLƒƒƒ‰‚O‚Q",
        "ƒLƒƒƒ‰‚O‚R"
    };
    [SerializeField]
    GameObject Menu;
    CharacterType c_type;
    [SerializeField]
    GameObject explanationOBJ;
    [SerializeField]
    GameObject charanameObj;

    [SerializeField]
    GameObject imageobj;

    GameSystem system;
    private void Start()
    {
        system = GameObject.Find("GameManager").GetComponent<GameSystem>();
    }
    void OpenMenu(int type)
    {
        Menu.SetActive(true);
        c_type = (CharacterType)type;
        Sprite chara2D = Resources.Load<Sprite>("Gallery/Sprite/" + c_type.ToString());
        //imageobj.GetComponent<Sprite>().

        string chara_text = Resources.Load<TextAsset>("Gallery/Text/" + c_type.ToString()).text;
        charanameObj.GetComponent<Text>().text = charaname[type];
    }
    void SelectCharacter()
    {
        system.m_result.character = c_type;
    }
   
}
