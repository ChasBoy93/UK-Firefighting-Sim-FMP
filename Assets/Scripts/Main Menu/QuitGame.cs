using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void QuitTheGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
