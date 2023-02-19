using UnityEngine;
using System.Collections;

//=================================================================
//	用於閃爍可行走範圍,以及指到的方格變成紅色
//=================================================================


public class ColorChagne : MonoBehaviour {

	public Material color;	//基本色(放入ChessColor)
	public Material colorRed;	//滑鼠移入時的顏色(放入ChessColorRed)

	private bool choose = true;	//用來判斷alpha現在是要增加還是要減少
	private bool mouse = false;	//滑鼠移入時為true,移出時為false

	private float alpha = 230.0f;	//透明度起始值


//-----------------------------------------------------------------------------------
	
	void  Start ()
	{

	}

//-----------------------------------------------------------------------------------
	
	void  Update ()
	{
		change ();	//用來控制可行走範圍的閃爍,以及滑鼠移入的那格變成紅色(不閃爍)
	}
	
//-----------------------------------------------------------------------------------

	void OnMouseEnter () 
	{
		mouse = true;	//滑鼠移入時
	}

//-----------------------------------------------------------------------------------

	void OnMouseExit ()
	{
		mouse = false ;	//滑鼠移出時
	}

//-----------------------------------------------------------------------------------

	void change()
	{
		if(mouse == false)	//如果滑鼠沒移入
		{
			if(alpha > 120.0f && choose == true)	//如果alpha大於120
			{
				alpha = alpha - 10.0f * Time.deltaTime * 5;	//alpha減少
				//更新材質球的alpha值
				GetComponent<Renderer>().material.color = new Color(color.color.r ,color.color.g ,color.color.b ,alpha / 255.0f);
			}
			
			else 
			{
				choose = false;	//當alpha小於120時,必須先令上面的if停止動作,否則一旦alpha變成121時,上面的if又會開始讓alpha遞減
				alpha = alpha + 10.0f * Time.deltaTime * 5;	//alpha增加
				//更新材質球的alpha值
				GetComponent<Renderer>().material.color = new Color(color.color.r ,color.color.g ,color.color.b ,alpha / 255.0f);
				
				if(alpha > 230.0f)	//當alpha大於230時
					choose = true;	//開始讓alpha遞減
			}
		}

		if (mouse == true) //如果滑鼠移入,基本上和上面相同
		{
			if(alpha > 120.0f && choose == true)
			{
				alpha = alpha - 10.0f * Time.deltaTime * 5;	//雖然現在不閃爍,但此行不可刪,否則滑鼠移出時,alpha值會不統一
				//只是材質球換成另一顆,RGB這三個引數的內容換了,現在要變成紅色不閃爍
				GetComponent<Renderer>().material.color = new Color(colorRed.color.r ,colorRed.color.g ,colorRed.color.b ,230.0f/ 255.0f);
			}
			
			else 
			{
				choose = false;
				alpha = alpha + 10.0f * Time.deltaTime * 5;
				GetComponent<Renderer>().material.color = new Color(colorRed.color.r ,colorRed.color.g ,colorRed.color.b ,230.0f/ 255.0f);
				
				if(alpha > 230.0f)
					choose = true;
			}
		}
	}

//-----------------------------------------------------------------------------------
}
