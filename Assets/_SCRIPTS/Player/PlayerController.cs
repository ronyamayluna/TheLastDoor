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

    // Переменная контроля движения
    private bool canMove = true;

    public void OnMove(InputValue val)
    {
        if (!canMove) return; // Игнорируем ввод, если движение заблокировано
        _move = val.Get<Vector2>();
    }

    private void Update()
    {
        if (!canMove) return; // Останавливаем выполнение Update, если ходить нельзя

        Vector3 moveDir = (GetForward() * _move.y + GetRight() * _move.x);
        _characterController.Move(moveDir * Time.deltaTime * characterSpeed);

        HandleFootsteps(moveDir);
    }

    // МЕТОДЫ ДЛЯ БЛОКИРОВКИ УПРАВЛЕНИЯ
    public void DisableMovement()
    {
        canMove = false;
        _move = Vector2.zero; // Сбрасываем движение, чтобы игрок замер
    }

    public void EnableMovement()
    {
        canMove = true;
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
