using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //탑뷰 형식의 캐릭터를 좌우 움직임을 구현화 하는 코드
    public float xRange = 10f; // x좌표의 경계값을 설정하는 변수
    public float speed = 10.0f; // 플레이어의 속도를 받는 변수
    public float horizontalInput; // 좌우로 입력값을 받는 변수
    void Start()
    {

    }

    void Update()
    {   // 만약 플레이어가 오른쪽 경계로 간다면 멈춤
        if (transform.position.x > xRange) {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        // 만약 플레어이가 왼쪽 경계로 간다면 멈춤
        if (transform.position.x < -xRange) {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
    }
}
