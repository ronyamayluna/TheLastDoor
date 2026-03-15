using JetBrains.Annotations;
using UnityEngine;


public class PickableObjects : MonoBehaviour
{
    public bool isPickedUp = false;

    private void Update()
    {

    }
    public void PickUpObject()
    {
        //    if (!isPickedUp && CompareTag("PickableObject"))
        //    {
        isPickedUp = true;
            Destroy(this.gameObject);
        //}
    }
}
