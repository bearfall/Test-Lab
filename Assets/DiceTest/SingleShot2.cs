using UnityEngine;

public class SingleShot2 : MonoBehaviour
{
    private Vector3 originalPosition;
    private bool isDragging = false;

    [SerializeField] private float maxDragDistance = 5f;
    [SerializeField] private float snapDistance = 2f;
    [SerializeField] private float launchForceMultiplier = 10f;
    [SerializeField] private Rigidbody projectile;

    private Vector3 launchDirection;
    private float launchForce;


    LineRenderer lineRenderer; // 儲存 LineRenderer 組件的引用
    public int pointCount = 100; // 彈射曲線上的點數量
    Vector3 mousePosition;
    Vector3 dragDirection;
    float dragDistance;

    private void Start()
    {
        originalPosition = transform.position;

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = pointCount;
    }

    private void Update()
    {
        if (isDragging)
        {
            mousePosition = GetWorldMousePosition();
            dragDirection = (mousePosition - originalPosition).normalized;
            dragDistance = Vector3.Distance(originalPosition, mousePosition);


            
            

            // 設置預覽線的點數量
            lineRenderer.positionCount = pointCount;


            if (dragDistance > maxDragDistance)
            {
                dragDistance = maxDragDistance;
                mousePosition = originalPosition + dragDirection * maxDragDistance;
            }

            transform.position = originalPosition - dragDirection * dragDistance; // Update the position // Update the position
            


            for (int i = 0; i < pointCount; i++)
            {
                float t = i / (float)(pointCount - 1);
                Vector3 point = CalculatePointOnTrajectory(t, launchDirection * -launchForce); // 計算曲線上的點
                lineRenderer.SetPosition(i, point);
            }





            if (dragDistance >= snapDistance)
            {
                launchDirection = -dragDirection; // Update the launch direction
                launchForce = dragDistance * launchForceMultiplier;

                // Apply force to the projectile in the opposite direction of the drag
                projectile.velocity = launchDirection * launchForce;
            }
        }
    }



    Vector3 CalculatePointOnTrajectory(float t, Vector3 dragForce)
    {
        // 根據拉扯的力量和方向計算彈射軌跡上的點的位置
        // 你可以使用公式或其他方法來計算彈射軌跡上的點
        // 這裡只是一個示例，你需要根據你的需求進行適當的計算
        Vector3 initialPosition = transform.position;
        Vector3 initialVelocity = dragForce; // 這裡假設拉扯的力量即為初始速度
        float gravity = Physics.gravity.y;
        float displacementY = initialVelocity.y * t + 0.5f * gravity * t * t;
        Vector3 displacementXZ = new Vector3(initialVelocity.x, 0f, initialVelocity.z) * t;
        Vector3 point = initialPosition + displacementXZ + Vector3.up * displacementY;
        return point;
    }

   

    private void OnMouseDown()
    {
        isDragging = true;
        lineRenderer.enabled = true;
        projectile.velocity = Vector3.zero;
    }

    private void OnMouseUp()
    {
        isDragging = false;

        // Launch the projectile in the opposite direction with the stored launch force
        projectile.velocity = launchDirection * -launchForce;

        lineRenderer.enabled = false; // 儲存 LineRenderer 組件的引用

        // Reset the position of the slingshot
        //transform.position = originalPosition;
    }

    private Vector3 GetWorldMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
