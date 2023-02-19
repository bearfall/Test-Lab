using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//=================================================================
//	用於儲存所有怪物的座標,目前只能在遊戲開始時儲存一次
//	欠缺動態記錄的功能
//=================================================================

public class MonsterPosition : MonoBehaviour {

	public static int mIndex = 0;	//儲存用引數

	public static List<Vector3> monsterPosition = new List<Vector3>();	//儲存怪物座標的陣列


	void Start () {

		monsterPosition.Insert (mIndex,this.transform.position);	//將攜帶此腳本的物件(此處為所有怪物)之座標,存入陣列
		mIndex++;	//儲存用的引數+1
	
	}

}
