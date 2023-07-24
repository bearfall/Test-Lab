using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flower;
public class MissonManager1 : MonoBehaviour
{
    FlowerSystem flowerSys;
    private string myName;
    private int progress = 0;
    private bool isMissionStart = false;
    private bool isMissionEnd = false;
    public bool isBoxTake = false;
    Mission1_Box mission1_Box;


    // Start is called before the first frame update
    void Start()
    {
        flowerSys = FlowerManager.Instance.CreateFlowerSystem("FlowerSample", false);

        mission1_Box = GameObject.Find("任務用箱子").GetComponent<Mission1_Box>();
        //flowerSys.SetupDialog();

        // Setup Variables.
        myName = "Bearfall";
        flowerSys.SetVariable("MyName", myName);

        // Define your customized commands.
        //   flowerSys.RegisterCommand("UsageCase", CustomizedFunction);
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
        
        
        if (flowerSys.isCompleted && isMissionStart && !isBoxTake)
        {
            flowerSys.SetupDialog();
            flowerSys.ReadTextFromResource("startMission");
            //flowerSys.RemoveDialog();
            mission1_Box.canTake = true;
            print("任務開始");

        }
        if (flowerSys.isCompleted && isMissionStart && isBoxTake)
        {
            //flowerSys.SetupDialog();
            flowerSys.ReadTextFromResource("Mission1_TakeBox");
           // flowerSys.RemoveDialog();

        }
        
       
    }




}




    
