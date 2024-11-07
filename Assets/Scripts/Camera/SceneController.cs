using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        // Ensure that there's only one instance of SceneController
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Optional, keeps the object alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Method to load the next level
    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public int GetCurrentSceneIndex()
    {
    return SceneManager.GetActiveScene().buildIndex;
    }

}
