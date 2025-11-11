using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [Tooltip("Tên hiển thị khi nhắm vào vật này (VD: Cửa, Đèn, Hộp thư...)")]
    public string interactName = "Đồ vật";

    // Hàm ảo để override
    public abstract void OnInteract();
}
