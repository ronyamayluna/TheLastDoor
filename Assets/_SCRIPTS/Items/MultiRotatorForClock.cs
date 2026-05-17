using UnityEngine;

public class MultiRotatorForClock : MonoBehaviour
{
    [SerializeField] private float speed = -25f; 

    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}

