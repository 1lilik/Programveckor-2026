using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
public class languageManager : MonoBehaviour
{
   public enum Language { English, Swedish}
    public Language currentLanguage = Language.English;

    public TMP_Text titleText;
    public TMP_Text startButtonText;
    public TMP_Text settingsText;
    public TMP_Text controlsText;
    public TMP_Text quitText;
    public TMP_Text audioSettingsText;
    public TMP_Text controlSettingsText;
    public TMP_Text exitToMenuText;
    public TMP_Text continueText;


    private Dictionary<string, string> english = new Dictionary<string, string>();
    private Dictionary<string, string> swedish = new Dictionary<string, string>();

    private void Awake()
    {
        //Fill English
        english["title"] = "Main Menu";
        english["start"] = "Start";
        english["settings"] = "settings";
        english["controls"] = "controls";
        english["quit"] = "quit";
        english["audio settings"] = "audio settings";
        english["control settings"] = "control settings";
        english["exit to menu"] = "Exit to menu";
        english["resume"] = "resume";

        //Fill Swedish
        swedish["title"] = "Huvudmeny";
        swedish["start"] = "Start";
        swedish["settings"] = "Inställningar";
        swedish["controls"] = "Kontroller";
        swedish["quit"] = "Avsluta";
        swedish["audio settings"] = "ljud-inställningar";
        swedish["control settings"] = "kontrol-inställningar";
        swedish["exit to menu"] = "avlsuta till huvudmenyn";
        swedish["resume"] = "fortsätt";


        int savedLanguage = PlayerPrefs.GetInt("Language", 0); //0 = English
        currentLanguage = (Language)savedLanguage;

        ApplyLanguage();
    }

    public void SetLanguage(int langIndex)
    {
        currentLanguage = (Language)langIndex;
        PlayerPrefs.SetInt("Language", langIndex);
        ApplyLanguage();
    }

    public void ApplyLanguage()
    {
        Dictionary<string, string> dict = (currentLanguage == Language.English) ? english : swedish;
        titleText.text = dict["titel"];
        startButtonText.text = dict["start"];
        settingsText.text = dict["settings"];
        controlsText.text = dict["controls"];
        quitText.text = dict["quit"];
        audioSettingsText.text = dict["audio settings"];
        controlSettingsText.text = dict["control settings"];
        exitToMenuText.text = dict["exit to menu"];
        continueText.text = dict["resume"];
    }
}
