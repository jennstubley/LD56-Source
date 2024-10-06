using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractType
    {
        Jump,
        Pickup,
        Hit,
        Interact,
        GiveItem,
        Drop,
    }

    public InteractType Type;
    public Material OutlineMaterial;
    public Material RegularMaterial;
    public bool Selected;
    public int Level;
    public GameObject ChangeDupe; // Only for Hit and Interact
    public Item PickupItem; // Only for Pickup
    public Item RequiredItem; // Only for Interact
    public Interactable LinkedObject; // When one is interacted with successfully the other one will be as well.
    public Vector3 JumpOffset; // Only for Jump
    public SpriteRenderer ChangeSpriteTarget;
    public Sprite SpriteChange;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        if (sr != null)
        {
            sr.material = OutlineMaterial;
        }
        Selected = true;
    }

    public void Deselect()
    {
        if (sr != null)
        {
            sr.material = RegularMaterial;
        }
        Selected = false;
    }

    public void Interact(Character character)
    {
        switch (Type)
        {
            case InteractType.Jump:
                character.transform.position = transform.position + JumpOffset;
                character.Level = Level;
                character.GetComponentInChildren<SpriteRenderer>().sortingOrder = Level;
                GameController.Instance.ColliderSpaceChanged();
                AudioController.Instance.PlayJump();
                break;
            case InteractType.Pickup:
                if (PickupItem != null)
                {
                    character.GetComponent<Inventory>().AddItem(PickupItem);
                    gameObject.SetActive(false);
                    AudioController.Instance.PlayPickUp();
                }
                break;
            case InteractType.Hit:
                if (ChangeDupe != null)
                {
                    ChangeDupe.SetActive(true);
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                    if (ChangeSpriteTarget != null)
                    {
                        ChangeSpriteTarget.sprite = SpriteChange;
                    }
                    AudioController.Instance.PlayHit();
                }
                if (LinkedObject != null)
                {
                    LinkedObject.Interact(character);
                }
                break;
            case InteractType.GiveItem:
                Inventory receiver = GetComponent<Inventory>();
                Inventory giver = character.GetComponent<Inventory>();
                receiver.AddItem(giver.CurrentItem);
                giver.RemoveItem();
                break;
            case InteractType.Interact:
                if (RequiredItem == null || character.GetComponent<Inventory>().CurrentItem == RequiredItem)
                {
                    AudioController.Instance.PlayDoor();
                    if (ChangeDupe != null)
                    {
                        ChangeDupe.SetActive(true);
                        gameObject.SetActive(false);
                    }
                    if (LinkedObject != null)
                    {
                        LinkedObject.Interact(character);
                    }
                }
                break;
            default:
                break;
        }
    } 
}
