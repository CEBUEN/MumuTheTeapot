using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1); // Load the first level or specified scene
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            int savedLevel = PlayerPrefs.GetInt("SavedLevel");
            SceneManager.LoadScene(savedLevel);

            LoadPlayerData();
        }
        else
        {
            Debug.Log("No saved game found!");
        }
    }

    public void QuitGame()
{
    Debug.Log("Quit Game");

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
}


    private void LoadPlayerData()
{
    float posX = PlayerPrefs.GetFloat("PlayerPosX", 0);
    float posY = PlayerPrefs.GetFloat("PlayerPosY", 0);
    float posZ = PlayerPrefs.GetFloat("PlayerPosZ", 0);
    float playerHealth = PlayerPrefs.GetFloat("PlayerHealth", 100); // Default health to 100 if no data

    GameObject player = GameObject.FindWithTag("Player");
    if (player != null)
    {
        player.transform.position = new Vector3(posX, posY, posZ);

        Health healthComponent = player.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.SetCurrentHealth(playerHealth);
        }
    }
}

}
