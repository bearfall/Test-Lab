using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1_Mission : MonoBehaviour
{
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("����1��ܫ��s�e��").GetComponent<Canvas>();
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("���a"))
        {
            canvas.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("���a"))
        {
            canvas.enabled = false;
        }
    }
}
