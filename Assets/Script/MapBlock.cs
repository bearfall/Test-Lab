using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class MapBlock : MonoBehaviour
{
	// ブロックデータ
	[HideInInspector] // インスペクタ上で非表示にする属性
	public int xPos; // X方向の位置
	[HideInInspector]
	public int zPos; // Z方向の位置
	[Header("可通過標示（如果為true則可通過）)")]
	public bool passable; // 通行可能フラグ



	// 高亮對象
	private GameObject selectionBlockObj;

	// 突出材質
	[Header("強調表示マテリアル：選択時")]
	public Material selMat_Select; // 選択時
	[Header("強調表示マテリアル：到達可能")]
	public Material selMat_Reachable; // 角色可達
	[Header("強調表示マテリアル：攻撃可能")]
	public Material selMat_Attackable; // 角色可以攻擊
									  
	public enum Highlight // 定義塊高亮模式(列挙型)
	{
		Off, // 離開
		Select, // 選択時
		Reachable, // 角色可達
		Attackable, // 角色攻撃可能
	}

	void Start()
	{
		// 獲取高亮對象
		selectionBlockObj = transform.GetChild(0).gameObject; // 作為第一個孩子的對象

		// 初期状態では強調表示をしない
		SetSelectionMode(Highlight.Off);

       
	}


	/// <summary>
	/// 選択状態表示オブジェクトの表示・非表示を設定する
	/// </summary>
	/// <param name="mode">強調表示モード</param>
	public void SetSelectionMode(Highlight mode)
	{
		switch (mode)
		{
			// 沒有突出顯示
			case Highlight.Off:
				selectionBlockObj.SetActive(false);
				break;
			// 選択時
			case Highlight.Select:
				selectionBlockObj.GetComponent<Renderer>().material = selMat_Select;
				selectionBlockObj.SetActive(true);
				break;
			// 角色到達可能
			case Highlight.Reachable:
				selectionBlockObj.GetComponent<Renderer>().material = selMat_Reachable;
				selectionBlockObj.SetActive(true);
				break;
			// 角色攻撃可能
			case Highlight.Attackable:
				selectionBlockObj.GetComponent<Renderer>().material = selMat_Attackable;
				selectionBlockObj.SetActive(true);
				break;
		}
	}
}