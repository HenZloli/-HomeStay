using UnityEngine;

public class FingerprintDoor : Interactable
{
    [Header("Door Settings")]
    public Transform leftDoor;
    public Transform rightDoor;

    public Vector3 leftOpenOffset = new Vector3(-2f, 0, 0);
    public Vector3 rightOpenOffset = new Vector3(2f, 0, 0);

    public float speed = 2f;
    public float closeDelay = 5f;   // đóng sau 5 giây

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;

    private bool daXacMinh = false;
    private bool isOpen = false;
    private bool isClosing = false;
    private float timer = 0f;

    void Start()
    {
        leftClosedPos = leftDoor.position;
        rightClosedPos = rightDoor.position;
    }

    void Update()
    {
        // 1) Bấm E để xác minh vân tay
        if (Input.GetKeyDown(KeyCode.E))
        {
            daXacMinh = true;
            Debug.Log("✓ Vân tay hợp lệ!");
        }

        // 2) Khi cửa đang mở → trượt sang hai bên
        if (isOpen)
        {
            leftDoor.position = Vector3.MoveTowards(
                leftDoor.position,
                leftClosedPos + leftOpenOffset,
                speed * Time.deltaTime
            );

            rightDoor.position = Vector3.MoveTowards(
                rightDoor.position,
                rightClosedPos + rightOpenOffset,
                speed * Time.deltaTime
            );

            // Đếm thời gian chờ đóng
            timer += Time.deltaTime;
            if (timer >= closeDelay)
            {
                isOpen = false;
                isClosing = true;
            }
        }

        // 3) Cửa đang đóng
        if (isClosing)
        {
            leftDoor.position = Vector3.MoveTowards(
                leftDoor.position,
                leftClosedPos,
                speed * Time.deltaTime
            );

            rightDoor.position = Vector3.MoveTowards(
                rightDoor.position,
                rightClosedPos,
                speed * Time.deltaTime
            );

            // Kiểm tra đã đóng xong chưa
            if (Vector3.Distance(leftDoor.position, leftClosedPos) < 0.01f &&
                Vector3.Distance(rightDoor.position, rightClosedPos) < 0.01f)
            {
                isClosing = false;
                timer = 0;
                daXacMinh = false; // bắt quét lại vân tay nếu muốn mở lần nữa
                Debug.Log("→ Cửa đã đóng.");
            }
        }
    }

    // Raycast của bạn sẽ gọi hàm này
    public override void OnInteract()
    {
        if (!daXacMinh)
        {
            Debug.Log("✗ Chưa xác minh vân tay!");
            return;
        }

        isOpen = true;
        isClosing = false;
        timer = 0;

        Debug.Log("→ Cửa đang mở...");
    }
}
