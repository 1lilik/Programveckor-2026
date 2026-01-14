using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class languageManager : MonoBehaviour
{
   public enum Language { English, Swedish}
    public Language currentLanguage = Language.English;

    public Text titleText;
    public Text startButtonText;
    public Text settingsText;
    public Text controlsText;
    public Text quitText;


    private Dictionary<string, string> english = new Dictionary<string, string>();
    private Dictionary<string, string> swedish = new Dictionary<string, string>();

    private void Awake()
    {
        //Fill English
        english["title"] = "Main Menu";
        english["start"] = "Start";
        english["settings"] = "settings";

        //Fill Swedish
        swedish["title"] = "Huvudmeny";
        swedish["start"] = "Start";
        swedish["settings"] = "Inställningar";


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
    }
}
