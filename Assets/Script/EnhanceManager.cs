using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnhanceManager : MonoBehaviour
{
	// データクラス
	private Data data;

	// UIボタン
	public List<Button> EnhanceButtons; // ステータス上昇ボタン
	public Button GoGameButton; // もう一度プレイボタン

	void Start()
	{
		// データマネージャからデータ管理クラスを取得
		data = GameObject.Find("DataManager").GetComponent<Data>();

		// いずれかを強化するまでは「もう一度プレイ」ボタンを押せないようにする
		GoGameButton.interactable = false;
	}

	/// <summary>
	/// (ステータス上昇ボタン)
	/// 最大HPを上昇する
	/// </summary>
	public void Enhance_AddHP()
	{
		// 強化処理
		data.addHP += 2;

		EnhanceComplete(); // 強化完了時処理
	}
	/// <summary>
	/// (ステータス上昇ボタン)
	/// 攻撃力を上昇する
	/// </summary>
	public void Enhance_AddAtk()
	{
		// 強化処理
		data.addAtk += 1;

		EnhanceComplete(); // 強化完了時処理
	}
	/// <summary>
	/// (ステータス上昇ボタン)
	/// 防御力を上昇する
	/// </summary>
	public void Enhance_AddDef()
	{
		// 強化処理
		data.addDef += 1;

		EnhanceComplete(); // 強化完了時処理
	}
	/// <summary>
	/// プレイヤー強化完了時の共通処理
	/// </summary>
	private void EnhanceComplete()
	{
		// 強化ボタンを押下不可にする
		foreach (Button button in EnhanceButtons)
		{
			button.interactable = false;
		}
		// 「もう一度プレイ」ボタンを押下可能にする
		GoGameButton.interactable = true;

		// 変更をデータに保存
		data.WriteSaveData();
	}

	/// <summary>
	/// ゲームシーンに切り替える
	/// </summary>
	public void GoGameScene()
	{
		SceneManager.LoadScene("Game");
	}
}