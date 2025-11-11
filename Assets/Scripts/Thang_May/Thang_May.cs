using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Thang_May : Interactable
{
    [SerializeField] private float speed_thang_may = 1f;
    [SerializeField] private int tang = 7;
    private int current_tang = 0;


    public override void OnInteract()
    {
       Debug.Log("Thang may dang di chuyen den tang " + tang);
    }
}
