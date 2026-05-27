using UnityEngine;

public class MenuScene : MonoBehaviour
{
    public void StartGame()
    {
        SceneManagerController.Instance.LoadScene("Juego");
    }

    public void QuitGame()
    {
        SceneManagerController.Instance.QuitGame();
    }
}
