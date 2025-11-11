using UnityEngine;

public class NhapNhay : MonoBehaviour
{
    [SerializeField] private GameObject lightCotDien; // ánh sáng cần nhấp nháy
    [SerializeField] private float flashInterval = 0.5f;

    private float nextFlashTime;

    void Update()
    {
        if (Time.time >= nextFlashTime)
        {
            lightCotDien.SetActive(!lightCotDien.activeSelf);
            nextFlashTime = Time.time + flashInterval;
        }
    }
}
