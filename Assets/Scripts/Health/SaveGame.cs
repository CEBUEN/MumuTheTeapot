using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGame : MonoBehaviour
{
    private void Update()
    {
        // Press S to save and return to the main menu
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGameData();
            LoadMainMenu();
        }
    }

    public void SaveGameData()
    {
        int currentLevel = SceneController.instance != null
            ? SceneController.instance.GetCurrentSceneIndex()
            : 0;

        PlayerPrefs.SetInt("SavedLevel", currentLevel);

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 position = player.transform.position;
            PlayerPrefs.SetFloat("PlayerPosX", position.x);
            PlayerPrefs.SetFloat("PlayerPosY", position.y);
            PlayerPrefs.SetFloat("PlayerPosZ", position.z);

            Health healthComponent = player.GetComponent<Health>();
            if (healthComponent != null)
            {
                PlayerPrefs.SetFloat("PlayerHealth", healthComponent.currentHealth);
            }
        }

        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
    }

    private void LoadMainMenu()
    {
        // Assuming the main menu is at scene index 0
        SceneManager.LoadScene("MainMenu");
    }
}
