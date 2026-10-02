using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥(Floor)이나 벽에 부딪히면 스스로 파괴
        if (collision.gameObject.name == "Floor" || collision.gameObject.CompareTag("Finish"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 만약 충돌 처리가 안 되어 Y 위치가 -6 이하로 더 떨어지면 안전하게 메모리 삭제
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}