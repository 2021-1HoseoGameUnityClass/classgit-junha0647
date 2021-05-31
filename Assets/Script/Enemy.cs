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
        // 죽지 않았다면
        if(isDead == false)
        {
            // 레이어 마스크
            LayerMask layerMask = new LayerMask();
            layerMask = LayerMask.GetMask("Platform");

            // 레이캐스트
            RaycastHit2D ray = Physics2D.Raycast(rayPos.transform.position, new Vector2(0, -1), 1.1f, layerMask.value);
            Debug.DrawRay(rayPos.transform.position, new Vector3(0, -1, 0), Color.red);

            // 광선에 히트 되지 않으면
            if(ray == false)
            {
                if (moveRight) moveRight = false;
                else moveRight = true;
            }
        }

        // 이동
        Move();
    }

    private void Move()
    {
        float direction = 0f;
        if (moveRight == true) direction = 1;
        else direction = -1;

        // 방향에 맞게 스프라이트를 수정
        Vector3 vector3 = new Vector3(direction, 1, 1);
        transform.localScale = vector3;

        // 스피드 계산과 이동
        float speed = moveSpeed * Time.deltaTime * direction;
        vector3 = new Vector3(speed, 0, 0);
        transform.Translate(vector3);
        //// 이동 애니메이션
        //GetComponent<Animator>().SetBool("Walk", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 총알에 맞을 경우
        if(collision.CompareTag("Bullet") == true)
        {
            // 라이프 -1
            HP -= 1;

            // 라이프가 0이 되었다면
            if(HP < 1)
            {
                // 죽음 애니메이션을 true로
                GetComponent<Animator>().SetBool("Death", true);

                // 추가 - 죽으면 제자리에서 죽는 효과가 나와야하는데
                // 움직이면서 죽는 효과가 발생하여 추가함
                moveSpeed = 0;
                // 또는 애니메이터에 Walk에 대한 Bool값 만들어서 false 만들어보기

                // 죽음을 true로
                isDead = true;

                // 1초 후 게임오브젝트를 삭제한다.
                Destroy(this.gameObject, 1);
            }
        }
    }
}
