using UnityEngine;
using System.Collections;

public class cua_phong : Interactable
{
    [Header("Effects")]
    [SerializeField] private float openDuration = 1f; // thời gian mở cửa
    [SerializeField] private float autoCloseDelay = 3f; // sau bao lâu cửa tự đóng

    [Header("Door")]
    [SerializeField] private Transform doorTransform; // assign cái cửa cần xoay

    private bool isOppened = false;
    private bool isRotating = false;
    

    public override void OnInteract()
    {
        if (isRotating) return;

        if (!isOppened)
        {
            StartCoroutine(RotateDoor(88f, openDuration, true)); // mở cửa
        }
        else
        {
            StartCoroutine(RotateDoor(-88f, openDuration, false)); // đóng cửa
        }
    }

    private IEnumerator RotateDoor(float angle, float duration, bool opening)
    {
        isRotating = true;

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

        isRotating = false;
        isOppened = opening;

        // Nếu là mở cửa, tự động đóng sau delay
        if (opening)
        {
            yield return new WaitForSeconds(autoCloseDelay);
            StartCoroutine(RotateDoor(-88f, openDuration, false));
        }
    }
}
