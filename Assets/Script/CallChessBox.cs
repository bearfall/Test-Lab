using UnityEngine;
using System.Collections;

public class CallChessBox : MonoBehaviour {

	public Transform chess;

	//Vector3 chessPosition = new Vector3 (0,0,0);
//	Quaternion chessRotation ;

	void Awake () {

		Instantiate (chess, chess.position, chess.rotation);
	
	}
	

}
