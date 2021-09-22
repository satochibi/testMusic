using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterType
{
    Kerotama =0,
    Cheerleader,
    Typenum
}


public class GalleryManager : MonoBehaviour
{

     string[] charaname =
    {
        "ÇØÇÎÇΩÇ‹",
        "ÉLÉÉÉâÇOÇQ",
        "ÉLÉÉÉâÇOÇR"
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


    [SerializeField]
    GameObject[] SelectObj;
    GameSystem system;
    private void Start()
    {
        system = GameObject.Find("GameManager").GetComponent<GameSystem>();
        if (system.playerPalam.character != default)
        {
            c_type = system.playerPalam.character;
        }
        else c_type = CharacterType.Kerotama;

        SelectON();
    }
    public void OpenMenu(int type)
    {
        Menu.SetActive(true);
        c_type = (CharacterType)type;
        Sprite chara2D = Resources.Load<Sprite>("Gallery/Sprite/" + c_type.ToString());
        imageobj.GetComponent<Image>().sprite = chara2D;

        string chara_text = Resources.Load<TextAsset>("Gallery/Text/" + c_type.ToString()).text;
        explanationOBJ.GetComponent<Text>().text = chara_text;
        charanameObj.GetComponent<Text>().text = charaname[type];
    }
    public void SelectCharacter()
    {
        system.playerPalam.character = c_type;
        Debug.Log(system.playerPalam.character.ToString() + "Ç™ÉZÉbÉgÇ≥ÇÍÇ‹ÇµÇΩÅI");
    }
   public void SelectON()
    {

        int type = (int)c_type;
        for (int i = 0; i < SelectObj.Length; i++)
        {
            if (i == type)
            {
                SelectObj[type].SetActive(true);
            }
            else
            {
                SelectObj[i].SetActive(false);
            }
        }
    }
}
