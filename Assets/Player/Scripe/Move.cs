using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Unity.Netcode;

public class Move : MonoBehaviour
{
    private Rigidbody2D playerRb;
    //private Animator myAnim;
    public float playerMoveSpeed;
    private float maxMoveSpeed;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        //myAnim = GetComponent<Animator>();
        maxMoveSpeed = playerMoveSpeed;
        currentTransfrom = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        //if(!IsOwner) { return; }
        
        playerRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * playerMoveSpeed * Time.deltaTime;

        /*myAnim.SetFloat("MoveX", playerRb.velocity.x);
        myAnim.SetFloat("MoveY", playerRb.velocity.y);*/
    }
    Transform currentTransfrom;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {

    }
}
