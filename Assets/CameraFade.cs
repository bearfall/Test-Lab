using UnityEngine;
using System.Collections;

public class CameraFade : MonoBehaviour {

	public float FadeInTime;
	public float FadeOutTime;
	public Color FadeInColor;
	public Color FadeOutColor;
	
	public static bool FadeInIsStart = false;
	public static bool FadeOutIsStart = false;
	public static bool FadeInIsDone = false;
	public static bool FadeOutIsDone = false;
	
	private Texture2D t2d;	//新建一個空紋理(2D)
	private GUIStyle gs;
	
	private static float a = 0;
	
	void Awake (){
		FadeInIsStart = false;	//控制參數
		FadeOutIsStart = false;	//控制參數
		FadeInIsDone = false;	//控制參數
		FadeOutIsDone = false;	//控制參數
		a = 0;	//控制參數
		
		t2d = new Texture2D (1, 1);	//?
		t2d.SetPixel (0, 0, FadeInColor);	//在x,y設置一個像素顏色
		t2d.Apply ();	//應用SetPixel設置好的紋理
		
		gs = new GUIStyle ();
		gs.normal.background = t2d;	//常態下的背景
	}
	
	void OnGUI (){
		GUI.depth = -1000;	//深度(圖層前後)
		GUI.Label (new Rect (0, 0, Screen.width, Screen.height), t2d, gs);	//GUI標籤(位置,紋理,樣式)
	}
	
	void Update () {
		if(FadeInIsStart){
			if (a > 0) {
				a -= Time.deltaTime / FadeInTime;
				t2d.SetPixel (0, 0, new Color (FadeInColor.r, FadeInColor.g, FadeInColor.b, a));
				t2d.Apply ();
			}else{
				FadeInIsStart = false;
				FadeInIsDone = true;
			}
		}
		
		if(FadeOutIsStart){
			if (a < 1) {
				a += Time.deltaTime / FadeOutTime;
				t2d.SetPixel (0, 0, new Color (FadeOutColor.r, FadeOutColor.g, FadeOutColor.b, a));
				t2d.Apply ();
			}else{
				FadeOutIsStart = false;
				FadeOutIsDone = true;
			}
		}
		
	}
	
	// 淡入
	public static void FadeIn(){
		a = 1;
		FadeInIsStart = true;
		FadeInIsDone = false;
	}
	
	// 淡出
	public static void FadeOut(){
		a = 0;
		FadeOutIsStart = true;
		FadeOutIsDone = false;
	}
}
