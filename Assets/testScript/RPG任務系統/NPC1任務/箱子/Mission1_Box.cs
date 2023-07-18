using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1_Box : MonoBehaviour
{
    public bool canTake = false;
    MissonManager missonManager;
    // Start is called before the first frame update
    void Start()
    {
        missonManager = GameObject.Find("任務管理器").GetComponent<MissonManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canTake)
        {
            gameObject.SetActive(false);
            missonManager.isBoxTake = true;
        }
    }
}
