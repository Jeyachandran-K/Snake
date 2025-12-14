using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputs : MonoBehaviour
{
    public static GameInputs Instance {  get; private set; }

    private InputActions inputActions;

    private void Awake()
    {
        Instance = this;
        inputActions = new InputActions();
        inputActions.Snake.Enable();
    }
    private void OnDisable()
    {
        inputActions.Snake.Disable();
    }

    private void Update()
    {
        
    }
    public bool IsUpPressed()
    {
        return inputActions.Snake.Up.IsPressed();
    }
    public bool IsDownPressed()
    {
        return inputActions.Snake.Down.IsPressed();
    }
    public bool IsRightPressed()
    {
        return inputActions.Snake.Right.IsPressed();
    }
    public bool IsLeftPressed()
    {
        return inputActions.Snake.Left.IsPressed();
    }
    public bool IsSpacePressed()
    {
        return inputActions.Snake.StartGame.IsPressed();
    }

}
