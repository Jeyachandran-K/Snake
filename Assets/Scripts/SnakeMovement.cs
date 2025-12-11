using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeMovement : MonoBehaviour
{
    [SerializeField] private float snakeSpeed;

    private float timer;
    private Vector3 snakeDirection;

    private void Awake()
    {
        snakeDirection = Vector3.right;
    }

    private void Update()
    {
        if (Keyboard.current.rightArrowKey.isPressed && snakeDirection!=Vector3.left)
        {   
            snakeDirection = Vector3.right; 
        }
        if (Keyboard.current.leftArrowKey.isPressed && snakeDirection != Vector3.right)
        {
            snakeDirection = Vector3.left;
        }
        if (Keyboard.current.upArrowKey.isPressed && snakeDirection != Vector3.down)
        {
            snakeDirection = Vector3.up;
        }
        if (Keyboard.current.downArrowKey.isPressed && snakeDirection != Vector3.up)
        {
            snakeDirection = Vector3.down;
        }

        MoveSnake(snakeDirection);
    }

    private void MoveSnake(Vector3 snakeDirection)
    {
        if (timer > 0.5f)
        {
            transform.position += snakeDirection * snakeSpeed;
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
