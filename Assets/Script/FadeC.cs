using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeC : MonoBehaviour
{

    [SerializeField]
    //フェードスピード
    public float speed;
    //フェード管理用フラグ
    public bool IsFadein { get; set; } =false;
    public bool IsFadeout { get; set; } = false;
    public bool IsEnd { get; set; } = false;
    float red, green, blue, alfa;
    Image fadeimage;

    public string nextSceneName;
    private void Start()
    {
        fadeimage = GetComponent<Image>();
        red = fadeimage.color.r;
        green = fadeimage.color.g;
        blue = fadeimage.color.b;
        alfa = fadeimage.color.a;
        alfa = 1;
        
        IsFadein = true;
        
    }

    public void SceneChangeOut(string name)
    {
        IsFadeout = true;
        nextSceneName = name;
    }
    public void FadeIn()
    {
        alfa -= speed;
        IsEnd = false;
        setalfa();
        if (alfa <= 0)
        {
            IsFadein = false;
            fadeimage.enabled = false;
        }
    }
    public void FadeOut()
    {
        fadeimage.enabled = true;
        alfa += speed;
        setalfa();
        if (alfa >= 1)
        {
            IsFadeout = false;
            IsEnd = true;
        }
    }
    void setalfa()
    {
        fadeimage.color = new Color(red, green, blue, alfa);
    }
    // Update is called once per frame
    void Update()
    {

        
        if (IsFadein)
        {
            FadeIn();
        }
        if (IsFadeout)
        {
            FadeOut();
        }
        if(IsEnd)
        {
            GameObject.Find("GameManager").GetComponent<GameSystem>().ChangeScene(nextSceneName);
        }
    }
}
