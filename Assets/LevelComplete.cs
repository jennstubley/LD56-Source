using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Collision?");
        if (collider != null && collider.GetComponent<Rigidbody2D>() != null)
        {
            GameController.Instance.CompleteLevel();
        }
    }
}
