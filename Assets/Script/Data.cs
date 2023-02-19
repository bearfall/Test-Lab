using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
	// 單例管理變量
	// (通過靜態配置在所有實例之間共享此值)
	[HideInInspector]
	public static bool _instance = false;


	// プレイヤー強化データ
	public int addHP; // 最大HP上昇量
	public int addAtk; // 攻撃力上昇量
	public int addDef; // 防御力上昇量

	// データのキー定義
	public const string Key_AddHP = "Key_AddHP";
	public const string Key_AddAtk = "Key_AddAtk";
	public const string Key_AddDef = "Key_AddDef";

	// Awake(Start前只執行一次的方法)
	private void Awake()
	{
		// Processing for Singleton ...如果數據管理器已經存在於場景中，請刪除自己
		if (_instance)
		{
			Destroy(gameObject);
			return;
		}
		_instance = true; // 在靜態變量中記錄數據管理器的創建
						  // 設置該對像不會跨場景被擦除
		DontDestroyOnLoad(gameObject);


		// セーブデータをPlayerPrefsから読み込み
		addHP = PlayerPrefs.GetInt(Key_AddHP, 0); // Key_AddHPに対応するデータを読み込む。データが無い場合は設定した初期値(0)が入る
		addAtk = PlayerPrefs.GetInt(Key_AddAtk, 0);
		addDef = PlayerPrefs.GetInt(Key_AddDef, 0);


	}


	/// <summary>
	/// 現在のプレイヤー強化データをPlayerPrefsに保存する
	/// </summary>
	public void WriteSaveData()
	{
		PlayerPrefs.SetInt(Key_AddHP, addHP); // データをキー(Key_AddHP)とともに変更
		PlayerPrefs.SetInt(Key_AddAtk, addAtk);
		PlayerPrefs.SetInt(Key_AddDef, addDef);
		PlayerPrefs.Save(); // 変更を保存
	}

}