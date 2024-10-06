using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilitiesUI : MonoBehaviour
{
    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            if (child.gameObject == GameController.Instance.ActiveCharacter.AbilitiesUI)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
