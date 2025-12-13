using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject foodPrefab;

    private Vector3 foodSpawnPosition;


    private void Snake_OnEatingFood(object sender, System.EventArgs e)
    {
        SpawnFood();
    }

    private void Start()
    {
        SpawnFood();

        Snake.Instance.OnEatingFood += Snake_OnEatingFood;
    }

    private void SpawnFood()
    {
        foodSpawnPosition = GameManager.Instance.GetRandomEmptyPosition();
        Instantiate(foodPrefab,foodSpawnPosition,Quaternion.identity);
    }
}
