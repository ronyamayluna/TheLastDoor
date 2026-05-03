

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
		public string requiredKeyID; // если пусто ключ не нужен

		private bool isUnlocked = false;

		[SerializeField]private float DoorOpenAngle = -90.0f;
		[SerializeField]private float DoorCloseAngle = 0.0f;

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
