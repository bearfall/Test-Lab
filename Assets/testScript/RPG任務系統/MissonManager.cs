using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flower;
using System;
using UnityEngine.AI;

namespace bearfall
{


    public class MissonManager : MonoBehaviour
    {
        FlowerSystem flowerSys;
        private string myName;
       // private int progress = 0;
        private bool isMissionStart = false;
        private bool isMissionEnd = false;
        public bool isEnemyDie = false;
       // Mission1_Box mission1_Box;
        private TestGameManager1 testGameManager1;
        private TestCharactersManager testCharactersManager;



        // Start is called before the first frame update
        void Start()
        {
            testGameManager1 = GetComponent<TestGameManager1>();

            testCharactersManager = GetComponent<TestCharactersManager>();

            flowerSys = FlowerManager.Instance.CreateFlowerSystem("FlowerSample", false);

           // mission1_Box = GameObject.Find("任務用箱子").GetComponent<Mission1_Box>();
            //flowerSys.SetupDialog();

            // Setup Variables.
            myName = "Bearfall";
            flowerSys.SetVariable("MyName", myName);

            // Define your customized commands.
            flowerSys.RegisterCommand("UsageCase", CustomizedFunction);
            // Define your customized effects.
            //   flowerSys.RegisterEffect("customizedRotation", EffectCustomizedRotation);
        }

        // Update is called once per frame
        void Update()
        {/*
        if (flowerSys.isCompleted && isMissionStart && !isBoxTake )
        {
            flowerSys.SetupDialog();
            flowerSys.ReadTextFromResource("startMission");
            flowerSys.RemoveDialog();
            mission1_Box.canTake = true;
                      
        }
        
        if (flowerSys.isCompleted && isMissionStart && isBoxTake)
        {
            flowerSys.SetupDialog();
            flowerSys.ReadTextFromResource("Mission1_TakeBox");
            flowerSys.RemoveDialog();

        }
        */

            if (isMissionStart)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Continue the messages, stoping by [w] or [lr] keywords.
                    flowerSys.Next();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    // Resume the system that stopped by [stop] or Stop().
                    flowerSys.Resume();
                }
            }
        }



        public void MissionStart()
        {
           
            
            isMissionStart = true;


            if (flowerSys.isCompleted && isMissionStart && !isEnemyDie)
            {
                flowerSys.SetupDialog();
                flowerSys.ReadTextFromResource("startMission 1");
                //flowerSys.RemoveDialog();

                print("任務開始");

               



            }
            if (flowerSys.isCompleted && isMissionStart && isEnemyDie)
            {
                //flowerSys.SetupDialog();
                flowerSys.ReadTextFromResource("Mission1_TakeBox");
                // flowerSys.RemoveDialog();

            }


        }

        private void CustomizedFunction(List<string> _position)
        {

            var playerCharas = new List<TestCharacter>(); // 敵キャラクターリスト
            foreach (TestCharacter charaData in testCharactersManager.testCharacters)
            {// 全生存キャラクターから敵フラグの立っているキャラクターをリストに追加
                if (charaData.isEnemy == false)
                {
                    playerCharas.Add(charaData);
                    charaData.attackFalse = false;
                    print(charaData.name);

                }
            }

            for (int i = 0; i < playerCharas.Count; i++)
            {
                playerCharas[i].GetComponent<Rigidbody>().isKinematic = true;
                playerCharas[i].GetComponent<Rigidbody>().detectCollisions = false;
                playerCharas[i].GetComponent<Collider>().enabled = false;
                playerCharas[i].GetComponent<NavMeshAgent>().enabled = false;
                playerCharas[i].transform.position = new Vector3 (float.Parse(_position[0]), float.Parse(_position[1]), float.Parse(_position[2]));
                
            }
            testGameManager1.currentArea = TestGameManager1.AreaType.TurnBasedCombat;


        }



    }
}




    
