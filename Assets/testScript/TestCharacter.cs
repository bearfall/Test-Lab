using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour
{
	public Path path;
	// メインカメラ
	private Camera mainCamera;

	// キャラクター初期設定(インスペクタから入力)
	[Header("初期X位置(-4～4)")]
	public int initPos_X; // 初期X位置
	[Header("初期Z位置(-4～4)")]
	public int initPos_Z; // 初期Z位置

	// ゲーム中に変化するキャラクターデータ
	[HideInInspector]
	public int xPos; // 現在のx座標
	[HideInInspector]
	public int zPos; // 現在のz座標

	void Start()
	{
		// メインカメラの参照を取得
		mainCamera = Camera.main;

		// 初期位置に対応する座標へオブジェクトを移動させる
		Vector3 pos = new Vector3();
		pos.x = initPos_X; // x座標：1ブロックのサイズが1(1.0f)なのでそのまま代入
		pos.y = 0f; // y座標（固定）
		pos.z = initPos_Z; // z座標
		transform.position = pos; // オブジェクトの座標を変更

		// オブジェクトを左右反転(ビルボードの処理にて一度反転してしまう為)
		Vector2 scale = transform.localScale;
		scale.x *= -1.0f; // X方向の大きさを正負入れ替える
		transform.localScale = scale;

		// その他変数初期化
		xPos = initPos_X;
		zPos = initPos_Z;


		path = GetComponent<Path>();
	}

	void Update()
	{
		// ビルボード処理
		// (スプライトオブジェクトをメインカメラの方向に向ける)
		Vector3 cameraPos = mainCamera.transform.position; // 現在のカメラ座標を取得
		cameraPos.y = transform.position.y; // キャラが地面と垂直に立つようにする
		transform.LookAt(cameraPos);
	}
	public void MovePosition(int targetXPos, int targetZPos)
	{
		// 移動物體
		// 獲取目標坐標的相對坐標
		Vector3 movePos = Vector3.zero; // (0.0f, 0.0f, 0.0f)でVector3で初期化
		movePos.x = targetXPos - xPos; // x方向的相對距離
		movePos.z = targetZPos - zPos; // z方向的相對距離


		transform.position += movePos;



		// キャラクターデータに位置を保存
		xPos = targetXPos;
		zPos = targetZPos;
	}
}