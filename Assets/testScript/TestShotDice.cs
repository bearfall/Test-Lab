using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShotDice : MonoBehaviour
{

    public TestCharacter player;
    public GameObject playerOwnDice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShotDice(player);
        }
        */
    }


    public void ShotDice(TestCharacter throwDicePlayer)
    {

        var diceUseNow = throwDicePlayer.playerDice;

        // Instantiate(diceUseNow, diceSpawnPoint, new Quaternion(0, 0, 0, 0));
        playerOwnDice.SetActive(true);
        Vector3 diceSpawnPoint = throwDicePlayer.playerDiceSpawnPoint.position;
        diceUseNow.transform.position = diceSpawnPoint;



       // DicePrefab.transform.position = new Vector3(spawnDicePosition.position.x, spawnDicePosition.position.y, spawnDicePosition.position.z);
       //   gameObject.transform.rotation = initial.rotation;


    }
}
