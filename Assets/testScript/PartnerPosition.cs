using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//=================================================================
//	用於儲存所有隊友的座標,目前只能在遊戲開始時儲存一次
//	欠缺動態記錄的功能
//=================================================================

public class PartnerPosition : MonoBehaviour {
	
	public static int pIndex = 0;	//儲存用引數
	
	public static List<Vector3> partnerPosition = new List<Vector3>();	//儲存隊友座標的陣列
	
	
	void Start () {
		
		partnerPosition.Insert (pIndex,this.transform.position);	//將攜帶此腳本的物件(此處為所有隊友)之座標,存入陣列
		pIndex++;	//儲存用的引數+1
		
	}
	
}
