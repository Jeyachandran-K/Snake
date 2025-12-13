using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    public static Snake Instance {  get; private set; }

    public event EventHandler OnEatingFood;

    [SerializeField] private float moveInterval = 0.15f;

    private float timer = 0f;

    private Vector3 inputVector;

    private void Awake()
    {
        Instance = this;
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
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.TryGetComponent(out Food food))
        {
            OnEatingFood?.Invoke(this,EventArgs.Empty);
            food.DestroySelf();
        }
    }

}
