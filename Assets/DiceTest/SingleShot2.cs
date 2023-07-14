using UnityEngine;
namespace bearfall
{


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
        private Rigidbody rig;

        public float rotationSpeed = 10f;
        public LineRenderer lineRenderer; // �x�s LineRenderer �ե󪺤ޥ�
        public int pointCount = 100; // �u�g���u�W���I�ƶq
        Vector3 mousePosition;
        Vector3 dragDirection;
        float dragDistance;


        public int hitCount = 1;

        public TestGameManager1 TestGameManager1;
        public PlayerDiceManager playerDiceManager;
        private void Start()
        {
            TestGameManager1 = GameObject.Find("Manager").GetComponent<TestGameManager1>();
            playerDiceManager = GameObject.Find("Manager").GetComponent<PlayerDiceManager>();
            originalPosition = transform.position;
            rig = gameObject.GetComponent<Rigidbody>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = pointCount;
            //gameObject.SetActive(false);
        }

        private void Update()
        {
            if (isDragging)
            {


                mousePosition = GetWorldMousePosition();
                dragDirection = (mousePosition - originalPosition).normalized;
                dragDistance = Vector3.Distance(originalPosition, mousePosition);

                transform.Rotate(Vector3.one, rotationSpeed * Time.deltaTime);



                // �]�m�w���u���I�ƶq
                lineRenderer.positionCount = pointCount;


                if (dragDistance > maxDragDistance)
                {
                    dragDistance = maxDragDistance;
                    mousePosition = originalPosition + dragDirection * maxDragDistance;
                }

                transform.position = mousePosition; // Update the position // Update the position



                for (int i = 0; i < pointCount; i++)
                {
                    float t = i / (float)(pointCount - 1);
                    Vector3 point = CalculatePointOnTrajectory(t, launchDirection * -launchForce); // �p�⦱�u�W���I
                    lineRenderer.SetPosition(i, point);
                }





                if (dragDistance >= snapDistance)
                {
                    launchDirection = dragDirection; // Update the launch direction
                    launchForce = dragDistance * launchForceMultiplier;

                    // Apply force to the projectile in the opposite direction of the drag
                    projectile.velocity = launchDirection * launchForce;
                }
            }
        }



        Vector3 CalculatePointOnTrajectory(float t, Vector3 dragForce)
        {
            // �ھکԧ誺�O�q�M��V�p��u�g�y��W���I����m
            // �A�i�H�ϥΤ����Ψ�L��k�ӭp��u�g�y��W���I
            // �o�̥u�O�@�ӥܨҡA�A�ݭn�ھڧA���ݨD�i��A�����p��
            Vector3 initialPosition = transform.position;
            Vector3 initialVelocity = dragForce; // �o�̰��]�ԧ誺�O�q�Y����l�t��
            float gravity = Physics.gravity.y;
            float displacementY = initialVelocity.y * t + 0.5f * gravity * t * t;
            Vector3 displacementXZ = new Vector3(initialVelocity.x, 0f, initialVelocity.z) * t;
            Vector3 point = initialPosition + displacementXZ + Vector3.up * displacementY;
            return point;
        }



        private void OnMouseDown()
        {
            originalPosition = transform.position;
            rig = gameObject.GetComponent<Rigidbody>();
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.positionCount = pointCount;


            isDragging = true;
            lineRenderer.enabled = true;
            projectile.velocity = Vector3.zero;


        }

        private void OnMouseUp()
        {
            isDragging = false;

            rig.useGravity = true;

            // Launch the projectile in the opposite direction with the stored launch force
            projectile.velocity = launchDirection * -launchForce;

            lineRenderer.enabled = false; // �x�s LineRenderer �ե󪺤ޥ�
            playerDiceManager.isThrowDice = true;
            // Reset the position of the slingshot
            //transform.position = originalPosition;
        }

        private Vector3 GetWorldMousePosition()
        {
            Vector3 mousePosition = Input.mousePosition;
           mousePosition.z = Camera.main.transform.position.z;
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }




        private void OnCollisionEnter(Collision EnemyDice)
        {
            if (EnemyDice.gameObject.tag.Contains("EnemyDice") && hitCount > 0)
            {
                rig = EnemyDice.gameObject.GetComponent<Rigidbody>();
                rig.velocity = new Vector3(0, 7, 0);
                rig.AddTorque(3, 3, 3);
                hitCount -= 1;
                StartCoroutine(TestGameManager1.CheckEnemyNumber());
            }
        }
    }
}