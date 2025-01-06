using UnityEngine;

public class BallController : MonoBehaviour
{
    public float gravity = -9.8f * 10f; // 중력 가속도
    public float initialVelocity = 10f; // 초기 속도 (위로 튀길 때의 속도)
    public float floorY = -4f; // 바닥의 Y 위치 (Floor의 위치에 맞게 설정)
    private float initialY = 0.0f; // 초기 Y 위치

    private float velocity = 0.0f; // 현재 속도
    private bool isFalling = true; // 공이 떨어지고 있는지 여부

    void Start()
    {
        // 초기 속도 설정
        velocity = initialVelocity;
        //initialY = transform.position.y + (initialVelocity * initialVelocity) / (-2 * gravity);
        initialY = 0.0f;
    }

    void Update()
    {
        // 중력 적용
        velocity += gravity * Time.deltaTime;

        // 공 이동
        transform.position += new Vector3(0, velocity * Time.deltaTime, 0);

        // 바닥 충돌 처리
        if (transform.position.y <= floorY && isFalling)
        {
            transform.position = new Vector3(transform.position.x, floorY, transform.position.z);

            // 튕길 때 속도 재설정
            velocity = Mathf.Sqrt(-2 * gravity * (initialY - floorY));
            isFalling = false;
        }

        // 정점에 도달 후 다시 떨어짐
        if (velocity < 0 && !isFalling)
        {
            isFalling = true;
        }
    }
}