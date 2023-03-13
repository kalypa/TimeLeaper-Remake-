using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float spd = 7f;
    public float jumpForce = 10f; // ���� ��
    public Transform groundCheck; // �� üũ�� ��ġ ����
    public float groundCheckRadius = 0.2f; // �� üũ �ݰ�
    public LayerMask whatIsGround;
    private bool isGrounded; // ���� ��Ҵ��� üũ
    private bool isOnGround = false; // ���� ��Ҵ��� üũ
    private Animator animator;
    private bool isJump = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        GroundCheck();
        Atk();
    }

    void Dead()
    {
        if(GameManager.Instance.playerHP <= 0)
        {
            animator.SetTrigger("isDead");
            //���ӿ��� UI ���
            
        }
    }

    void Move()
    {
        float input = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(input * spd * Time.deltaTime, 0f, 0f);
        if (input < 0)
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        else if(input > 0)
            this.transform.eulerAngles = new Vector3(0, 0, 0);

        if (input != 0 && !isJump)
        {
            animator.SetFloat("Move", 1);
        }
        else
        {
            animator.SetFloat("Move", 0);
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        { // ���� ��Ұ� ����Ű�� �����ٸ�
            isJump = true;
            isOnGround = false;
            Debug.Log(isGrounded);
            animator.SetBool("isJump", true);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce); // ����
            
        }
        if(isJump == true && isGrounded)
        {
            isJump = false;
            animator.SetBool("isJump", false);
        }
    }
    void Atk()
    {
        if(isJump && Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("isAtk");
            animator.SetBool("isJump", true);
        }
        else if(Input.GetMouseButtonDown(0) && !isJump)
        {
            animator.SetTrigger("isAtk");
        }
    }

}
