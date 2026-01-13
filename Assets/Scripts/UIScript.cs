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

    //Hotbar
    public GameObject[] slots;
    public Sprite InactiveSlot;
    public Sprite ActiveSlot;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAmmo = maxAmmo;
        ammoText.text = "Ammo: " + currentAmmo;


        //Setting sprites for the hotbarslots when starting the game
        foreach (GameObject slot in slots)
        {
            slot.GetComponent<UnityEngine.UI.Image>().sprite = InactiveSlot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo > 0)
        {
            currentAmmo--;
            ammoText.text = "Ammo: " + currentAmmo;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && currentAmmo <= 0)
        {
            currentAmmo = maxAmmo + 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            slots[0].GetComponent<UnityEngine.UI.Image>().sprite = ActiveSlot;
            slots[1].GetComponent<UnityEngine.UI.Image>().sprite = InactiveSlot;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slots[1].GetComponent<UnityEngine.UI.Image>().sprite = ActiveSlot;
            slots[0].GetComponent<UnityEngine.UI.Image>().sprite = InactiveSlot;
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
