using UnityEngine;

namespace ClockPuzzle
{
    public class ClockInteractable : MonoBehaviour
    {
        [Header("Настройки вращения")]
        [SerializeField] private Transform arrowTransform; // Ссылка на стрелку, которую надо крутить
        [SerializeField] private float rotationSpeed = 100f; // Скорость вращения

        private ClockPuzzleManager manager;
        private bool isActivated = false;
        private bool shouldRotate = false;

        private void Start()
        {
            // Автоматически находим менеджер на сцене
            manager = FindFirstObjectByType<ClockPuzzleManager>();

            if (manager == null)
            {
                Debug.LogError("Забыли поставить ClockPuzzleManager на сцену!");
            }
        }

        // Этот метод вызывается из рейкаста игрока
        public void Interact()
        {
            // Если часы уже были нажаты, ничего не делаем (или можно крутить дальше, если нужно)
            if (isActivated) return;

            isActivated = true;
            shouldRotate = true;

            // Уведомляем менеджер, что эти часы активированы
            if (manager != null)
            {
                manager.RegisterClockActivation();
            }
        }

        private void Update()
        {
            // Стрелка крутится только после взаимодействия
            if (shouldRotate && arrowTransform != null)
            {
                arrowTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
        }
    }
}
