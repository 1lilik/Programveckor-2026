using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    //Healthbar
    public Slider slider;
    public Gradient gradient;
    public Image fill;


    //Ammo counter
    public int maxAmmo;
    public int currentAmmo;
    public TextMeshProUGUI ammoText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = maxAmmo;
        ammoText.text = "Ammo: " + currentAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0)
        {
            currentAmmo--;
            ammoText.text = "Ammo: " + currentAmmo;

            Debug.Log(currentAmmo);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo <= 0)
        {
            currentAmmo = maxAmmo + 1;
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


}
