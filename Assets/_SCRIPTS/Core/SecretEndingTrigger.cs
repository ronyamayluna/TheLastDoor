using UnityEngine;

public class SecretEndingTrigger : MonoBehaviour
{
    [SerializeField] private SecretEndingController secretEndingController;
    [SerializeField] private string playerTag = "Player";

    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if (activated) return;

        if (!other.CompareTag(playerTag))
            return;

        activated = true;

        secretEndingController.StartSecretEnding();

        gameObject.SetActive(false);
    }
}
