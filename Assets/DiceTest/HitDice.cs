using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDice : MonoBehaviour
{
    private Rigidbody rig;
    public int hitCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnCollisionEnter(Collision EnemyDice)
    {
        if (EnemyDice.gameObject.tag.Contains("EnemyDice") && hitCount > 0)
        {
            rig = EnemyDice.gameObject.GetComponent<Rigidbody>();
            rig.velocity = new Vector3(0, 5, 0);
            hitCount -= 1;
        }
    }
}
