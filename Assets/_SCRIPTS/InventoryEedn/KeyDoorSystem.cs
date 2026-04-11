using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyDoorSystem : MonoBehaviour
{
    [System.Serializable]
    public class KeyDoorMapping
    {
        public string KeyTag;
        public GameObject Door;
        public Sprite KeyIcon;
        public bool IsOpen, HasBeenOpened;
        public Quaternion OpenRotation;
        public Quaternion ClosedRotation;
        public float OpenSpeed = 2f;
    }

    public List<KeyDoorMapping> KeyDoorMappings;
    public Image[] InventorySlots;
    public Text InteractionText;
    public Camera PlayerCamera;
    public Transform PlayerTransform;
    public float RaycastDistance = 5f;

    private List<string> inventory = new List<string>();
    private bool isInTransition = false;

    private void Start() => InteractionText.enabled = false;

    private void Update()
    {
        CheckForInteraction();
    }

    private void CheckForInteraction()
    {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, RaycastDistance))
        {
            bool interactionPossible = false;

            foreach (var mapping in KeyDoorMappings)
            {
                if (hit.collider.CompareTag(mapping.KeyTag) && Vector3.Distance(PlayerTransform.position, hit.collider.transform.position) <= RaycastDistance)
                {
                    HandleKey(mapping, hit);
                    interactionPossible = true;
                }
                else if (hit.collider.gameObject == mapping.Door && Vector3.Distance(PlayerTransform.position, hit.collider.transform.position) <= RaycastDistance)
                {
                    HandleDoor(mapping, hit);
                    interactionPossible = true;
                }
            }

            if (!interactionPossible)
            {
                InteractionText.enabled = false;
            }
        }
        else
        {
            InteractionText.enabled = false;
        }
    }

    private void HandleKey(KeyDoorMapping mapping, RaycastHit hit)
    {
        if (Vector3.Distance(PlayerTransform.position, hit.collider.transform.position) <= RaycastDistance)
        {
            InteractionText.text = "E";
            InteractionText.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                AddKeyToInventory(mapping, hit);
            }
        }
    }

    private void HandleDoor(KeyDoorMapping mapping, RaycastHit hit)
    {
        if (Vector3.Distance(PlayerTransform.position, hit.collider.transform.position) <= RaycastDistance)
        {
            InteractionText.text = "E";
            InteractionText.enabled = true;

            if (mapping.IsOpen && Input.GetKeyDown(KeyCode.E) && !isInTransition)
            {
                mapping.IsOpen = false;
                StartCoroutine(CloseDoorAnimation(mapping));
            }

            if (!isInTransition)
            {
                if (inventory.Contains(mapping.KeyTag))
                {
                    if (!mapping.IsOpen && Input.GetKeyDown(KeyCode.E))
                    {
                        mapping.IsOpen = true;
                        RemoveKeyFromInventory(mapping.KeyTag);
                        mapping.HasBeenOpened = true;
                        StartCoroutine(OpenDoorAnimation(mapping));
                    }
                }
                else if (mapping.HasBeenOpened)
                {
                    if (!mapping.IsOpen && Input.GetKeyDown(KeyCode.E))
                    {
                        mapping.IsOpen = true;
                        RemoveKeyFromInventory(mapping.KeyTag);
                        mapping.HasBeenOpened = true;
                        StartCoroutine(OpenDoorAnimation(mapping));
                    }
                }
            }
        }
    }

    private IEnumerator OpenDoorAnimation(KeyDoorMapping mapping)
    {
        isInTransition = true;
        Quaternion initialRotation = mapping.Door.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < mapping.OpenSpeed)
        {
            elapsedTime += Time.deltaTime;
            mapping.Door.transform.rotation = Quaternion.Slerp(initialRotation, mapping.OpenRotation, elapsedTime / mapping.OpenSpeed);
            yield return null;
        }

        mapping.Door.transform.rotation = mapping.OpenRotation;
        isInTransition = false;
    }

    private IEnumerator CloseDoorAnimation(KeyDoorMapping mapping)
    {
        isInTransition = true;
        Quaternion initialRotation = mapping.Door.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < mapping.OpenSpeed)
        {
            elapsedTime += Time.deltaTime;
            mapping.Door.transform.rotation = Quaternion.Slerp(initialRotation, mapping.ClosedRotation, elapsedTime / mapping.OpenSpeed);
            yield return null;
        }

        mapping.Door.transform.rotation = mapping.ClosedRotation;
        isInTransition = false;
    }

    private void AddKeyToInventory(KeyDoorMapping mapping, RaycastHit hit)
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            if (InventorySlots[i].sprite == null)
            {
                InventorySlots[i].sprite = mapping.KeyIcon;
                inventory.Add(mapping.KeyTag);
                Destroy(hit.collider.gameObject);
                return;
            }
        }
    }

    private void RemoveKeyFromInventory(string keyTag)
    {
        inventory.Remove(keyTag);
        UpdateInventoryUI();
    }

    private void UpdateInventoryUI()
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            if (i < inventory.Count)
                InventorySlots[i].sprite = KeyDoorMappings.Find(m => m.KeyTag == inventory[i]).KeyIcon;
            else
                InventorySlots[i].sprite = null;
        }
    }
}
