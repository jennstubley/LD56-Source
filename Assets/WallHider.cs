using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHider : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    private HashSet<Rigidbody2D> objectsUnder = new HashSet<Rigidbody2D>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.GetComponent<Rigidbody2D>() != null) {
            objectsUnder.Add(collision.GetComponent<Rigidbody2D>());
            SpriteRenderer.color = new Color(1,1, 1, .4f);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb) && rb.simulated)
        {
            objectsUnder.Remove(rb);
            if (objectsUnder.Count == 0)
            {
                SpriteRenderer.color = new Color(1, 1, 1, 1f);
            }
        }
    }
}
