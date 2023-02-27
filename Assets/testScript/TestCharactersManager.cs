using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestCharactersManager : MonoBehaviour
{
    public Transform charactersParent;
    

    [HideInInspector]
    public List<TestCharacter> testCharacters;
   
    // Start is called before the first frame update
    void Start()
    {
        testCharacters = new List<TestCharacter>();
        charactersParent.GetComponentsInChildren(testCharacters);

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public TestCharacter GetCharacterDataByPos(float xPos, float zPos)
    {
        // 検索処理
        // （使用foreach對地圖中的所有角色數據一一做同樣的處理）
        foreach (TestCharacter charaData in testCharacters)
        {
            // 檢查角色的位置是否與指定位置匹配
            if ((charaData.xPos == xPos) && // 相同的 X 位置
                (charaData.zPos == zPos)) // 相同的 Z 位置
            {// 匹配位置
                return charaData; // 返回數據並退出
            }
        }

        // 如果沒有找到數據則返回 null
        return null;
    }


    /// 指定したキャラクターを削除する
	/// </summary>
	/// <param name="charaData">対象キャラデータ</param>
	public void DeleteCharaData(TestCharacter charaData)
    {
        // リストからデータを削除
        testCharacters.Remove(charaData);
        // オブジェクト削除
        DOVirtual.DelayedCall(
            0.5f, // 遅延時間(秒)
            () =>
            {// 遅延実行する内容
                Destroy(charaData.gameObject);
            }
        );
    }






  
}
