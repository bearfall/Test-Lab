using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/*
[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }
*/
public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance;

    public Texture2D point, target, doorway;

    RaycastHit hitInfo;     

    public event Action<Vector3> OnMouseClicked;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        SetCursorTexture();
        MouseControl();
    }


    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;

            }


        }

    }

    void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground"))
            {
                OnMouseClicked?.Invoke(hitInfo.point);
            }
        }




    }
}
