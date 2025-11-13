using UnityEngine;
using System.Collections;

public class cua_phong : Interactable
{
    [Header("Effects")]
    [SerializeField] private float openDuration = 1f; // thời gian mở cửa

    [Header("Door")]
    [SerializeField] private Transform doorTransform; // assign cái cửa cần xoay
    [SerializeField] private BoxCollider doorCollider; // collider của cửa

    private bool isOppened = false;
    private bool isRotating = false;
    
    public override void OnInteract()
    {
        if (isRotating) return;
        if (!isOppened)
        {
            StartCoroutine(RotateDoor(88f, openDuration, true)); 
        }
        else
        {
            StartCoroutine(RotateDoor(-88f, openDuration, false)); 
        }
    }

    private IEnumerator RotateDoor(float angle, float duration, bool opening)
    {
        isRotating = true;

        doorCollider.isTrigger = true; // bật collider khi cửa đang xoay

        float rotated = 0f;
        float speed = Mathf.Abs(angle) / duration;
        float direction = Mathf.Sign(angle);

        while (rotated < Mathf.Abs(angle))
        {
            float step = speed * Time.deltaTime;
            if (rotated + step > Mathf.Abs(angle)) step = Mathf.Abs(angle) - rotated; // không vượt quá
            doorTransform.Rotate(0, step * direction, 0);
            rotated += step;
            yield return null;
        }

        if(opening)
            doorCollider.isTrigger = true; // bật collider khi cửa đã mở
        else 
            doorCollider.isTrigger = false; // giữ tắt collider khi cửa đóng
        

        isRotating = false;
        isOppened = opening;
    }
}
