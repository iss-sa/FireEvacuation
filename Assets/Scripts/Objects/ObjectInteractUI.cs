using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI_1;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI_2;

    // listen to player visual change event
    private void Start()
    {
        Player.Instance.OnSelectedVisualChanged += Player_OnSelectedVisualChanged;
    }

    private void Player_OnSelectedVisualChanged(object sender, Player.OnSelectedVisualChangedEventArgs e)
    {
        if (e.selectedObjects != null) 
        {
            Show(e.selectedObjects); 

        } else {
            Hide();
        }
    }

    private void Show(Objects_clear selectedObjects)
    {
        if (selectedObjects.name == "Fire")
        {
            Debug.Log("Fire!");
            selectedObjects.InteractFire();
        }
        else
        {
            containerGameObject.SetActive(true);
            interactTextMeshProUGUI_1.text = selectedObjects.GetInteractTextOption_1();
            interactTextMeshProUGUI_2.text = selectedObjects.GetInteractTextOption_2();
        }
    } 

    public void Hide()
    {
        containerGameObject.SetActive(false);
    } 
}
