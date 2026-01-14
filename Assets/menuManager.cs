using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class menuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;
    public GameObject pauseMenu;
    public GameObject audioSettingsMenu;
    public GameObject controlSettingsMenu;
    public GameObject languageMenu;

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

    public void ShowLanguageMenu()
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
