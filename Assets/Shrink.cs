using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : MonoBehaviour
{
    private Character character;
    public bool Shrunk = false;

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

        if (Input.GetKeyDown(KeyCode.X))
        {
            Shrunk = !Shrunk;
            GameController.Instance.ColliderSpaceChanged();
            if (Shrunk)
            {
                transform.localScale = new Vector3(0.5f * (character.FacingDir.x != 0 ? character.FacingDir.x : 1), 0.5f, 1f);
                AudioController.Instance.PlayShrink();
            }
            else
            {
                transform.localScale = new Vector3((character.FacingDir.x != 0 ? character.FacingDir.x : 1), 1, 1);
                ContactFilter2D filter = new ContactFilter2D().NoFilter();
                filter.useTriggers = false;
                List<Collider2D> results = new List<Collider2D>();
                GetComponent<Collider2D>().OverlapCollider(filter, results);
                foreach (Collider2D c in results)
                {
                    if (c.GetComponent<ColliderController>() != null && !c.GetComponent<ColliderController>().appliesToShrunk)
                    {
                        // Stay shrunk
                        transform.localScale = new Vector3(0.5f, 0.5f, 1f);
                        Shrunk = true;
                        GameController.Instance.ColliderSpaceChanged();
                        return;
                    }
                }

                AudioController.Instance.PlayUnshrink();
            }
        }
    }

    public void Reset()
    {
        Shrunk = false;
        transform.localScale = new Vector3((character.FacingDir.x != 0 ? character.FacingDir.x : 1), 1, 1);
    }
}
