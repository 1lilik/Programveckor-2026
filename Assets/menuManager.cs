using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class menuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;
    public GameObject pauseMenu;

    public void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu() //Shows main menu
    {
        mainMenu.SetActive(true);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void ShowControlsMenu() //Show controls menu
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void ShowSettingsMenu() //Show settings menu
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void ShowPauseMenu() //Shows pause menu
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(false);
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); //Loads game (change name later)
    }

    public void QuitGame() //Quits game
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ShowPauseMenu();
        }
    }
}
