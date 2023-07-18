using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1_Mission : MonoBehaviour
{
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("任務1對話按鈕畫布").GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("玩家"))
        {
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("玩家"))
        {
            canvas.enabled = false;
        }
    }
}
