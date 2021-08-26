using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesCollisionDetection : MonoBehaviour
{
    [SerializeField]
    Track track;

    [SerializeField]
    AudioClip audioClip;

    float distanceOfNote = float.NaN;

    float distanceOfNotePrev = float.NaN;
    private GameSystem manager;
    const string tagName = "note";
    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameSystem>();
    }
    void Update()
    {
        //Debug.Log(track + " dis: "+ distanceOfNote);

        if (float.IsNaN(distanceOfNote))
        {
            return;
        }

        if (!float.IsNaN(distanceOfNotePrev))
        {
            if (Mathf.Sign(distanceOfNote) != Mathf.Sign(distanceOfNotePrev))
            {
                this.Play();

            }

        }



        distanceOfNotePrev = distanceOfNote;
    }

    void Play()
    {
        //Debug.Log("clap!!");
        this.transform.parent.gameObject.GetComponent<AudioSource>().PlayOneShot(this.audioClip);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == tagName)
        {
            distanceOfNote = other.gameObject.transform.position.z - this.transform.position.z;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == tagName)
        {
            distanceOfNote = other.gameObject.transform.position.z - this.transform.position.z;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == tagName)
        {
            distanceOfNote = float.NaN;
            distanceOfNotePrev = float.NaN;
        }
    }




}
