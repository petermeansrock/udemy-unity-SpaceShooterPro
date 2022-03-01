using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadSinglePlayerGame()
    {
        SceneManager.LoadScene("SinglePlayerMode");
    }

    public void LoadCoOpGame()
    {
        SceneManager.LoadScene("CoOpMode");
    }
}
