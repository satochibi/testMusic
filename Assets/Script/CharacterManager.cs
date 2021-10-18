using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    
    GameSystem system;
    Animator animator;
    SpriteRenderer sprite;
    AnimatorOverrideController aniCon;

    //‰ñ“]’†‚©‚Ç‚¤‚©
    bool coroutineBool = false;
    // Start is called before the first frame update
    void Start()
    {
        
        system = GameObject.Find("GameManager").GetComponent<GameSystem>();
        Debug.Log(system.playerPalam.character);
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = Resources.Load<Sprite>("Gallery/Sprite/" + system.playerPalam.character.ToString());
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Animator/" + system.playerPalam.character.ToString()));
        //aniCon =new AnimatorOverrideController(Resources.Load<Animator>("Animator/" + system.playerPalam.character.ToString()).runtimeAnimatorController);
        //animator.runtimeAnimatorController = aniCon;
        
        
    }
    IEnumerator RotateON()
    {
        for (int turn = 0; turn < 20; turn++)
        {
            
            transform.Rotate(0, -18, 0);
            yield return new WaitForSeconds(0.01f);
        }
        coroutineBool = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (system.Combo ==50)
        {
            if (!coroutineBool)
            {
                coroutineBool = true;
                StartCoroutine("RotateON");
            }
        }
       
    }
}
