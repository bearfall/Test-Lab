using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIコンポーネントを扱うのに必要
using DG.Tweening;
public class TestGUIManager : MonoBehaviour
{
	// ステータスウィンドウUI
	public GameObject statusWindow; // ステータスウィンドウオブジェクト
	public Text nameText; // 名前Text
	public Image attributeIcon; // 属性アイコンImage
	public Image hpGageImage; // HPゲージImage
	public Text hpText; // HPText
	public Text atkText; // 攻撃力Text
	public Text defText; // 防御力Text
						 // 属性アイコン画像
	public Sprite attr_Water; // 水属性アイコン画像
	public Sprite attr_Fire;  // 火属性アイコン画像
	public Sprite attr_Wind;  // 風属性アイコン画像
	public Sprite attr_Soil;  // 土属性アイコン画像


	// キャラクターのコマンドボタン
	public GameObject commandButtons; // 全コマンドボタンの親オブジェクト

	public TestBattleWindowUI testBattleWindowUI;

	// 各種ロゴ画像
	public Image playerTurnImage; // プレイヤーターン開始時画像
	public Image enemyTurnImage; // 敵ターン開始時画像


	void Start()
	{
		// UI初期化
		HideStatusWindow(); // ステータスウィンドウを隠す
		HideCommandButtons(); // コマンドボタンを隠す
	}

	/// <summary>
	/// ステータスウィンドウを表示する
	/// </summary>
	/// <param name="charaData">表示キャラクターデータ</param>
	public void ShowStatusWindow(TestCharacter charaData)
	{
		// オブジェクトアクティブ化
		statusWindow.SetActive(true);

		// 名前Text表示
		nameText.text = charaData.charaName;

		// 属性Image表示
		switch (charaData.attribute)
		{
			case TestCharacter.Attribute.Water:
				attributeIcon.sprite = attr_Water;
				break;
			case TestCharacter.Attribute.Fire:
				attributeIcon.sprite = attr_Fire;
				break;
			case TestCharacter.Attribute.Wind:
				attributeIcon.sprite = attr_Wind;
				break;
			case TestCharacter.Attribute.Soil:
				attributeIcon.sprite = attr_Soil;
				break;
		}

		// HPゲージ表示
		// 最大値に対する現在HPの割合をゲージImageのfillAmountにセットする
		float ratio = (float)charaData.nowHP / charaData.maxHP;
		hpGageImage.fillAmount = ratio;

		// HPText表示(現在値と最大値両方を表示)
		hpText.text = charaData.nowHP + "/" + charaData.maxHP;
		// 攻撃力Text表示(intからstringに変換)
		atkText.text = charaData.atk.ToString();
		// 防御力Text表示(intからstringに変換)
		defText.text = charaData.def.ToString();
	}
	/// <summary>
	/// ステータスウィンドウを隠す
	/// </summary>
	public void HideStatusWindow()
	{
		// オブジェクト非アクティブ化
		statusWindow.SetActive(false);
	}

	public void ShowCommandButtons()
	{
		commandButtons.SetActive(true);
	}




	/// <summary>
	/// コマンドボタンを隠す
	/// </summary>
	public void HideCommandButtons()
	{
		commandButtons.SetActive(false);
	}




	/// <summary>
	/// プレイヤーのターンに切り替わった時のロゴ画像を表示する
	/// </summary>
	public void ShowLogo_PlayerTurn()
	{
		// 徐々に表示→非表示を行うアニメーション(Tween)
		playerTurnImage
			.DOFade(1.0f, // 指定数値まで画像のalpha値を変化
				1.0f) // アニメーション時間(秒)
			.SetEase(Ease.OutCubic) // イージング(変化の度合)を設定
			.SetLoops(2, LoopType.Yoyo); // ループ回数・方式を指定
	}
	/// <summary>
	/// 敵のターンに切り替わった時のロゴ画像を表示する
	/// </summary>
	public void ShowLogo_EnemyTurn()
	{
		// 徐々に表示→非表示を行うアニメーション(Tween)
		enemyTurnImage
			.DOFade(1.0f, // 指定数値まで画像のalpha値を変化
				1.0f) // アニメーション時間(秒)
			.SetEase(Ease.OutCubic) // イージング(変化の度合)を設定
			.SetLoops(2, LoopType.Yoyo); // ループ回数・方式を指定
	}
}