using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audioset : MonoBehaviour
{
    //�I�[�f�B�I�̃I�t�Z�b�g�p
    public GameObject AudioObject;
    AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        //�I�[�f�B�I�ɃZ�b�g���ꂽ�ȃf�[�^�̉��
        clip = AudioObject.GetComponent<AudioSource>().clip;
        this.audioAwake();
    }

    // Update is called once per frame
    void Update()
    {

    }
    //�I�t�Z�b�g�̎��ԋy�уI�t�Z�b�g�N���p
    
    //�I�[�f�B�I�̃I�u�W�F�N�g���A�N�e�B�u��
    void audioAwake()
    {
        AudioObject.SetActive(true);
    }
}
