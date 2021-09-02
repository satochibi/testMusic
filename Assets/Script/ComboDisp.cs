using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboDisp : MonoBehaviour
{
    [SerializeField]
    public GameSystem gameSystem;
    public GameObject hukidasi;
    public Animator animator;
    public GameObject ComboText;
    // Start is called before the first frame update
    void Start()
    {
        gameSystem =GameObject.Find("GameManager").GetComponent<GameSystem>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if(gameSystem.Combo !=0 &&gameSystem.Combo%50 ==0)
        {
            animator.SetInteger("CharaState", 1);
        }
        
        GetComponent<TextMesh>().text = gameSystem.Combo.ToString();
        if(gameSystem.Combo>10)
        {
            GetComponent<MeshRenderer>().enabled = true;
            hukidasi.GetComponent<SpriteRenderer>().enabled = true;
            ComboText.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = false;
            hukidasi.GetComponent<SpriteRenderer>().enabled = false;
            ComboText.GetComponent<MeshRenderer>().enabled = false;
            animator.SetInteger("CharaState", 0);
        }
    }
}
