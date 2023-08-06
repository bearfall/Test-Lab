using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TestBattleWindowUI : MonoBehaviour
{
	// バトル結果表示ウィンドウUI
	public GameObject energyBar;

	 
	
	void Start()
	{
		// 初期化時にウィンドウを隠す
		HideWindow();
	}

	/// <summary>
	/// バトル結果ウィンドウを表示する
	/// </summary>
	/// <param name="charaData">攻撃されたキャラクターのデータ</param>
	/// <param name="damageValue">ダメージ量</param>
	public void ShowWindow()
	{
		// オブジェクトアクティブ化
		energyBar.SetActive(true);

		// 名前Text表示
	
	}
	/// <summary>
	/// バトル結果ウィンドウを隠す
	/// </summary>
	public void HideWindow()
	{
		// オブジェクト非アクティブ化
		energyBar.SetActive(false);
	}
}