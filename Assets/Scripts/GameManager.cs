using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    private int gridWidth = 34;
    private int gridHeight = 20;

    private void Awake()
    {
        Instance= this;
    }
    public Vector3 GetRandomEmptyPosition()
    {
        return new Vector3(Mathf.FloorToInt(Random.Range(-gridWidth/2 ,gridWidth/2))-0.5f,Mathf.FloorToInt(Random.Range(-gridHeight/2 ,gridHeight/2))-0.5f,0);
    }

}
