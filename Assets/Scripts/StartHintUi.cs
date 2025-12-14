using UnityEngine;

public class StartHintUi : MonoBehaviour
{

    private void Start()
    {
        Snake.Instance.OnStateChangeToPlay += Snake_OnStateChangeToPlay;
    }

    private void Snake_OnStateChangeToPlay(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
