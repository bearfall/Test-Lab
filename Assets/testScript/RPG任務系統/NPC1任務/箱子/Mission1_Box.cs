using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1_Box : MonoBehaviour
{
    public bool canTake = false;
    MissonManager1 missonManager;
    // Start is called before the first frame update
    void Start()
    {
        missonManager = GameObject.Find("���Ⱥ޲z��").GetComponent<MissonManager1>();
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
