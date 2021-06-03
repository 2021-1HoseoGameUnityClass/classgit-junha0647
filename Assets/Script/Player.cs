//���̺귯��
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ŭ����
public class Player : MonoBehaviour
{   // ������ ���� ����
    // �������� �� ���ȿ� ���� public�� ������ !!
    // �� private�� �ٲٰ� �ش� ���� ���� [SerializeField] ����
    // ���� ����
    //public float moveSpeed = 3f;
    [SerializeField] private float moveSpeed = 3f;

    // ���� ���� ����
    [SerializeField] private float jumpForce = 300f;
    private bool isJump = false;

    // �Ѿ� �߻� ���� ����
    [SerializeField] private GameObject bulletPos = null;
    [SerializeField] private GameObject bulletObj = null;

    private bool move = false;
    private float moveHorizontal = 0;

    // Update is called once per frame
    // �����Ӹ��� �Լ��� �����
    void Update() // == private void Update()
    {
        if(move == true)
        {
            PlayerMove();
        }
        if(Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }
        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    // �÷��̾� ������
    private void PlayerMove()
    {
        // �ֵ���ǥ ��� �� string ���ڿ��� �޾ƿ´�.
        //float h = Input.GetAxis("Horizontal");
        float h = moveHorizontal;
        float playerSpeed = h * moveSpeed * Time.deltaTime;
        Vector3 vector3 = new Vector3();
        vector3.x = playerSpeed;
        // �ش��ϴ� ������Ʈ�� �̵���Ų��.
        transform.Translate(vector3);

        // Horizontal���� �������
        if (h < 0)
        {
            // �����̸� �ִϸ��̼ǰ��� �ٲ۴�.
            GetComponent<Animator>().SetBool("Walk", true);
            // ������ x���� -1�� �ٲ۴�.
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h == 0)
        {
            // �������� �ʴ´ٸ� False��
            GetComponent<Animator>().SetBool("Walk", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    // �÷��̾� ����
    private void PlayerJump()
    {
        // ���� ���°� �ƴ� ���� �����ϵ��� ��
        if(isJump == false)
        {
            // �ִϸ��̼� ó����
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().SetBool("Jump", true);

            // ��������ŭ Add Force
            Vector2 vector2 = new Vector2(0, jumpForce);
            GetComponent<Rigidbody2D>().AddForce(vector2);
            isJump = true;
        }
    }
    // �÷��̾� �Ѿ� �߻�
    private void Fire()
    {
        AudioClip audioClip = Resources.Load<AudioClip>("RangedAttack") as AudioClip;
        GetComponent<AudioSource>().clip = audioClip;
        GetComponent<AudioSource>().Play();

        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        Instantiate(bulletObj, bulletPos.transform.position, quaternion).GetComponent<Bullet>().InstantiateBullet(direction);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹ü�� �ݶ��̴��� �÷��� �±׶��
        if (collision.collider.tag == "Platform")
        {
            GetComponent<Animator>().SetBool("Jump", false);
            isJump = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // �浹ü�� �ݶ��̴��� �÷��� �±׶��
        if (collision.collider.tag == "Enemy")
        {
            DataManager.instance.playerHP -= 1;
            if (DataManager.instance.playerHP < 0)
            {
                DataManager.instance.playerHP = 0;
            }
            UIManager.instance.PlayerHP();
        }
    }



    public void OnMove(bool _right)
    {
        if(_right)
        {
            moveHorizontal = 1;
        }
        else
        {
            moveHorizontal = -1;
        }
        move = true;
    }
    public void OffMove()
    {
        moveHorizontal = 0;
        move = false;
    }
}