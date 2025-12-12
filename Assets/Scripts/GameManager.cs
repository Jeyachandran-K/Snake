using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    private int gridWidth = 20;
    private int gridHeight = 20;
    private Vector2Int foodPadding = new Vector2Int(1, 1);
    private Vector2Int foodPosition;

    [SerializeField] private Snake snake;
    [SerializeField] private Food food;

    [Header("Prefabs")]
    [SerializeField] private GameObject foodPrefab;
    [SerializeField] private GameObject snakeBodyPrefab;
    private bool isRunning = false;

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        SpawnFood();
    }

    private Vector2Int GetRandomEmptyGridPosition()
    {

        while (true)
        {
            int x = Random.Range(foodPadding.x, gridWidth - foodPadding.x);
            int y = Random.Range(foodPadding.y, gridWidth - foodPadding.y);

            Vector2Int candidate= new Vector2Int(x, y);
            if (!snake.OccupiesPositon(candidate))
                return candidate;
        }    
    }
    private Vector3 GridToWorld(Vector2Int gridPosition)
    {
        float offSetX = -gridWidth/2+0.5f;
        float offSetY = -gridHeight/2+0.5f;

        return new Vector3(gridPosition.x + offSetX, gridPosition.y + offSetY, 0);
    }
    private Vector2 WorldToGrid(Vector3 worldPosition)
    {
        float offSetX =Mathf.FloorToInt( gridWidth / 2 + 0.5f);
        float offSetY =Mathf.FloorToInt( gridHeight/ 2 + 0.5f);

        return new Vector2(worldPosition.x+offSetX,worldPosition.y +offSetY);
    }

    private void SpawnFood()
    {
        foodPosition=GetRandomEmptyGridPosition();
        Vector3 worldFoodPosition = GridToWorld(foodPosition);
        Instantiate(foodPrefab, worldFoodPosition, Quaternion.identity);
    }
    public void OnFoodEaten()
    {
        SpawnFood();
    }
    public bool IsInsideGrid(Vector2Int gridPosition)
    {
        return gridPosition.x>0 && gridPosition.y >0 && gridPosition.x<gridWidth && gridPosition.y<gridHeight;
    }
}
