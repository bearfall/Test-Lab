using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceManager2 : MonoBehaviour
{
    public TextMeshProUGUI TMP_Number;
    public Rigidbody rb;
    public GameObject[] faceDetectors;


    [Header("Debug")]
    public int defaultFaceResult = -1;
    public int alteredFaceResult = -1;
    // Start is called before the first frame update
    void Start()
    {

        rb = gameObject.GetComponent<Rigidbody>();
        TMP_Number = GameObject.Find("Number").GetComponent<TextMeshProUGUI>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckObjectHasStopped() == true)
        {
            int indexResult = FindFaceResult();
            ChangeNumber(indexResult);
        }
    }



    private InitialState SetInitialState()
    {
        //Randomize X, Y, Z position in the bounding box
        float x = transform.position.x + Random.Range(-transform.localScale.x / 2,
                                                       transform.localScale.x / 2);
        float y = transform.position.y + Random.Range(-transform.localScale.y / 2,
                                                       transform.localScale.y / 2);
        float z = transform.position.z + Random.Range(-transform.localScale.z / 2,
                                                       transform.localScale.z / 2);
        Vector3 position = new Vector3(x, y, z);

        x = Random.Range(0, 360);
        y = Random.Range(0, 360);
        z = Random.Range(0, 360);
        Quaternion rotation = Quaternion.Euler(x, y, z);

        x = Random.Range(0, 5);
        y = Random.Range(0, 5);
        z = Random.Range(0, 5);
        Vector3 force = new Vector3(x, -y, z);

        x = Random.Range(0, 5);
        y = Random.Range(0, 5);
        z = Random.Range(0, 5);
        Vector3 torque = new Vector3(x, y, z);

        return new InitialState(position, rotation, force, torque);
    }

    public void ThrowTheDice()
    {

        
        InitialState initial = SetInitialState();


        gameObject.transform.position =new Vector3(0, 5, 1);
     //   gameObject.transform.rotation = initial.rotation;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.velocity = initial.force;
        rb.AddTorque(initial.torque, ForceMode.VelocityChange);
    }

    public struct InitialState
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 force;
        public Vector3 torque;

        public InitialState(Vector3 position, Quaternion rotation,
                            Vector3 force, Vector3 torque)
        {
            this.position = position;
            this.rotation = rotation;
            this.force = force;
            this.torque = torque;
        }
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
        return maxIndex;
    }

    public bool CheckObjectHasStopped()
    {
        if (rb.velocity == Vector3.zero &&
            rb.angularVelocity == Vector3.zero)
        {
            return true;
        }
        else return false;
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
                TMP_Number.text = "61";
                break;

        }



    }
}
