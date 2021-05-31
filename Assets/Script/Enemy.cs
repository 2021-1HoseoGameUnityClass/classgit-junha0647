using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject rayPos = null;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private int HP = 3;

    private bool moveRight = true;
    private bool isDead = false;

    // Update is called once per frame
    void Update()
    {
        CheckRay();
    }

    private void CheckRay()
    {
        // ���� �ʾҴٸ�
        if(isDead == false)
        {
            // ���̾� ����ũ
            LayerMask layerMask = new LayerMask();
            layerMask = LayerMask.GetMask("Platform");

            // ����ĳ��Ʈ
            RaycastHit2D ray = Physics2D.Raycast(rayPos.transform.position, new Vector2(0, -1), 1.1f, layerMask.value);
            Debug.DrawRay(rayPos.transform.position, new Vector3(0, -1, 0), Color.red);

            // ������ ��Ʈ ���� ������
            if(ray == false)
            {
                if (moveRight) moveRight = false;
                else moveRight = true;
            }
        }

        // �̵�
        Move();
    }

    private void Move()
    {
        float direction = 0f;
        if (moveRight == true) direction = 1;
        else direction = -1;

        // ���⿡ �°� ��������Ʈ�� ����
        Vector3 vector3 = new Vector3(direction, 1, 1);
        transform.localScale = vector3;

        // ���ǵ� ���� �̵�
        float speed = moveSpeed * Time.deltaTime * direction;
        vector3 = new Vector3(speed, 0, 0);
        transform.Translate(vector3);
        //// �̵� �ִϸ��̼�
        //GetComponent<Animator>().SetBool("Walk", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �Ѿ˿� ���� ���
        if(collision.CompareTag("Bullet") == true)
        {
            // ������ -1
            HP -= 1;

            // �������� 0�� �Ǿ��ٸ�
            if(HP < 1)
            {
                // ���� �ִϸ��̼��� true��
                GetComponent<Animator>().SetBool("Death", true);

                // �߰� - ������ ���ڸ����� �״� ȿ���� ���;��ϴµ�
                // �����̸鼭 �״� ȿ���� �߻��Ͽ� �߰���
                moveSpeed = 0;
                // �Ǵ� �ִϸ����Ϳ� Walk�� ���� Bool�� ���� false ������

                // ������ true��
                isDead = true;

                // 1�� �� ���ӿ�����Ʈ�� �����Ѵ�.
                Destroy(this.gameObject, 1);
            }
        }
    }
}
