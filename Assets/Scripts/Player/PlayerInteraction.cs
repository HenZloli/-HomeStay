using UnityEngine;
using TMPro;

public class PlayerInteraction_Debug : MonoBehaviour
{
    [Header("Settings")]
    public float interactRange = 3f;
    public Camera playerCamera;
    public LayerMask interactLayer; 

    [Header("UI")]
    public GameObject interactUI;
    public TextMeshProUGUI interactText;

    private Interactable currentTarget;
    private Collider playerCollider;

    void Start()
    {
        playerCollider = GetComponent<Collider>(); // collider của Player
        if (interactUI != null) interactUI.SetActive(false);
    }

    void Update()
    {
        CheckForInteractable();
        HandleInteraction();
    }

    void CheckForInteractable()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, interactRange); // dùng RaycastAll để log tất cả

        Interactable found = null;

        Debug.DrawRay(ray.origin, ray.direction * interactRange, Color.red);

        foreach (var hit in hits)
        {
            // Debug.Log($"Raycast hit: {hit.collider.name} (Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)})");

            // Ignore collider của Player
            if (hit.collider == playerCollider) continue;

            // Chỉ tìm object có Interactable
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                found = interactable;
                break; // tìm thấy vật đầu tiên có Interactable
            }
        }

        if (found != null)
        {
            currentTarget = found;
            if (interactUI != null)
            {
                interactUI.SetActive(true);
                interactText.text = $"[E] Tương tác với {found.interactName}";
            }
        }
        else
        {
            currentTarget = null;
            if (interactUI != null)
                interactUI.SetActive(false);

            Debug.Log("da ngung tuong tac voi objs");
        }
    }

    void HandleInteraction()
    {
        if (currentTarget != null && Input.GetKeyDown(KeyCode.E))
        {
            currentTarget.OnInteract();
        }
    }
}
