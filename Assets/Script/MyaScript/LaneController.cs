using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LaneController : MonoBehaviour
{
    [SerializeField]
    int Tracknum = default;

    [SerializeField]
    GameObject fumenObj;
    Fumen fumen;
    Material material;
    Color changeColor = Color.clear;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        fumen = fumenObj.GetComponent<Fumen>();


    }
    public void ChangeColor(Color color)
    {
        material.SetColor("_Color", color);
        changeColor = color;
    }
    public void GoJudge(NotesType type)
    {
        switch (type)
        {
            case NotesType.Normal:
                fumen.Judge(Tracknum, type);
                break;
            case NotesType.Long:
                fumen.LongNotesEventJudge(Tracknum);
                break;
            case NotesType.LongEnd:
                fumen.Judge(Tracknum, type);
                break;
            case NotesType.NotesTypeNum:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (material.HasProperty("_Color"))
        {

            Color color = Color.gray;
            if (changeColor != Color.clear)
            {
                color = changeColor;
                changeColor = Color.clear;
            }
            material.SetColor("_Color", color);
        }

    }
}
