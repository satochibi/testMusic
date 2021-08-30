using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class MusicSelect : MonoBehaviour
{

    [SerializeField]
    public string[] names;
    public GameObject listItemPre;
    public List<GameObject> m_nameList;
    public GameObject contentOBJ;
    
    Vector3 pos;
   
    public void EraceList()
    {
        foreach(GameObject obj in m_nameList)
        {
            Destroy(obj);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        //指定フォルダからjsonファイルを一括読み込み
        string path = Application.dataPath + "/Resources/NoteJson/";
             names = Directory.GetFiles(@path, "*.json",SearchOption.TopDirectoryOnly);
        
        Debug.Log(names[0]);
  
        for (int i = 0; i < names.Length; i++)
        {
            pos.Set(0, 0, 0);

            
            names[i] = Path.GetFileNameWithoutExtension(names[i]);
            GameObject m_obj = Instantiate(listItemPre, pos, Quaternion.identity);
            //m_obj.transform.parent = contentOBJ.transform;
            m_obj.transform.SetParent(contentOBJ.transform, false);
            m_nameList.Add(m_obj);
            m_nameList[i].transform.Find("Musicname").GetComponent<Text>().text = names[i];
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            EraceList();
        }
    }
}
