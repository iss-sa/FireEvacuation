using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects_clear : MonoBehaviour
{
    [SerializeField] private string interactTextOption_1;
    [SerializeField] private string interactTextOption_2;
    [SerializeField] private WindowInteractable windowInteractable;
    [SerializeField] private StairsInteractable stairsInteractable;
    [SerializeField] private ChairInteractable chairInteractable;
    [SerializeField] private FireSafetyToolInteractable fireSafetyToolInteractable;
    [SerializeField] private DoorInteractable doorInteractable;
    [SerializeField] private PlantInteractable plantInteractable;
    [SerializeField] private AlarmInteractable alarmInteractable;
    [SerializeField] private PhoneInteractable phoneInteractable;
    [SerializeField] private FireInteract fireInteract;

    public string GetInteractTextOption_1() 
    {
        return interactTextOption_1;
    }

    public string GetInteractTextOption_2() 
    {
        return interactTextOption_2;
    }

    public void InteractWindow(int state)
    {
        windowInteractable.Interact(state);        
    }

    public void InteractStairs()
    {
        stairsInteractable.Interact();
    }

        public void InteractChair()
    {
        chairInteractable.Interact();
    }

    public void InteractFireSafetyTool()
    {
        fireSafetyToolInteractable.Interact();
    }

    public void InteractDoor(int state)
    {
        doorInteractable.Interact(state);
    }

    public void InteractPlant()
    {
        plantInteractable.Interact();
    }

    public void InteractAlarm()
    {
        alarmInteractable.Interact();
    }

    public void InteractPhone()
    {
        phoneInteractable.Interact();
    }

    public void InteractFire()
    {
        fireInteract.Interact();        
    }
}
