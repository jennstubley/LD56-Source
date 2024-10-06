using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public KeyCode Key;
    public Interactable.InteractType InteractType;

    private Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.GameOver) return;
        if (!GameController.Instance.IsActiveCharacter(character)) return;

        if (Input.GetKeyDown(Key)) {
            Interactable closestInteractable = GameController.Instance.ClosestInteractable;
            if (closestInteractable != null && closestInteractable.Type == InteractType)
            {
                closestInteractable.Interact(character);
                return;
            }

            if (InteractType == Interactable.InteractType.Drop)
            {
                if (CanInteract(null))
                {
                    Item item = GetComponent<Inventory>().CurrentItem;
                    item.DropObject.SetActive(true);
                    item.DropObject.transform.position = transform.position;
                    GetComponent<Inventory>().RemoveItem();

                    AudioController.Instance.PlayDrop();
                }
            }
        }
    }


    public bool CanInteract(Interactable obj)
    {
        if (obj != null && InteractType != obj.Type) return false;

        switch (InteractType)
        {
            case Interactable.InteractType.Jump:
                return Mathf.Abs(character.Level - obj.Level) == 1 && (character.GetComponent<Shrink>() == null || !character.GetComponent<Shrink>().Shrunk);
            case Interactable.InteractType.Hit:
                return character.Level == obj.Level && (character.GetComponent<Shrink>() == null || !character.GetComponent<Shrink>().Shrunk);
            case Interactable.InteractType.Interact:
                return character.Level == obj.Level && (character.GetComponent<Shrink>() == null || !character.GetComponent<Shrink>().Shrunk) && (obj.RequiredItem == null || (character.GetComponent<Inventory>() != null && character.GetComponent<Inventory>().CurrentItem == obj.RequiredItem));
            case Interactable.InteractType.Pickup:
                return character.Level == obj.Level && GetComponent<Inventory>().CurrentItem == null && (character.GetComponent<Shrink>() == null || !character.GetComponent<Shrink>().Shrunk);
            case Interactable.InteractType.Drop:
                return GetComponent<Inventory>().CurrentItem != null && (character.GetComponent<Shrink>() == null || !character.GetComponent<Shrink>().Shrunk);
        }
        return false;
    }
}
