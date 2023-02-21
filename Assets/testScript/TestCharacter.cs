﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestCharacter : MonoBehaviour
{


	public Path path;
	// メインカメラ
	private Camera mainCamera;



	public static int mSave = 0;    //暫存探索位置的 m 值,用於比較大小
	public static int targetChess = 0;  //存取aaa陣列的引數
	private int i = 0;  //迴圈計數用

	public static bool ChessBoard = false; //為true時,隱藏大棋盤
	public static bool chose = false;   //防止移動中偵測到滑鼠"左鍵"被點擊的誤判
	public static bool delete = false;  //用於判斷刪除棋盤的時機

	public static List<Vector3> aaa = new List<Vector3>();  //用來儲存最短路徑的陣列






	// キャラクター初期設定(インスペクタから入力)
	[Header("初期X位置(-4～4)")]
	public int initPos_X; // 初期X位置
	[Header("初期Z位置(-4～4)")]
	public int initPos_Z; // 初期Z位置
	[Header("敵フラグ(ONで敵キャラとして扱う)")]
	public bool isEnemy; // 敵フラグ
						 // キャラクターデータ(初期ステータス)
	[Header("キャラクター名")]
	public string charaName; // キャラクター名
	[Header("最大HP(初期HP)")]
	public int maxHP; // 最大HP
	[Header("攻撃力")]
	public int atk; // 攻撃力
	[Header("防御力")]
	public int def; // 防御力
	[Header("属性")]
	public Attribute attribute; // 属性
								// ゲーム中に変化するキャラクターデータ
	[HideInInspector]
	public int xPos; // 現在のx座標
	[HideInInspector]
	public int zPos; // 現在のz座標
	[HideInInspector]
	public int nowHP; // 現在HP

	// キャラクター属性定義(列挙型)
	public enum Attribute
	{
		Water, // 水属性
		Fire,  // 火属性
		Wind,  // 風属性
		Soil,  // 土属性
	}

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
		nowHP = maxHP;

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
		//Vector3 movePos = Vector3.zero; // (0.0f, 0.0f, 0.0f)でVector3で初期化
										//movePos.x = targetXPos - xPos; // x方向的相對距離
										//movePos.z = targetZPos - zPos; // z方向的相對距離
										//transform.position += movePos;









		Vector3 nowPosition = new Vector3(targetXPos, 0, targetZPos);
		var b = PartnerPosition.partnerPosition.Exists(n => n == nowPosition); // <= b == true
		if (!b)
		{



			//先記錄目前點下的位置,它的 m 值是多少
			for (int z = 0; z < Path.Count; z++)
			{
				//在座標陣列裡,找尋和nowPosition座標相同的格子
				if ((nowPosition.x == Path.ppp[z].x) && (nowPosition.z == Path.ppp[z].z))
				{

					mSave = Path.mCount[z]; //暫存這格的 m 值
					aaa.Insert(targetChess, Path.ppp[z]);   //把這格的座標值,儲存到aaa陣列裡
					targetChess++;  //把存取用的索引值+1
				}
			}

			//Debug.Log ("mSave = " + mSave); //可以確認被點下的那格的 m 值是多少

			while ((nowPosition.x != Path.ppp[0].x) || (nowPosition.z != Path.ppp[0].z))    //直到走回Player的位置為止
			{

				for (i = 0; i < Path.Count; i++)    //迴圈最大值 = ppp[]的最大值-1(因為最後一個是空的) = Count
				{
					//向上探索
					if ((nowPosition.z + 1 == Path.ppp[i].z) && (nowPosition.x == Path.ppp[i].x))
						cmpM(); //比較探索方向的 m(剩餘行動力) 值，是否比前一個探索方向的 m(剩餘行動力) 值大

					//向下探索
					if ((nowPosition.z - 1 == Path.ppp[i].z) && (nowPosition.x == Path.ppp[i].x))
						cmpM();

					//向左探索
					if ((nowPosition.x - 1 == Path.ppp[i].x) && (nowPosition.z == Path.ppp[i].z))
						cmpM();

					//向右探索
					if ((nowPosition.x + 1 == Path.ppp[i].x) && (nowPosition.z == Path.ppp[i].z))
						cmpM();

				}

				nowPosition = aaa[targetChess]; //更換nowPosition的位置為 m 值最大的位置
				targetChess++;  //探索完一遍後,把儲存用的引數+1

			}


			//for(int j = targetChess - 1; j >= 0; j--)	//可以查看結果正不正確(將路徑搜尋的結果倒印)
			//	Debug.Log ("aaa = " + aaa[j]);

			//Debug.Log ("Destination = " + nowPosition);	//可以查看路徑搜尋的結果,是不是從目標點回到原點


			//delete = true;  //算完最短路徑之後,將delete設為true,藉此刪除棋盤
			//Path.camera = false;    //移動前拉近攝影機
			chose = true;   //這時候角色才能開始移動(請見PlayerController的腳本)
			Path.cancel = false;    //令滑鼠"右鍵"的功能失效,防止移動中亂按的誤判
			//ChessBoard = true;  //隱藏大棋盤
		}

		// キャラクターデータに位置を保存
		xPos = targetXPos;
		zPos = targetZPos;
	}


	void cmpM() //比較探索方向的 m(剩餘行動力) 值，是否比前一個探索方向的 m(剩餘行動力) 值大
	{
		if (Path.mCount[i] > mSave)
		{
			mSave = Path.mCount[i]; //如果比較大，就把mSave換成比較大的
			aaa.Insert(targetChess, Path.ppp[i]); //把 m 值比較大的那格的座標丟入陣列,取代 m 值比較小的那格的座標
		}
	}

	/// <summary>
	/// キャラクターの近接攻撃アニメーション
	/// </summary>
	/// <param name="targetChara">相手キャラクター</param>
	public void AttackAnimation(TestCharacter targetChara)
	{
		// 攻撃アニメーション(DoTween)
		// 相手キャラクターの位置へジャンプで近づき、同じ動きで元の場所に戻る
		transform.DOJump(targetChara.transform.position, // 指定座標までジャンプしながら移動する
				1.0f, // ジャンプの高さ
				1, // ジャンプ回数
				0.5f) // アニメーション時間(秒)
			.SetEase(Ease.Linear) // イージング(変化の度合)を設定
			.SetLoops(2, LoopType.Yoyo); // ループ回数・方式を指定
	}

}