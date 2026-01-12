using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Hotbar : MonoBehaviour
{

    public GameObject[] slots;
    public Sprite InactiveSlot;
    public Sprite ActiveSlot;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (GameObject slot in slots)
        {
            slot.GetComponent<UnityEngine.UI.Image>().sprite = InactiveSlot;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
    }
}
