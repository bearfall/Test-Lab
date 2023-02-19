using System.Collections.Generic;
using UnityEngine;

public class TestMapBlock : MonoBehaviour
{
	// 強調表示オブジェクト
	private GameObject selectionBlockObj;
	// ブロックデータ
	[HideInInspector] // インスペクタ上で非表示にする属性
	public int xPos; // X方向の位置	
	[HideInInspector]
	public int zPos; // Z方向の位置

	void Start()
	{
		// 強調表示オブジェクトを取得
		selectionBlockObj = transform.GetChild(0).gameObject; // 子の１番目にあるオブジェクト

		// 初期状態では強調表示をしない
		SetSelectionMode(false);
	}

	/// <summary>
	/// 強調表示オブジェクトの表示・非表示を設定する
	/// </summary>
	/// <param name="mode">選択状態(true:選択中)</param>
	
	public void SetSelectionMode(bool mode)
	{
		if (mode)// 選択中なら強調表示オブジェクトを表示
			selectionBlockObj.SetActive(true);
		else// 選択中でないなら強調表示オブジェクトを非表示
			selectionBlockObj.SetActive(false);
	}
}