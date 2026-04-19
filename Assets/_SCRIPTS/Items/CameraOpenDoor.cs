using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace CameraDoorScript
{
    public class CameraOpenDoor : MonoBehaviour
    {
        private float DoorDistanceOpen = 3;
        private InventoryInv inventory;

        private void Start()
        {
            inventory = GetComponentInParent<InventoryInv>();
        }

        private void OnEnable()
        {
            if (InputManager.Instance != null)
                InputManager.Instance.OnInteractPressed += OpenDoorRayecast;
        }

        private void OnDisable()
        {
            if (InputManager.Instance != null)
                InputManager.Instance.OnInteractPressed -= OpenDoorRayecast;
        }

        public void OpenDoorRayecast()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, DoorDistanceOpen))
            {
                DoorScript.Door door = hit.transform.GetComponent<DoorScript.Door>();

                if (door != null && inventory != null)
                {
                    door.TryOpen(inventory); // ВАЖНО
                }
            }
        }
    }
}
// namespace CameraDoorScript
// {
// 	public class CameraOpenDoor : MonoBehaviour
// 	{
//         private float DoorDistanceOpen = 3;

//         private void OnEnable()
//         {
//             if (InputManager.Instance != null)
//             {
//                 InputManager.Instance.OnInteractPressed += OpenDoorRayecast;
//             }
//         }

//         private void OnDisable()
//         {
//             if (InputManager.Instance != null)
//             {
//                 InputManager.Instance.OnInteractPressed -= OpenDoorRayecast;
//             }
//         }

//         public void OpenDoorRayecast()
//         {
//             RaycastHit hit;

//             if (Physics.Raycast(transform.position, transform.forward, out hit, DoorDistanceOpen))
//             {
//                 DoorScript.Door door = hit.transform.GetComponent<DoorScript.Door>();
//                 if (door != null)
//                 {
//                     door.OpenDoor();
//                 }
//             }
//         }
// 	}
// }

