using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMapManager : MonoBehaviour
{
    public static List<TestMapBlock> testMapBlocks = new List<TestMapBlock>();
    public GameObject parent;
    private TestMapBlock testMapBlock;
    private TestCharactersManager testCharactersManager;

    public const int MAP_WIDTH = 100; // マップの横幅
    public const int MAP_HEIGHT = 100; // マップの縦(奥行)の幅
    // Start is called before the first frame update
    void Start()
    {
        FindChild(parent);
        testCharactersManager = GetComponent<TestCharactersManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FindChild(GameObject parentGameObject)
    {
        for (int i = 0; i < parentGameObject.transform.childCount; i++)
        {

            //print(mapBlocks[i].transform.position);
            testMapBlock = parentGameObject.transform.GetChild(i).gameObject.GetComponent<TestMapBlock>();
            testMapBlock.xPos = (int)parentGameObject.transform.GetChild(i).gameObject.transform.position.x;
            testMapBlock.zPos = (int)parentGameObject.transform.GetChild(i).gameObject.transform.position.z;
            testMapBlocks.Add(parentGameObject.transform.GetChild(i).gameObject.GetComponent<TestMapBlock>());

        }
        
    }
    
    public void AllselectionModeClear()
    {
        for (int i = 0; i < testMapBlocks.Count; i++)
        {
            testMapBlocks[i].SetSelectionMode(TestMapBlock.Highlight.Off);
        }
    }




    public List<TestMapBlock> SearchAttackableBlocks(int xPos, int zPos)
    {
        bool ischaraData;
        var Charas = new List<TestCharacter>(); // 敵キャラクターリスト
        for (int i = 0; i < testCharactersManager.testCharacters.Count; i++)
        {
            if (!testCharactersManager.testCharacters[i].isEnemy)
            {
                Charas.Add(testCharactersManager.testCharacters[i]);
            }
        }
        var results = new List<TestMapBlock>();


        Vector3 hi2 = new Vector3(xPos, 0, zPos);


            ischaraData = testCharactersManager.isCharacterDataByPos(xPos + 1, zPos);
            if (ischaraData)
            {
                ischaraData = testCharactersManager.isCharacterDataByPos(xPos + 2, zPos);
                if (ischaraData)
                {
                    ischaraData = testCharactersManager.isCharacterDataByPos(xPos + 3, zPos);
                    if (ischaraData)
                    {
                        hi2 = new Vector3(xPos + 4, 0, zPos);
                    }
                    else
                    {
                        hi2 = new Vector3(xPos + 3, 0, zPos);
                    }
                }
                else
                {
                    hi2 = new Vector3(xPos + 2, 0, zPos);
                }
            }
            else
            {
                hi2 = new Vector3(xPos + 1, 0, zPos);
            }
        

        foreach (var gameObject in testMapBlocks)
        {
            if (gameObject.transform.position == hi2)
            {
                
                //gameObject.transform.GetChild(0).gameObject.SetActive(true);
                //print(gameObject.transform.GetChild(0).gameObject);
                results.Add(gameObject);
            }
        }

        hi2 = new Vector3(xPos, 0, zPos);

        ischaraData = testCharactersManager.isCharacterDataByPos(xPos - 1, zPos);
        if (ischaraData)
        {
            ischaraData = testCharactersManager.isCharacterDataByPos(xPos - 2, zPos);
            if (ischaraData)
            {
                ischaraData = testCharactersManager.isCharacterDataByPos(xPos - 3, zPos);
                if (ischaraData)
                {
                    hi2 = new Vector3(xPos - 4, 0, zPos);
                }
                else
                {
                    hi2 = new Vector3(xPos - 3, 0, zPos);
                }
            }
            else
            {
                hi2 = new Vector3(xPos - 2, 0, zPos);
            }
        }
        else
        {
            hi2 = new Vector3(xPos - 1, 0, zPos);
        }

        foreach (var gameObject in testMapBlocks)
        {
            if (gameObject.transform.position == hi2)
            {
               // gameObject.transform.GetChild(0).gameObject.SetActive(true);
                //print(gameObject.transform.GetChild(0).gameObject);
                results.Add(gameObject);
            }
        }

        hi2 = new Vector3(xPos, 0, zPos);

        ischaraData = testCharactersManager.isCharacterDataByPos(xPos, zPos + 1);
        if (ischaraData)
        {
            ischaraData = testCharactersManager.isCharacterDataByPos(xPos, zPos + 2);
            if (ischaraData)
            {
                ischaraData = testCharactersManager.isCharacterDataByPos(xPos, zPos + 3);
                if (ischaraData)
                {
                    hi2 = new Vector3(xPos, 0, zPos + 4);
                }
                else
                {
                    hi2 = new Vector3(xPos, 0, zPos + 3);
                }
            }
            else
            {
                hi2 = new Vector3(xPos, 0, zPos + 2);
            }
        }
        else
        {
            hi2 = new Vector3(xPos, 0, zPos + 1);
        }


        foreach (var gameObject in testMapBlocks)
        {
            if (gameObject.transform.position == hi2)
            {
               // gameObject.transform.GetChild(0).gameObject.SetActive(true);
               // print(gameObject.transform.GetChild(0).gameObject);
                results.Add(gameObject);
            }
        }

        hi2 = new Vector3(xPos, 0, zPos);

        ischaraData = testCharactersManager.isCharacterDataByPos(xPos, zPos - 1);
        if (ischaraData)
        {
            ischaraData = testCharactersManager.isCharacterDataByPos(xPos, zPos - 2);
            if (ischaraData)
            {
                ischaraData = testCharactersManager.isCharacterDataByPos(xPos, zPos - 3);
                if (ischaraData)
                {
                    hi2 = new Vector3(xPos, 0, zPos - 4);
                }
                else
                {
                    hi2 = new Vector3(xPos, 0, zPos - 3);
                }
            }
            else
            {
                hi2 = new Vector3(xPos, 0, zPos - 2);
            }
        }
        else
        {
            hi2 = new Vector3(xPos, 0, zPos - 1);
        }

        foreach (var gameObject in testMapBlocks)
        {
            if (gameObject.transform.position == hi2)
            {
               // gameObject.transform.GetChild(0).gameObject.SetActive(true);
                //print(gameObject.transform.GetChild(0).gameObject);
                results.Add(gameObject);
            }
        }


        foreach (var item in results)
        {
            print(item.transform.position);
        }
        return results;



    }


    public List<TestMapBlock> EnemySearchAttackableBlocks(int xPos, int zPos)
    {
        var results = new List<TestMapBlock>();


        Vector3 hi3 = new Vector3(xPos + 1, 0, zPos);

        foreach (var gameObject in testMapBlocks)
        {
            if (gameObject.transform.position == hi3)
            {
               // gameObject.transform.GetChild(0).gameObject.SetActive(true);
                print(gameObject.transform.GetChild(0).gameObject);
                results.Add(gameObject);
            }
        }

        hi3 = new Vector3(xPos, 0, zPos + 1);

        foreach (var gameObject in testMapBlocks)
        {
            if (gameObject.transform.position == hi3)
            {
                //gameObject.transform.GetChild(0).gameObject.SetActive(true);
                print(gameObject.transform.GetChild(0).gameObject);
                results.Add(gameObject);
            }
        }

        hi3 = new Vector3(xPos - 1, 0, zPos);

        foreach (var gameObject in testMapBlocks)
        {
            if (gameObject.transform.position == hi3)
            {
                //gameObject.transform.GetChild(0).gameObject.SetActive(true);
                print(gameObject.transform.GetChild(0).gameObject);
                results.Add(gameObject);
            }
        }

        hi3 = new Vector3(xPos, 0, zPos - 1);

        foreach (var gameObject in testMapBlocks)
        {
            if (gameObject.transform.position == hi3)
            {
                //gameObject.transform.GetChild(0).gameObject.SetActive(true);
                print(gameObject.transform.GetChild(0).gameObject);
                results.Add(gameObject);
            }
        }

        return results;



    }

}
