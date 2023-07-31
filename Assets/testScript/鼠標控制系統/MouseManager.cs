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

        public LayerMask raycastLayerMask;

        public event Action<Vector3Int> OnMouseClicked;

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
            if (Input.GetMouseButtonDown(1))
            {
                // 建立一條從滑鼠位置發射的射線
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                // 使用LayerMask進行射線檢測，忽略指定的層
                if (Physics.Raycast(ray, out hitInfo, raycastLayerMask) && hitInfo.collider != null &&
                    !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                {
                    print(hitInfo.collider.name);
                    if (hitInfo.collider.gameObject.CompareTag("Ground") || hitInfo.collider.gameObject.CompareTag("666"))
                    {
                        OnMouseClicked?.Invoke(MousePoint());
                    }
                }
            }




        }

        Vector3Int MousePoint()
        {
            Vector3 hitPosition = hitInfo.point;

            Vector3Int hitPositionInt = new Vector3Int(
                Mathf.RoundToInt(hitPosition.x),
                Mathf.RoundToInt(hitPosition.y),
                Mathf.RoundToInt(hitPosition.z)
            );
            print(hitPositionInt);
            return hitPositionInt;

        }
    }
}
