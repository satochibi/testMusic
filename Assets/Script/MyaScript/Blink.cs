using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blink : MonoBehaviour
{

    [SerializeField]
    float speed = 1.0f;

    //private
    Text text;
    Image image;
    float time;

    enum ObjType
    {
        Text,
        Image
    };
    ObjType thisObjType = ObjType.Text;

    void Start()
    {
        //�A�^�b�`���Ă�I�u�W�F�N�g�𔻕�
        if (this.gameObject.GetComponent<Image>())
        {
            thisObjType = ObjType.Image;
            image = this.gameObject.GetComponent<Image>();
        }
        else if (this.gameObject.GetComponent<Text>())
        {
            thisObjType = ObjType.Text;
            text = this.gameObject.GetComponent<Text>();
        }
    }

    void Update()
    {
        //�I�u�W�F�N�g��Alpha�l���X�V
        if (thisObjType == ObjType.Image)
        {
            image.color = GetAlphaColor(image.color);
        }
        else if (thisObjType == ObjType.Text)
        {
            text.color = GetAlphaColor(text.color);
        }
    }

    //Alpha�l���X�V����Color��Ԃ�
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}