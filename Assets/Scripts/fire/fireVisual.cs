using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireVisual : MonoBehaviour
{
    [SerializeField] private GameObject fireVisualObj;
    public bool active = false;
    public bool used = false;
    private void Update()
    {
        if (active)
        {
            fireVisualObj.SetActive(true);
            fireVisualObj.tag = "Fire";
        }
        else{
            fireVisualObj.SetActive(false);
        }
    }
}
