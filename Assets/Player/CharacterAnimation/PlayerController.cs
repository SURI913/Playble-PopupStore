using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using static Unity.Burst.Intrinsics.X86.Avx;

public class PlayerController : MonoBehaviour, IPunObservable
{
    Rigidbody2D rigid;
    [SerializeField] float speed = 5f;
    Vector2 movement = new Vector2();
    Animator animator;
    PhotonView PV;
    ChatManager cm;

    Vector2 networkPosition;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();

        if (!PV.IsMine)
        {
            networkPosition = new Vector2(transform.position.x, transform.position.y);
        }
    }

    void Update()
    {
        if (PV.IsMine)
        {
            UpdateState();
            //print("이건 내 거");

            cm.SendChatMessage("hi");
        }
        else
        {
            // 네트워크 플레이어의 위치를 보간합니다.
            rigid.position = Vector2.MoveTowards(rigid.position, networkPosition, speed * Time.deltaTime);

            print("이건 너 거야.");
        }
    }

    void MoveCharacter()
    {
        if (!PV.IsMine) return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        float tempSpeed = speed;
        if (Input.GetKey(KeyCode.LeftShift))
            tempSpeed = speed * 1.9f;

        rigid.velocity = movement * tempSpeed;
    }

    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            MoveCharacter();
        }
    }

    void UpdateState()
    {
        if (!PV.IsMine) return;

        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            animator.SetBool("isMove", true);
        }

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            animator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }

        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // 로컬 플레이어의 데이터를 네트워크에 보냅니다.
            stream.SendNext(rigid.position);
            stream.SendNext(movement);
        }
        else
        {
            // 네트워크 플레이어의 데이터를 받습니다.
            networkPosition = (Vector2)stream.ReceiveNext();
            movement = (Vector2)stream.ReceiveNext();
            print("데이터를 읽었어요 ");
        }
    }
}
