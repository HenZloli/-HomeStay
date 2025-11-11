using UnityEngine;
using TMPro;

public class UIInteract : MonoBehaviour
{
    public TextMeshProUGUI interactText;

    public void Show(string text)
    {
        gameObject.SetActive(true);
        interactText.text = text;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
