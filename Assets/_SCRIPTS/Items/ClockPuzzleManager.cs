using UnityEngine;

namespace ClockPuzzle
{
    public class ClockPuzzleManager : MonoBehaviour
    {
        [Header("Настройки загадки")]
        [SerializeField] private int totalClocks = 3; // Сколько всего часов нужно нажать
        [SerializeField] private GameObject keyObject; // Ссылка на объект ключа на сцене

        private int activatedClocksCount = 0; // Сколько часов уже нажато

        private void Start()
        {
            // В начале игры прячем ключ, если забыли выключить в инспекторе
            if (keyObject != null)
            {
                keyObject.SetActive(false);
            }
        }

        // Метод, который вызывают каждые часы при нажатии
        public void RegisterClockActivation()
        {
            activatedClocksCount++;

            // Проверяем, выполнено ли условие
            if (activatedClocksCount >= totalClocks)
            {
                SpawnKey();
            }
        }

        private void SpawnKey()
        {
            if (keyObject != null)
            {
                keyObject.SetActive(true); // Ключ появляется!
                Debug.Log("Все часы активированы! Ключ появился.");
            }
        }
    }
}
