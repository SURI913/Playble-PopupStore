using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField] float speed =5f;
    Vector2 movement = new Vector2();
    Animator animator;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateState();
    }
    void MoveCharacter(){
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        float tempSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
            tempSpeed = speed * 1.9f;

        rigid.velocity = movement * tempSpeed;

        //rigid.velocity = movement * speed;
    }
    void FixedUpdate()
    {  
        MoveCharacter();
    }
    void UpdateState()
    {
        if(Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isMove", false);
            
        }
        else
        {
            animator.SetBool("isMove", true);
            
        }
        if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
    }
}