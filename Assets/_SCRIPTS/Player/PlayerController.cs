// using Unity.Cinemachine;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class PlayerController : MonoBehaviour
// {
//     public float characterSpeed;
//     [SerializeField] private CharacterController _characterController;
//     [SerializeField] private CinemachineCamera _cinCam;

//     private Vector2 _move;
//     public void OnMove(InputValue val)
//     {
//         _move = val.Get<Vector2>();
//     }
//     private void Update()
//     {
//         _characterController.Move((GetForward() * _move.y + GetRight() * _move.x) * Time.deltaTime * characterSpeed);
//     }
//     private Vector3 GetForward()
//     {
//         Vector3 forward = _cinCam.transform.forward;
//         forward.y = 0f;

//         return forward.normalized;
//     }
//     private Vector3 GetRight()
//     {
//         Vector3 right = _cinCam.transform.right;
//         right.y = 0f;

//         return right.normalized;
//     }

// }
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float characterSpeed;

    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CinemachineCamera _cinCam;

    [Header("Footsteps")]
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private float stepInterval = 0.5f;

    private float stepTimer;

    private Vector2 _move;

    public void OnMove(InputValue val)
    {
        _move = val.Get<Vector2>();
    }

    private void Update()
    {
        Vector3 moveDir = (GetForward() * _move.y + GetRight() * _move.x);

        _characterController.Move(moveDir * Time.deltaTime * characterSpeed);

        HandleFootsteps(moveDir);
    }

    private void HandleFootsteps(Vector3 moveDir)
    {
        bool isMoving = moveDir.magnitude > 0.1f && _characterController.isGrounded;

        if (isMoving)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                footstepSource.Play();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
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