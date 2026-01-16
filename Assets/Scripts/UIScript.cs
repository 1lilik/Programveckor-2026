using NUnit.Framework;
using System.Collections.Generic;
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
    public GameObject ammoTextActivate;

    //Weapon
    public GameObject weaponUI;
    public GameObject weaponImage;

    //Player Icon
    public GameObject playerIcon;
    public GameObject playerIconImage;
    public Animator playerAnimator;
    public Animation playerAnimation;

    //Objective
    public string[] objectives;
    public TextMeshProUGUI objectivesText;
    public List<DestroyableBox> boxes = new List<DestroyableBox>();
    bool canShowMessage = true;
  


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weaponUI.SetActive(false);
        ammoTextActivate.SetActive(false);
        weaponImage.SetActive(false);
        objectivesText.text = "Objective: " + objectives[0];

        
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.Play("Player Icon");

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
        ammoTextActivate.SetActive(true);
        weaponUI.SetActive(true);
        weaponImage.SetActive(true);
    }

    public void BoxDestroyed (DestroyableBox box)
    {
        boxes.Remove(box);

        if (boxes.Count == 0 & canShowMessage == true)
        {
            objectivesText.text = "Objective: " + objectives[0];
            canShowMessage = false;
        }
    }
  
}
