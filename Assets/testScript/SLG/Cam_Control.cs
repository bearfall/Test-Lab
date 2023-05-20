using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Control : MonoBehaviour
{
    public Transform Map;                   // Target 物件
    public Transform fixed_X_Rotate_point;  // 利用此點做拖拉平移 (旋轉點)

    // 最小、最大縮放距離
    public float minZoomDist;
    public float maxZoomDist;

    public float cam_rotate_speed;
    public float cam_move_speed;
    public float cam_zoom_speed;

    // 滑鼠位置
    private float rotate_x = 0;
    private float rotate_y = 0;
    private float mouse_x = 0;
    private float mouse_y = 0;

    // 將按鍵編號用
    private enum MouseButton { MouseBtn_Left = 0, MouseBtn_Right = 1, MouseBtn_roll = 2 }

    private void Update()
    {
        Move();
        Rotate();
        Zoom();
    }


    void Rotate()
    {
        // 取得按鍵事件
        if (Map != null && Input.GetMouseButton((int)MouseButton.MouseBtn_Right))
        {
            // 獲得滑鼠移動變量
            rotate_x += Input.GetAxis("Mouse X") * cam_rotate_speed;
            rotate_y -= Input.GetAxis("Mouse Y") * cam_rotate_speed;

            // 利用滑鼠移動變量計算旋轉角度
            var rotate = Quaternion.Euler(-rotate_y, -rotate_x, 0);

            // 旋轉鏡頭
            transform.rotation = rotate;

            // 以 y 軸旋轉 fixed_X_rotate ( 旋轉基準點 ) 
            rotate = Quaternion.Euler(0, -rotate_x, 0);
            fixed_X_Rotate_point.rotation = rotate;
        }
    }


    void Move()
    {
        // 取得按鍵事件
        if (Map != null && Input.GetMouseButton((int)MouseButton.MouseBtn_Left))
        {
            // 獲得滑鼠移動變量
            mouse_x = Input.GetAxis("Mouse X") * cam_move_speed;
            mouse_y = Input.GetAxis("Mouse Y") * cam_move_speed;

            // 移動鏡頭
            transform.position -= fixed_X_Rotate_point.forward * mouse_y;
            transform.position -= fixed_X_Rotate_point.right * mouse_x;
        }
    }

    void Zoom()
    {
        if (Map != null && (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0))
        {
            // 計算 Map 與鏡頭距離
            Vector3 diff_vector = transform.position - Map.position;

            // 防止太過靠近
            if (diff_vector.y <= minZoomDist && Input.GetAxis("Mouse ScrollWheel") > 0)
                return;

            // 獲得滑鼠滾輪旋轉變量
            float zoomD = Input.GetAxis("Mouse ScrollWheel") * cam_zoom_speed;

            // 移動鏡頭
            transform.position += transform.forward * zoomD;

        }
    }
}
