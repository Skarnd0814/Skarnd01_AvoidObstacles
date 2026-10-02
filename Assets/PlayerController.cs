using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // 공의 이동 속도
    private Rigidbody2D rb;
    private float moveInput;

    void Start()
    {
        // 공에 붙어있는 Rigidbody 2D 부품을 찾아옵니다.
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 키보드 좌/우 입력 받기 (A/D 키 또는 왼쪽/오른쪽 화살표)
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        // 물리적인 힘으로 공을 좌우로 움직입니다.
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }
}