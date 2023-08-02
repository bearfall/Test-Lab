using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RollDice : MonoBehaviour
{
    public TextMeshProUGUI TMP_Number;
    public GameObject[] faceDetectors;
    public int defaultFaceResult = -1;
    private Rigidbody rb;
    private Vector3 force;

    public bool isThrowDice = false;
    public bool diceStop = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TMP_Number = GameObject.Find("DiceNumber").GetComponent<TextMeshProUGUI>();


    }

    // Update is called once per frame
    void Update()
    {
        if (isThrowDice == true)
        {
            print("DiceStop");
            int indexResult = FindFaceResult();

            ChangeNumber(indexResult);
            

            // playerDiceNumber = indexResult + 1;
        }


        if (CheckObjectHasStopped() == true)
        {
            
            isThrowDice = false;

           // playerDiceNumber = indexResult + 1;
        }
    }


    private void SetDiceForce()
    {
        
       

       
        float z = Random.Range(5, 10);
        force = new Vector3(0, 0, z);

       

        
    }


    public void RollTheDice()
    {
        isThrowDice = true;

            SetDiceForce();
        rb.velocity = force;
        
    }

    public int FindFaceResult()
    {
        //Since we have all child objects for each face,
        //We just need to find the highest Y value
        int maxIndex = 0;
        for (int i = 1; i < faceDetectors.Length; i++)
        {
            if (faceDetectors[maxIndex].transform.position.y <
                faceDetectors[i].transform.position.y)
            {
                maxIndex = i;
            }
        }

        
        defaultFaceResult = maxIndex;
        print(maxIndex);
        return maxIndex;
    }

    public bool CheckObjectHasStopped()
    {
        if (isThrowDice)
        {
            //rb = DicePrefab.GetComponent<Rigidbody>();
            if (rb.velocity == Vector3.zero &&
                rb.angularVelocity == Vector3.zero)
            {
                diceStop = true;
                //isThrowDice = false;
                return true;

            }
            else
            {
                diceStop = false;
                return false;
            }
        }
        else
        {
            return false;
        }
    }


    public void ChangeNumber(int faceResult)
    {
        switch (faceResult)
        {
            case 0:
                TMP_Number.text = "1";
                break;
            case 1:
                TMP_Number.text = "2";
                break;
            case 2:
                TMP_Number.text = "3";
                break;
            case 3:
                TMP_Number.text = "4";
                break;
            case 4:
                TMP_Number.text = "5";
                break;
            case 5:
                TMP_Number.text = "6";
                break;
            case 6:
                TMP_Number.text = "7";
                break;
            case 7:
                TMP_Number.text = "8";
                break;
            case 8:
                TMP_Number.text = "9";
                break;
            case 9:
                TMP_Number.text = "10";
                break;
            

        }



    }
}
