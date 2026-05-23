using UnityEngine;

public class Clock : MonoBehaviour, IInteractable
{
    [Header("Clock Settings")]
    [SerializeField] private GameObject[] thingsToRotate = new GameObject[1];
    [SerializeField] private float speed = -25f;

    [Header("Reward")]
    [SerializeField] private GameObject keyObject;

    private bool isActivated = false;

    public void Interact(PlayerInteraction player)
    {
        if (isActivated) return;

        isActivated = true;
        if (keyObject != null)
        {
            keyObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            foreach (GameObject arrow in thingsToRotate)
            {
                if (arrow != null)
                {
                    arrow.transform.Rotate(0, 0, speed * Time.deltaTime);
                }
            }
        }
    }
}

