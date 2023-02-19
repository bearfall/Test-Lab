using UnityEngine;
using UnityEngine.UI;
namespace bearfall
{


    public class LeaveNPC : MonoBehaviour
    {
        private string nameTarget = "ª±®a1";
        public GameObject canvas;
        public bool isOpen = false;
        // Start is called before the first frame update
        void Start()
        {
            canvas = GameObject.Find("BOSS¹ï¸Ü").GetComponent<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.name.Contains(nameTarget))
            {
                canvas.SetActive(false);
            }
        }
    }
}