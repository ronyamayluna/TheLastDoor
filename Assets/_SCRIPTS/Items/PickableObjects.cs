using UnityEngine;

public class PickableObjects : MonoBehaviour
{
    private bool isPickedUp = false;

    public void PickUpObject()
    {
        if (isPickedUp) return;
        isPickedUp = true;
        Destroy(this.gameObject);
    }
}
