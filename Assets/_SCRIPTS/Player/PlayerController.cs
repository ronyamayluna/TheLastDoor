using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float characterSpeed;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CinemachineCamera _cinCam;

    private Vector2 _move;
    public void OnMove(InputValue val)
    {
        _move = val.Get<Vector2>();
    }
    private void Update()
    {
        _characterController.Move((GetForward() * _move.y + GetRight() * _move.x) * Time.deltaTime * characterSpeed);
    }
    private Vector3 GetForward()
    {
        Vector3 forward = _cinCam.transform.forward;
        forward.y = 0f;

        return forward.normalized;
    }
    private Vector3 GetRight()
    {
        Vector3 right = _cinCam.transform.right;
        right.y = 0f;

        return right.normalized;
    }

}
