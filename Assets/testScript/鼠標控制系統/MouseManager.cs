using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace bearfall
{
    /*
    [System.Serializable]
    public class EventVector3 : UnityEvent<Vector3> { }
    */
    public class MouseManager : MonoBehaviour
    {
        public static MouseManager Instance;

        public TestGameManager1 testGameManager1;

        public Texture2D point, target, doorway;

        RaycastHit hitInfo;

        public event Action<Vector3> OnMouseClicked;

        private void Awake()
        {
            testGameManager1 = GetComponent<TestGameManager1>();

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
            if (testGameManager1.currentArea == TestGameManager1.AreaType.FreeExplore)
            {
                SetCursorTexture();
                MouseControl();
            }
            
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
            if (Input.GetMouseButtonDown(1) && hitInfo.collider != null && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if (hitInfo.collider.gameObject.CompareTag("Ground"))
                {
                    OnMouseClicked?.Invoke(hitInfo.point);
                }
            }




        }
    }
}
