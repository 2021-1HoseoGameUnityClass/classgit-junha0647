//라이브러리
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//클래스
public class Player : MonoBehaviour
{   // 움직임 관련 변수
    // 유지보수 및 보안에 의해 public은 위험해 !!
    // → private로 바꾸고 해당 변수 위에 [SerializeField] 설정
    // 변수 선언
    //public float moveSpeed = 3f;
    [SerializeField] private float moveSpeed = 3f;

    // 점프 관련 변수
    [SerializeField] private float jumpForce = 300f;
    private bool isJump = false;

    // 총알 발사 관련 변수
    [SerializeField] private GameObject bulletPos = null;
    [SerializeField] private GameObject bulletObj = null;

    // Update is called once per frame
    // 프레임마다 함수가 실행됨
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

    // 플레이어 움직임
    private void PlayerMove()
    {
        // 쌍따옴표 사용 시 string 문자열값 받아온다.
        float h = Input.GetAxis("Horizontal");
        float playerSpeed = h * moveSpeed * Time.deltaTime;
        Vector3 vector3 = new Vector3();
        vector3.x = playerSpeed;
        // 해당하는 오브젝트를 이동시킨다.
        transform.Translate(vector3);

        // Horizontal값이 음수라면
        if (h < 0)
        {
            // 움직이면 애니메이션값을 바꾼다.
            GetComponent<Animator>().SetBool("Walk", true);
            // 스케일 x값을 -1로 바꾼다.
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h == 0)
        {
            // 움직이지 않는다면 False로
            GetComponent<Animator>().SetBool("Walk", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    // 플레이어 점프
    private void PlayerJump()
    {
        // 점프 상태가 아닐 때만 점프하도록 함
        if(isJump == false)
        {
            // 애니메이션 처리부
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().SetBool("Jump", true);

            // 점프량만큼 Add Force
            Vector2 vector2 = new Vector2(0, jumpForce);
            GetComponent<Rigidbody2D>().AddForce(vector2);
            isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌체의 콜라이더가 플랫폼 태그라면
        if (collision.collider.tag == "Platform")
        {
            GetComponent<Animator>().SetBool("Jump", false);
            isJump = false;
        }
    }

    // 플레이어 총알 발사
    private void Fire()
    {
        GetComponent<AudioSource>().Play();
        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        Instantiate(bulletObj, bulletPos.transform.position, quaternion).GetComponent<Bullet>().InstantiateBullet(direction);
    }
}