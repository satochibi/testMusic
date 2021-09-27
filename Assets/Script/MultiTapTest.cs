using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MultiTapTest : MonoBehaviour
{
    [SerializeField]
    GameObject tapTextGameObj;
    

    Text tapText;
    
    void Start()
    {
        tapText = tapTextGameObj.GetComponent<Text>();
        
    }
    void Update()
    {
        // Track a single touch as a direction control.
        
        var touchCount = Input.touchCount;
        for (int i = 0; i < touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    tapText.text = "開始地点"+touch.position.ToString();
                    Debug.Log("Began"+touch.position);
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    tapText.text ="移動中"+ touch.position.ToString();
                    Debug.Log("Moved"+touch.position);
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    tapText.text = "離した"+touch.position.ToString();
                    Debug.Log("Ended"+touch.position);
                    break;
            }
        }
        
    }
}
