using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public  List<GameObject> building = new List<GameObject>();
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        //FindBuilding(parent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void FindBuilding(GameObject parentGameObject)
    {
        for (int i = 0; i < parentGameObject.transform.childCount; i++)
        {


            building.Add(parentGameObject.transform.GetChild(i).gameObject);

        }


    }


    public void ClearBuilding()
    {
        FindBuilding(parent);


        for (int i = 0; i < building.Count; i++)
        {
            building[i].gameObject.GetComponent<BuildingInfo>().UndoMaterial();
        }
        building.Clear();


    }
}
