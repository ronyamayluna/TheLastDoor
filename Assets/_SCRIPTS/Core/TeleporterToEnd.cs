using UnityEngine;

public class SimpleTeleport : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private Transform teleportExit; // Куда телепортируем (пустой объект-точка)
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private TimeScript timer;

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что это игрок 
        if (other.CompareTag(playerTag) || (other.transform.root != null && other.transform.root.CompareTag(playerTag)))
        {
            TeleportPlayer(other.transform.root != null ? other.transform.root : other.transform);
        }
    }

    private void TeleportPlayer(Transform player)
    {
        if (timer != null) timer.StopTimer();
        // Если на игроке есть CharacterController, его нужно отключить перед перемещением
        CharacterController cc = player.GetComponent<CharacterController>();

        if (cc != null) cc.enabled = false;

        // Переносим позицию и поворот
        player.position = teleportExit.position;
        player.rotation = teleportExit.rotation;

        if (cc != null) cc.enabled = true;
    }
}
