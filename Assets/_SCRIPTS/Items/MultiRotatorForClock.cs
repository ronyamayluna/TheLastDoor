using UnityEngine;
using System;

public class MultiRotatorForClock : MonoBehaviour
{
    [Serializable]
    public struct RotationSettings
    {
        public Transform targetObject;
        public float speed;
    }

    [SerializeField] private RotationSettings[] objectsToRotate;

    // Выключаем скрипт сразу при старте, чтобы Update не работал впустую
    void Start()
    {
        this.enabled = false;
    }

    void Update()
    {
        //Update работает ТОЛЬКО когда enabled == true
        foreach (var item in objectsToRotate)
        {
            if (item.targetObject != null)
            {
                item.targetObject.Rotate(0, 0, item.speed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.enabled = true; // Включаем Update
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.enabled = false; // Полностью "усыпляем" Update
        }
    }
}
