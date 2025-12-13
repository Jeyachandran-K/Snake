
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    public static Snake Instance {  get; private set; }

    public event EventHandler OnEatingFood;

    [SerializeField] private float moveInterval = 0.15f;
    [SerializeField] private GameObject snakeBody;

    private float timer = 0f;
    
    private int snakeLength;

    private Vector3 inputVector;

    private List<GameObject> snakeBodyList = new List<GameObject>();
    private List<Vector3> snakeBodyPositionList = new List<Vector3>();

    private void Awake()
    {
        Instance = this;
        inputVector = Vector3.right;
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

            Vector3 newHeadPosition = transform.position + inputVector;

            if (snakeBodyPositionList.Contains(newHeadPosition))
            {
                Debug.Log("Game Over!");
                transform.position = Vector3.zero;
                SceneManager.LoadScene(0);
            }

            for (int i = snakeBodyPositionList.Count - 1; i > 0; i--)
            {
                snakeBodyPositionList[i] = snakeBodyPositionList[i - 1];
            }
            snakeBodyPositionList[0] = newHeadPosition;

            for (int j = 0; j < snakeBodyList.Count; j++)
            {
                snakeBodyList[j].transform.position = snakeBodyPositionList[j];
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
            snakeLength++;
            GrowSnake();
            OnEatingFood?.Invoke(this,EventArgs.Empty);
            food.DestroySelf();
        }
        
    }

    
    private void GrowSnake()
    {
        Vector3 spawnPosition = snakeBodyPositionList[snakeBodyPositionList.Count-1];
        GameObject spawedObject = Instantiate(snakeBody,spawnPosition,Quaternion.identity);
        snakeBodyList.Add(spawedObject);
        snakeBodyPositionList.Add(spawnPosition);
    }
    public bool OccupiesPosition(Vector3 candiate)
    {
        foreach(Vector3 pos in snakeBodyPositionList)
        {
            if(pos==candiate) return true;
        }
        return false;
    }
    
}
