using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class menuManager : MonoBehaviour
{
    // --Menu panels--
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;
    public GameObject pauseMenu;
    public GameObject audioSettingsMenu;
    public GameObject controlSettingsMenu;
    public GameObject languageMenu;
    public GameObject gameOverMenu;
    public AudioClip buttonClick;

    public void Start() //Shows the main menu on start
    {
        ShowMainMenu();
    }

    public void ShowMainMenu() //Shows the main menu
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
        controlSettingsMenu.SetActive(false);
        languageMenu.SetActive(false);
    }

    public void ShowControlsMenu() //Show the controls menu
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
        controlSettingsMenu.SetActive(false);
        languageMenu.SetActive(false);
    }

    public void ShowSettingsMenu() //Show the settings menu
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
        controlSettingsMenu.SetActive(false);
        languageMenu.SetActive(false);
    }

    public void ShowAudioSettingsMenu() //Shows the audio settings menu
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        audioSettingsMenu.SetActive(true);
        controlSettingsMenu.SetActive(false);
        languageMenu.SetActive(false);
    }

    public void ShowControlSettingsMenu() //Shows the control settings menu
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
        controlSettingsMenu.SetActive(true);
        languageMenu.SetActive(false);
    }

    public void ShowLanguageMenu() //Shows the language menu
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
        controlSettingsMenu.SetActive(false);
        languageMenu.SetActive(true);
    }

    public void ShowPauseMenu() //Shows the pause menu
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
        audioSettingsMenu.SetActive(false);
        controlSettingsMenu.SetActive(false);
        languageMenu.SetActive(false);
        Time.timeScale = 0f; // Pauses the game by setting time to 0f
        Debug.Log("Game paused"); //Tells that the game is paused
    }

    public void ShowGameOverMenu()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
        controlSettingsMenu.SetActive(false);
        languageMenu.SetActive(false);
        gameOverMenu.SetActive(true);
    }

    public void ContinueGame() //Disables the pause menu
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        audioSettingsMenu.SetActive(false);
        controlSettingsMenu.SetActive(false);
        languageMenu.SetActive(false);
        Time.timeScale = 1f; // Un-pauses the game by re-setting time to 1f
        Debug.Log("Game no longer paused"); //Tells that the game is no longer paused
    }

    public void StartGame() //Loads the game scene
    {
        SceneManager.LoadScene("GameScene"); //Change ("GameScene") to actual scene name later
    }

    public void QuitGame() //Quits the application
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void Update() //Shows the pause menu when escape key is pressed
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ShowPauseMenu();           
        }
    }

   
}
