using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SetUP : MonoBehaviour
{
    public Button[] enhanceButton;
    public Vector2[] enhanceVector2;
    public bool enhancebool = false;
    public int buttonNum;




    // Start is called before the first frame update
    void Start()
    {
        buttonNum = enhanceButton.Length;
        for (int i = 0; i < buttonNum; i++)
        {
            enhanceVector2[i] = new Vector2(enhanceButton[i].transform.localPosition.x, enhanceButton[i].transform.localPosition.y);
            enhanceButton[i].transform.LeanMoveLocal(new Vector2(0, 0), 0.3f);
        }
    }

    public void ExpandEnhance()
    {
        if (!enhancebool)
        {
            enhancebool = true;
            for (int i = 0; i < buttonNum; i++)
            {
                enhanceButton[i].transform.LeanMoveLocal(enhanceVector2[i], 0.3f).setEaseInOutQuart().setLoopOnce();
            }
        }
        else
        {
            enhancebool = false;
            for (int i = 0; i < buttonNum; i++)
            {
                enhanceButton[i].transform.LeanMoveLocal(new Vector2(0, 0), 0.3f).setEaseInOutQuart().setLoopOnce();
            }
        }
        
    }



}
