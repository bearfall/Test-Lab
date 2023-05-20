using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	// カメラ移動用変数
	private bool isCameraRotate; // カメラ回転中フラグ
	private bool isMirror; // 回転方向反転フラグ

	// 定数定義
	const float SPEED = 30.0f; // 回転速度

	void Update()
	{
		// カメラ回転処理
		if (isCameraRotate)
		{
			// 回転速度を計算する
			float speed = SPEED * Time.deltaTime* 2;
			// 回転方向反転フラグが立っているなら速度反転
			if (isMirror)
				speed *= -1.0f;

			// 圍繞基點位置旋轉相機
			transform.RotateAround(
				new Vector3(1, 0, 8), // 基点の位置(0, 0, 0)
				Vector3.up, // 回転軸
				speed // 回転速度
			);
		}
	}

	/// <summary>
	/// カメラ移動ボタンが押し始められた時に呼び出される処理
	/// </summary>
	/// <param name="rightMode">右向きフラグ(右移動ボタンから呼ばれた時trueになっている)</param>
	public void CameraRotate_Start(bool rightMode)
	{
		// 打開相機旋轉標誌
		isCameraRotate = true;
		// 應用旋轉方向反轉標誌
		isMirror = rightMode;
	}
	/// <summary>
	/// カメラ移動ボタンが押されなくなった時に呼び出される処理
	/// </summary>
	public void CameraRotate_End()
	{
		// 關閉相機旋轉標誌
		isCameraRotate = false;
	}
}