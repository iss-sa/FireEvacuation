using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObjectVisual : MonoBehaviour
{
    [SerializeField] private Objects_clear objectInteract;
    [SerializeField] private GameObject visualGameObject;

    // change color when player is facing object to a light blue

    // listen to player visual change event
    private void Start()
    {
        Player.Instance.OnSelectedVisualChanged += Player_OnSelectedVisualChanged;
    }

    private void Player_OnSelectedVisualChanged(object sender, Player.OnSelectedVisualChangedEventArgs e)
    {
        if (e.selectedObjects == objectInteract) 
        {
            Show();
        } else {
            Hide();
        }
    }

    private void Show()
    {
        visualGameObject.SetActive(true);
    }

    private void Hide()
    {
        visualGameObject.SetActive(false);
    }
}
