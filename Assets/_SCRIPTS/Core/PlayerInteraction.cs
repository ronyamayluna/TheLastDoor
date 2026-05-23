using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float interactDistance = 3f;
    [SerializeField] private LayerMask interactLayer;

    // Публичное свойство, чтобы любые объекты могли получить инвентарь
    public InventoryInv Inventory { get; private set; }

    private void Start()
    {
        Inventory = GetComponentInParent<InventoryInv>();
        if (Inventory == null)
        {
            Debug.LogError("InventoryInv не найден на игроке или камере!");
        }
    }

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

    private void TryInteract()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        // Пускаем ЕДИНЫЙ луч для дверей, шкафов и подбираемых предметов
        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                // Передаем этот скрипт внутрь объекта
                interactable.Interact(this);
            }
        }
    }
}




