using UnityEngine;

namespace DoorScript
{
	public class Door : MonoBehaviour, IInteractable
	{
		private bool open;
		private float smooth = 1.0f;

		[Header("Key settings")]
		public string requiredKeyID;
		private bool isUnlocked = false;

		[SerializeField] private float DoorOpenAngle = -90.0f;
		[SerializeField] private float DoorCloseAngle = 0.0f;

		public AudioSource asource;
		public AudioClip openDoor, closeDoor;

		void Start() => asource = GetComponent<AudioSource>();

		void Update()
		{
			var target = open ? Quaternion.Euler(0, DoorOpenAngle, 0) : Quaternion.Euler(0, DoorCloseAngle, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * 5 * smooth);
		}

		// РЕАЛИЗАЦИЯ ИНТЕРФЕЙСА
		public void Interact(PlayerInteraction player)
		{
			if (string.IsNullOrEmpty(requiredKeyID) || isUnlocked)
			{
				ToggleDoor();
				return;
			}

			// Достаем инвентарь из игрока прямо здесь
			InventoryInv inventory = player.Inventory;

			if (inventory != null && inventory.HasItem(requiredKeyID))
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
			if (asource != null)
			{
				asource.clip = open ? openDoor : closeDoor;
				asource.Play();
			}
		}
	}
}

