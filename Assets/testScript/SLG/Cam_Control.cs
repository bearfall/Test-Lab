using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Control : MonoBehaviour
{
    public Transform Map;                   // Target ����
    public Transform fixed_X_Rotate_point;  // �Q�Φ��I����ԥ��� (�����I)

    // �̤p�B�̤j�Y��Z��
    public float minZoomDist;
    public float maxZoomDist;

    public float cam_rotate_speed;
    public float cam_move_speed;
    public float cam_zoom_speed;

    // �ƹ���m
    private float rotate_x = 0;
    private float rotate_y = 0;
    private float mouse_x = 0;
    private float mouse_y = 0;

    // �N����s����
    private enum MouseButton { MouseBtn_Left = 0, MouseBtn_Right = 1, MouseBtn_roll = 2 }

    private void Update()
    {
        Move();
        Rotate();
        Zoom();
    }


    void Rotate()
    {
        // ���o����ƥ�
        if (Map != null && Input.GetMouseButton((int)MouseButton.MouseBtn_Right))
        {
            // ��o�ƹ������ܶq
            rotate_x += Input.GetAxis("Mouse X") * cam_rotate_speed;
            rotate_y -= Input.GetAxis("Mouse Y") * cam_rotate_speed;

            // �Q�ηƹ������ܶq�p����ਤ��
            var rotate = Quaternion.Euler(-rotate_y, -rotate_x, 0);

            // �������Y
            transform.rotation = rotate;

            // �H y �b���� fixed_X_rotate ( �������I ) 
            rotate = Quaternion.Euler(0, -rotate_x, 0);
            fixed_X_Rotate_point.rotation = rotate;
        }
    }


    void Move()
    {
        // ���o����ƥ�
        if (Map != null && Input.GetMouseButton((int)MouseButton.MouseBtn_Left))
        {
            // ��o�ƹ������ܶq
            mouse_x = Input.GetAxis("Mouse X") * cam_move_speed;
            mouse_y = Input.GetAxis("Mouse Y") * cam_move_speed;

            // �������Y
            transform.position -= fixed_X_Rotate_point.forward * mouse_y;
            transform.position -= fixed_X_Rotate_point.right * mouse_x;
        }
    }

    void Zoom()
    {
        if (Map != null && (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0))
        {
            // �p�� Map �P���Y�Z��
            Vector3 diff_vector = transform.position - Map.position;

            // ����ӹL�a��
            if (diff_vector.y <= minZoomDist && Input.GetAxis("Mouse ScrollWheel") > 0)
                return;

            // ��o�ƹ��u�������ܶq
            float zoomD = Input.GetAxis("Mouse ScrollWheel") * cam_zoom_speed;

            // �������Y
            transform.position += transform.forward * zoomD;

        }
    }
}
