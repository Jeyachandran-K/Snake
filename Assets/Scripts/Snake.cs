
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    public enum GameState
    {
        Wait,
        Play,
        End
    }

    public static Snake Instance {  get; private set; }

    public event EventHandler OnEatingFood;
    public event EventHandler OnStateChangeToPlay;

    [SerializeField] private float moveInterval = 0.15f;
    [SerializeField] private GameObject snakeBody;

    private float timer = 0f;
    
    private int snakeLength;

    private Vector3 inputVector;

    private GameState gameState;

    private List<GameObject> snakeBodyList = new List<GameObject>();
    private List<Vector3> snakeBodyPositionList = new List<Vector3>();

    private void Awake()
    {
        Instance = this;

        gameState = GameState.Wait;
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
        if (GameInputs.Instance.IsSpacePressed())
        {
            gameState = GameState.Play;
            OnStateChangeToPlay?.Invoke(this,EventArgs.Empty);
        }
        if (gameState == GameState.Play) 
        {
            MoveSnake();
        }
    }

    private void MoveSnake()
    {
        if (timer > moveInterval)
        {

            Vector3 newHeadPosition = transform.position + inputVector;

            if (snakeBodyPositionList.Contains(newHeadPosition))
            {
                GameOver();
            }
            newHeadPosition=TeleportSnake(newHeadPosition);

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
        if(collider2D.gameObject.TryGetComponent(out BoundaryWall boundaryWall))
        {
            GameOver();
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
    private Vector3 TeleportSnake(Vector3 newHeadPosition)
    {
        if (newHeadPosition.x > GameManager.Instance.GetGridWidth()/2 || newHeadPosition.x < -GameManager.Instance.GetGridWidth()/2)
        {
            newHeadPosition.x = -newHeadPosition.x;
        }
        if (newHeadPosition.y > GameManager.Instance.GetGridHeight()/2 || newHeadPosition.y < -GameManager.Instance.GetGridHeight()/ 2)
        {
            newHeadPosition.y = -newHeadPosition.y;
        }
        return newHeadPosition;
    }
    private void GameOver()
    {
        transform.position = Vector3.zero;
        SceneManager.LoadScene(0);
    }
}
