using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

    [SerializeField]
    GameObject audioOBJ;

    [SerializeField]
    GameObject tapObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PauseStart()
    {
        //pauseOBJ.SetActive(true);
        audioOBJ.GetComponent<AudioSource>().Pause();
        tapObj.GetComponent<MultiTapTest>().IsPouse = true;
        Time.timeScale = 0.0f;
    }
    public void PauseEnd()
    {
        audioOBJ.GetComponent<AudioSource>().UnPause();
        gameObject.SetActive(false);
        tapObj.GetComponent<MultiTapTest>().IsPouse = false;
        Time.timeScale = 1.0f;
    }
   
    public void SceneChange(string nextSceneName)
    {
        GameSystem game= GameObject.Find("GameManager").GetComponent<GameSystem>();
        if(nextSceneName =="SampleScene"|| nextSceneName  == "MultiTapTest")
        {
            string titlename = game.result.musicTitle;
            Difficulty difficulty = game.result.difficulty;
            game.InitializedPalam();
            game.result.musicTitle = titlename;
            game.result.difficulty = difficulty;
        }
        if (nextSceneName == "Result")
        {
            game.IsEnd = true;
        }
        else
        {
            game.ChangeScene(nextSceneName);
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
