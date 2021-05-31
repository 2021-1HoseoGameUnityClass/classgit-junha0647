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

    // Update is called once per frame
    // �����Ӹ��� �Լ��� �����
    void Update() // == private void Update()
    {
        PlayerMove();

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
        float h = Input.GetAxis("Horizontal");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹ü�� �ݶ��̴��� �÷��� �±׶��
        if (collision.collider.tag == "Platform")
        {
            GetComponent<Animator>().SetBool("Jump", false);
            isJump = false;
        }
    }

    // �÷��̾� �Ѿ� �߻�
    private void Fire()
    {
        GetComponent<AudioSource>().Play();
        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        Instantiate(bulletObj, bulletPos.transform.position, quaternion).GetComponent<Bullet>().InstantiateBullet(direction);
    }
}