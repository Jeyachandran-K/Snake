using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    private int gridWidth = 34;
    private int gridHeight = 20;
    private int score;

    private void Awake()
    {
        Instance= this;
    }
    private void Start()
    {
        Snake.Instance.OnEatingFood += Snake_OnEatingFood;
    }

    private void Snake_OnEatingFood(object sender, System.EventArgs e)
    {
        score++;
    }

    public Vector3 GetRandomEmptyPosition()
    {
        while (true)
        {
            float posX = Mathf.FloorToInt(Random.Range(-gridWidth / 2, gridWidth / 2 - 1));
            float posY = Mathf.FloorToInt(Random.Range(-gridHeight / 2, gridHeight / 2 - 1));
            Vector3 candidate= new Vector3(posX + 0.5f, posY + 0.5f, 0);
            if(!Snake.Instance.OccupiesPosition(candidate)) return candidate;
        }
        
    }
    public int GetScore()
    {
        return score;
    }
    public float GetGridWidth()
    {
        return gridWidth;
    }
    public float GetGridHeight()
    {
        return gridHeight;
    }
}
