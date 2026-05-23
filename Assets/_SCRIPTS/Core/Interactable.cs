public interface IInteractable
{
    // Объект получает ссылку на игрока и сам решает, что у него взять
    void Interact(PlayerInteraction player);
}


