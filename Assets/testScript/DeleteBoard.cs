using UnityEngine;
using System.Collections;

//=================================================================
//	用於隱藏大棋盤,時機為點選了某一個方格之後
//=================================================================

public class DeleteBoard : MonoBehaviour {


	void Update () {
	
		if(ClickPosition.ChessBoard == true)	
			gameObject.SetActive (false);
	}
}
