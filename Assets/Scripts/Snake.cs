
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Snake : MonoBehaviour
{
    public enum SnakeDirection 
    {
        Right,
        Left,
        Up,
        Down
    }

    public static Snake Instance {  get; private set; }

    public event EventHandler OnEatingFood;

    [SerializeField] private float moveInterval = 0.15f;
    [SerializeField] private GameObject snakeBody;

    [SerializeField]private SnakeBodyPrefab snakeBodyPrefab;

    private float timer = 0f;
    private SnakeDirection snakeDirection;

    private Vector3 inputVector;

    private List<GameObject> snakeBodyList = new List<GameObject>();
    private List<Vector3> snakeBodyPositionList = new List<Vector3>();

    private void Awake()
    {
        Instance = this;
        inputVector = Vector3.right;
        snakeDirection = SnakeDirection.Right;
    }

    private void Start()
    {
        snakeBodyList.Add(gameObject);
        snakeBodyPositionList.Add(transform.position);

    }

    private void Update()
    {
        if (GameInputs.Instance.IsRightPressed() && inputVector != Vector3.left)
        {
            inputVector = Vector3.right;
            snakeDirection = SnakeDirection.Right;
        }
        if (GameInputs.Instance.IsLeftPressed() && inputVector != Vector3.right)
        {
            inputVector = Vector3.left;
            snakeDirection = SnakeDirection.Left;
        }
        if (GameInputs.Instance.IsUpPressed() && inputVector != Vector3.down)
        {
            inputVector = Vector3.up;
            snakeDirection = SnakeDirection.Up;
        }
        if (GameInputs.Instance.IsDownPressed() && inputVector != Vector3.up)
        {
            inputVector = Vector3.down;
            snakeDirection = SnakeDirection.Down;
        }
        MoveSnake();
    }

    private void MoveSnake()
    {
        if (timer > moveInterval)
        {
            foreach(GameObject g in snakeBodyList)
            {
                g.transform.position += inputVector;

            }
            
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
            GrowSnake();
            OnEatingFood?.Invoke(this,EventArgs.Empty);
            food.DestroySelf();
        }
    }

    
    private void GrowSnake()
    {
        Vector3 currentSnakePosition=transform.position;
        Vector3 newBodyPartPosition=Vector3.zero;

        switch (snakeDirection)
        {
            case SnakeDirection.Up:
                newBodyPartPosition = new Vector3(currentSnakePosition.x, currentSnakePosition.y - 1, 0);
                break;
            case SnakeDirection.Down:
                newBodyPartPosition = new Vector3(currentSnakePosition.x, currentSnakePosition.y +1, 0);
                break;
            case SnakeDirection.Right:
                newBodyPartPosition = new Vector3(currentSnakePosition.x-1, currentSnakePosition.y, 0);
                break;
            case SnakeDirection.Left:
                newBodyPartPosition = new Vector3(currentSnakePosition.x+1, currentSnakePosition.y , 0);
                break;
            default:
                break;
        }

        Transform parent = transform;
        
        GameObject spawnedObject=Instantiate(snakeBody,newBodyPartPosition, Quaternion.identity,parent);

        snakeBodyList.Add(spawnedObject);
        snakeBodyPositionList.Add(spawnedObject.transform.position);
           
    }

}
