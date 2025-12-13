using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    [SerializeField] private float moveInterval = 0.15f;

    private float timer = 0f;

    private Vector3 inputVector;

    private void Awake()
    {
        inputVector = Vector3.right;
    }

    private void Update()
    {
        if (GameInputs.Instance.IsRightPressed() && inputVector != Vector3.left)
        {
            inputVector = Vector3.right;
        }
        if (GameInputs.Instance.IsLeftPressed() && inputVector != Vector3.right)
        {
            inputVector = Vector3.left;
        }
        if (GameInputs.Instance.IsUpPressed() && inputVector != Vector3.down)
        {
            inputVector = Vector3.up;
        }
        if (GameInputs.Instance.IsDownPressed() && inputVector != Vector3.up)
        {
            inputVector = Vector3.down;
        }
        MoveSnake();
    }

    private void MoveSnake()
    {
        if (timer > moveInterval)
        {
            transform.position += inputVector;
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

}
