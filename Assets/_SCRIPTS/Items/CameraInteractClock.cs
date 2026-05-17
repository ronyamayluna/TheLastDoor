using UnityEngine;

namespace ClockPuzzle
{
    public class CameraInteractClock : MonoBehaviour
    {
        [SerializeField] private float interactDistance = 3f;

        private void OnEnable()
        {
            if (InputManager.Instance != null)
                InputManager.Instance.OnInteractPressed += TryInteract;
        }

        private void OnDisable()
        {
            if (InputManager.Instance != null)
                InputManager.Instance.OnInteractPressed -= TryInteract;
        }

        public void TryInteract()
        {
            RaycastHit hit;

            // Стреляем лучом вперед
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactDistance))
            {
                // Пытаемся найти компонент часов на объекте, в который попали
                ClockInteractable clock = hit.transform.GetComponent<ClockInteractable>();

                if (clock != null)
                {
                    clock.Interact(); // Взаимодействуем с часами
                }
            }
        }
    }
}
