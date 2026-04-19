

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DoorScript
{
	public class Door : MonoBehaviour
	{
		private bool open;
		private float smooth = 1.0f;

		[Header("Key settings")]
		public string requiredKeyID; // если пусто → ключ не нужен

		private bool isUnlocked = false;

		private float DoorOpenAngle = -90.0f;
		private float DoorCloseAngle = 0.0f;

		public AudioSource asource;
		public AudioClip openDoor, closeDoor;

		void Start()
		{
			asource = GetComponent<AudioSource>();
		}

		void Update()
		{
			var target = open
				? Quaternion.Euler(0, DoorOpenAngle, 0)
				: Quaternion.Euler(0, DoorCloseAngle, 0);

			transform.localRotation =
				Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
		}

		public void TryOpen(InventoryInv inventory)
		{

			if (string.IsNullOrEmpty(requiredKeyID))
			{
				ToggleDoor();
				return;
			}


			if (isUnlocked)
			{
				ToggleDoor();
				return;
			}

			if (inventory.HasItem(requiredKeyID))
			{
				inventory.RemoveItem(requiredKeyID);
				isUnlocked = true;
				ToggleDoor();
			}
			else
			{
				Debug.Log("Нужен ключ: " + requiredKeyID);
			}
		}

		private void ToggleDoor()
		{
			open = !open;

			asource.clip = open ? openDoor : closeDoor;
			asource.Play();
		}
	}
}
// namespace DoorScript
// {
// 	[RequireComponent(typeof(AudioSource))]


// 	public class Door : MonoBehaviour
// 	{
// 		private bool open;
// 		private float smooth = 1.0f;
// 		private float DoorOpenAngle = -90.0f;
// 		private float DoorCloseAngle = 0.0f;
// 		public AudioSource asource;
// 		public AudioClip openDoor, closeDoor;
// 		// Use this for initialization
// 		void Start()
// 		{
// 			asource = GetComponent<AudioSource>();
// 		}

// 		// Update is called once per frame
// 		void Update()
// 		{
// 			if (open)
// 			{
// 				var target = Quaternion.Euler(0, DoorOpenAngle, 0);
// 				transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
// 			}
// 			else
// 			{
// 				var target1 = Quaternion.Euler(0, DoorCloseAngle, 0);
// 				transform.localRotation = Quaternion.Slerp(transform.localRotation, target1, Time.deltaTime * 5 * smooth);
// 			}
// 		}

// 		public void OpenDoor()
// 		{
// 			open = !open;
// 			asource.clip = open ? openDoor : closeDoor;
// 			asource.Play();
// 		}
// 	}
// }