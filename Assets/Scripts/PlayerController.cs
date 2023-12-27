using UnityEngine;

// Unity UI 및 Input System을 사용하는 데 필요한 네임스페이스를 포함합니다.
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{

	// 플레이어 속도 및 텍스트 UI 게임 오브젝트에 대한 public 변수를 만듭니다.
	public float speed;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

	private float movementX;
	private float movementY;

	private Rigidbody rb;
	private int count;

	// 게임을 시작할 때
	void Start()
	{
		// 리지드바디 컴포넌트를 private rb 변수에 할당합니다.
		rb = GetComponent<Rigidbody>();

		// count를 0으로 설정합니다. 
		count = 0;

		SetCountText();

		// Win Text UI의 텍스트 프로퍼티를 빈 문자열로 설정하여 'You Win'(게임 오버 메시지)을 공백으로 만듭니다.
		winTextObject.SetActive(false);
	}

	void FixedUpdate()
	{
		// Vector3 변수를 만들고 X와 Z를 할당하여 위의 horizontal 및 vertical 플로트 변수를 구현합니다.
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);

		rb.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		// 교차하는 게임 오브젝트에 'Pick Up' 태그가 할당되어 있는 경우
		if (other.gameObject.CompareTag("PickUp"))
		{
			other.gameObject.SetActive(false);

			// 점수 변수 'count'에 1을 추가합니다.
			count = count + 1;

			// 'SetCountText()' 함수를 실행합니다(아래 참조).
			SetCountText();
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>();

		movementX = v.x;
		movementY = v.y;
	}

	void SetCountText()
	{
		countText.text = "Count: " + count.ToString();

		if (count >= 12)
		{
			// 'winText'의 텍스트 값을 설정합니다.
			winTextObject.SetActive(true);
		}
	}
}