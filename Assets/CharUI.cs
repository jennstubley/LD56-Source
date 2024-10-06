using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharUI : MonoBehaviour
{
    public TMP_Text NameText;

    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Character c = GameController.Instance.ActiveCharacter;
        if (c != null)
        {
            image.sprite = c.Portrait;
            NameText.text = c.Name;
        }
    }
}
