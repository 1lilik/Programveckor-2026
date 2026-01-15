using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    //Healthbar
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public PlayerStats playerStatsScript;

    //Ammo counter
    public int currentAmmo;
    public TextMeshProUGUI ammoText;
    public Gun gunScript;
    public GameObject ammoTextFix;

    //Weapon
    public GameObject weaponUI;
    public GameObject weaponImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponUI.SetActive(false);
        ammoTextFix.SetActive(false);
        weaponImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.R))
        {
            ammoText.text = "Ammo: " + gunScript.ammo;
        }

    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }


    public void SetHealth(int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void ActivateWeapon()
    {
        ammoTextFix.SetActive(true);
        weaponUI.SetActive(true);
        weaponImage.SetActive(true);
    }

}
