using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TestBattleWindowUI : MonoBehaviour
{
	// バトル結果表示ウィンドウUI
	public Text nameText; // 名前Text
	public Image hpGageImage; // HPゲージImage
	public Text hpText; // HPText
	public Text damageText; // ダメージ量Text

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
	public void ShowWindow(TestCharacter charaData, int damageValue)
	{
		// オブジェクトアクティブ化
		gameObject.SetActive(true);

		// 名前Text表示
		nameText.text = charaData.charaName;

		// ダメージ計算後の残りHPを取得する
		// (ここでは対象キャラクターデータのHPは変化させない)
		int nowHP = charaData.nowHP - damageValue;
		// HPが0～最大値の範囲に収まるよう補正
		nowHP = Mathf.Clamp(nowHP, 0, charaData.maxHP);

		// HPゲージ表示
		float amount = (float)charaData.nowHP / charaData.maxHP;// 表示中のFillAmount(初期値はHP減少前のもの)
		float endAmount = (float)nowHP / charaData.maxHP;// アニメーション後のFillAmount
														 // HPゲージを徐々に減少させるアニメーション
														 // (DOFillAmountメソッドを使ってもよい)
		DOTween.To(// 変数を時間をかけて変化させる
				() => amount, (n) => amount = n, // 変化させる変数を指定
				endAmount, // 変化先の数値
				1.0f) // アニメーション時間(秒)
			.OnUpdate(() =>// アニメーション中毎フレーム実行される処理を指定
			{
				// 最大値に対する現在HPの割合をゲージImageのfillAmountにセットする
				hpGageImage.fillAmount = amount;
			});

		// HPText表示(現在値と最大値両方を表示)
		hpText.text = nowHP + "/" + charaData.maxHP;
		// ダメージ量Text表示
		damageText.text = damageValue + "ダメージ！";
	}
	/// <summary>
	/// バトル結果ウィンドウを隠す
	/// </summary>
	public void HideWindow()
	{
		// オブジェクト非アクティブ化
		gameObject.SetActive(false);
	}
}