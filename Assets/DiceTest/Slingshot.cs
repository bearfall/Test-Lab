using UnityEngine;

public class Slingshot : MonoBehaviour
{
    private Vector3 dragStartPosition;
    private bool isDragging = false;
     private GameObject targetObject = null;
    [SerializeField] private float maxDragDistance = 5f;
    [SerializeField] private float launchPowerMultiplier = 2f;




    void Update()
    {
        GetDiceByTapPos();
    }

    private void GetDiceByTapPos()
    {
         // タップ対象のオブジェクト

        // 從相機向點擊的方向投射光線
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {// Rayに当たる位置に存在するオブジェクトを取得(対象にColliderが付いている必要がある)
            targetObject = hit.collider.gameObject;
            print(targetObject.name);
        }

        // 対象オブジェクト(マップブロック)が存在する場合の処理
        if (targetObject != null && targetObject.tag.Contains("Dice"))
        {
            if (isDragging)
            {
                print("開始拉骰子");
                OnMouseDrag();
                /*
                Vector3 mousePosition = targetObject.transform.position;
                Vector3 desiredPosition = dragStartPosition + (mousePosition - dragStartPosition).normalized * maxDragDistance;
                transform.position = new Vector3(desiredPosition.x, desiredPosition.y, desiredPosition.z);
                */
            }

        }
    }


    private void OnMouseDown()
    {
        isDragging = true;
        dragStartPosition = transform.position;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        Launch();
    }
    
    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 desiredPosition = dragStartPosition + (mousePosition - dragStartPosition).normalized * maxDragDistance;
            transform.position = new Vector3(desiredPosition.x, desiredPosition.y, transform.position.z);
        }
    }
    
    private void Launch()
    {
        Vector3 launchDirection = (dragStartPosition - transform.position).normalized;
        float launchPower = Vector3.Distance(dragStartPosition, transform.position) * launchPowerMultiplier;

        // Apply launch force to the slingshot object's Rigidbody component
        GetComponent<Rigidbody>().AddForce(launchDirection * launchPower, ForceMode.Impulse);
    }
}
