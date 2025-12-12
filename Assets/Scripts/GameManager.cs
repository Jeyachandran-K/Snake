using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    private int gridWidth = 20;
    private int gridHeight = 20;
    private Vector2Int foodPadding = new Vector2Int(1, 1);

    [Header("Prefabs")]
    [SerializeField] private Snake snake;
    //[SerializeField] private Food food;

    [Header("GamePlay")]
    [SerializeField] private float moveInterval = 0.15f;

    private bool isRunning = false;

    private void Awake()
    {
        Instance = this;
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
        float offSetX = gridWidth / 2 + 0.5f;
        float offSetY = gridHeight/ 2 + 0.5f;

        return new Vector2(worldPosition.x+offSetX,worldPosition.y +offSetY);
    }

}
