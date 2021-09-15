using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{

    [SerializeField]
    public GameObject audioOBJ;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PauseStart()
    {
        //pauseOBJ.SetActive(true);
        audioOBJ.GetComponent<AudioSource>().Pause();
        Time.timeScale = 0.0f;
    }
    public void PauseEnd()
    {
        audioOBJ.GetComponent<AudioSource>().UnPause();
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
   
    public void SceneChange(string nextSceneName)
    {
        GameSystem game= GameObject.Find("GameManager").GetComponent<GameSystem>();
        if(nextSceneName =="SampleScene")
        {
            string titlename = game.m_result.MusicTitle;
            game.InitializedPalam();
            game.m_result.MusicTitle = titlename;
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
