using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    private Image image;
    public Sprite EmptyImage;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.ActiveCharacter != null)
        {
            Character character = GameController.Instance.ActiveCharacter;
            Inventory inv = character.GetComponent<Inventory>();
            if (inv != null)
            {
                image.sprite = inv.CurrentItem == null ? EmptyImage : inv.CurrentItem.Image;
            }
        }
    }
}
