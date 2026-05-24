using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private InventoryInv inventory;
    [SerializeField] private TimeScript timer;

    public Transform Player => player;
    public InventoryInv Inventory => inventory;
    public TimeScript Timer => timer;
}
