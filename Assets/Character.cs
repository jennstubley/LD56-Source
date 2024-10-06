using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float MoveSpeed = 2.0f;
    public Vector2 FacingDir = new Vector2(0, 1);
    public int Level;
    public string Name;
    public Sprite Portrait;

    private float deltaX;
    private float deltaY;
    private Rigidbody2D rigidBody;
    private Animator anim;

    public GameObject AbilitiesUI;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.GameOver) return;

        if (!GameController.Instance.IsActiveCharacter(this)) return;

        deltaX = Input.GetAxisRaw("Horizontal"); // -1 is left.
        deltaY = Input.GetAxisRaw("Vertical"); // -1 is down.
        /*if (Input.GetKey(KeyCode.UpArrow))
        {
            deltaY = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            deltaY = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            deltaX = -1;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            deltaX = 1;
        }*/

    }

    void FixedUpdate()
    {
        if (GameController.Instance.GameOver) return;
        if (!GameController.Instance.IsActiveCharacter(this))
        {
            deltaX = 0;
            deltaY = 0;
        }

        rigidBody.velocity = new Vector2(deltaX * MoveSpeed, deltaY * MoveSpeed);
        //if (deltaX != 0 || deltaY != 0)
        //{
            //ValidateAndSetPosition(new Vector3(transform.position.x + deltaX * Time.deltaTime * MoveSpeed, transform.position.y + deltaY * Time.deltaTime * MoveSpeed, transform.position.z));
            SetFacingDir(deltaX, deltaY);
        //}

        if (anim != null)
        {
            anim.SetBool("IsMoving", deltaX != 0 || deltaY != 0);
        }
    }

    private void ValidateAndSetPosition(Vector3 newPos)
    {
        if (Level > 0) return;
        transform.position = newPos;
    }

    private void SetFacingDir(float x, float y)
    {
        if (x != 0)
        {
            FacingDir = new Vector2Int(x > 0 ? 1 : -1, 0);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * FacingDir.x, transform.localScale.y, transform.localScale.z);
            //playerSprite.sprite = x > 0 ? Sprites[0] : Sprites[1];
        }
        else if (y != 0)
        {
            FacingDir = new Vector2Int(0, y > 0 ? 1 : -1);
            //playerSprite.sprite = y > 0 ? Sprites[2] : Sprites[3];
        }

    }

    public void Reset()
    {
        Level = 0;
    }
}
