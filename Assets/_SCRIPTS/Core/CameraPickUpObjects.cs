using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    public float PickUpDistance = 3;


    private void Start()
    {
        
    }
    void Update()
    {


        RaycastHit hit;
        //ототбражение луча в редакторе для отладки, можно удалить после тестов
        Debug.DrawRay(transform.position, transform.forward * PickUpDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, PickUpDistance))
        {
            if (hit.transform.GetComponent<PickableObjects>())
            {
                if (InputManager.Instance.IsInteractPressed())
                    hit.transform.GetComponent<PickableObjects>().PickUpObject();

            }
        }
    }
}
