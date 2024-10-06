using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public Interactable ClosestInteractable { get; private set; }


    [SerializeField]
    private List<Character> characters;
    [SerializeField]
    private List<Level> levels;

    public Character ActiveCharacter { get { return characters[activeCharacterIdx]; } }
    private int activeCharacterIdx;
    private int currentLevel;
    public List<ColliderController> colliders = new List<ColliderController>();
    public GameObject LevelTransitionUI;
    public bool GameOver;
    public GameObject WinScreen;

    private void Awake()
    {
        colliders = FindObjectsOfType<ColliderController>().ToList();
    }


    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        activeCharacterIdx = 1;
        currentLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver) return;
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ActiveCharacter.GetComponent<Rigidbody2D>().simulated = false;
            activeCharacterIdx++;
            if (activeCharacterIdx >= characters.Count)
            {
                activeCharacterIdx = 0;
            }
            ColliderSpaceChanged();
            ActiveCharacter.GetComponent<Rigidbody2D>().simulated = true;
        }

        UpdateClosestInteractable();

    }

    public bool IsActiveCharacter(Character character)
    {
        return ActiveCharacter == character;
    }

    public void UpdateClosestInteractable()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(ActiveCharacter.transform.position, 0.5f, ActiveCharacter.FacingDir, 0.5f);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != ActiveCharacter.gameObject)
            {
                Interactable obj = hit.collider.gameObject.GetComponentInParent<Interactable>();
                if (obj != null && CanActiveCharacterInteract(obj))
                {
                    if (obj != ClosestInteractable)
                    {
                        if (ClosestInteractable != null)
                        {
                            ClosestInteractable.Deselect();
                        }

                        obj.Select();
                        ClosestInteractable = obj;
                    }
                    return;
                }
            }
        }

        // Didn't find anything so deselect the current one.
        if (ClosestInteractable != null)
        {
            ClosestInteractable.Deselect();
            ClosestInteractable = null;
        }

    }

    private bool CanActiveCharacterInteract(Interactable obj)
    {
        foreach (Interact interact in ActiveCharacter.GetComponents<Interact>())
        {
            if (interact.CanInteract(obj)) return true;
        }
        return false;
    }

    public void ColliderSpaceChanged()
    {
        bool isShrunk = ActiveCharacter.GetComponent<Shrink>() == null ? false : ActiveCharacter.GetComponent<Shrink>().Shrunk;
        int level = ActiveCharacter.Level;
        foreach (ColliderController col in colliders)
        {
            col.GetComponent<Collider2D>().enabled = level == col.Level && (col.appliesToShrunk ? true : !isShrunk);
        }
    }

    public void CompleteLevel()
    {
        LevelTransitionUI.SetActive(true);
        AudioController.Instance.PlayFinalDoor();
    }

    public void ContinueCompleteLevel()
    {
        if (currentLevel == levels.Count - 1)
        {
            GameOver = true;
            WinScreen.SetActive(true);
            return;
        }

        LevelTransitionUI.SetActive(false);
        levels[currentLevel].gameObject.SetActive(false);
        currentLevel++;
        levels[currentLevel].gameObject.SetActive(true);
        colliders.Clear();
        colliders = FindObjectsOfType<ColliderController>().ToList();
        foreach (Character c in characters)
        {
            if (c.name == "Cat")
            {
                c.transform.position = levels[currentLevel].CatStart;
            }
            else if (c.name == "Dog")
            {
                c.transform.position = levels[currentLevel].DogStart;
            }
            else
            {
                c.transform.position = levels[currentLevel].ManStart;
            }

            c.Reset();
            if (c.TryGetComponent(out Shrink shrink))
            {

                shrink.Reset();
            }
            if (c.TryGetComponent(out Inventory inv))
            {

                inv.Reset();
            }
        }

        ActiveCharacter.GetComponent<Rigidbody2D>().simulated = false;
        activeCharacterIdx = 0;
        ColliderSpaceChanged();
        ActiveCharacter.GetComponent<Rigidbody2D>().simulated = true;
        UpdateClosestInteractable();

    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
