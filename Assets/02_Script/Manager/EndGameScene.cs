using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void MenuGame()
    {
        SceneManagerController.Instance.LoadScene("Menu");
    }

    public void RetryGame()
    {
        SceneManagerController.Instance.LoadScene("Juego");
    }
}