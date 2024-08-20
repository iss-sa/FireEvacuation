using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteractable : MonoBehaviour
{
    [SerializeField] private ObjectInteractUI objectInteractUI;
    private Rigidbody rb;
    private float throwForce = 6f;
    private Vector3 spinDirection = new Vector3(1f, -0.5f, 3f);
    EndScreenStatistics statistics;
    private void Awake()
    {
        statistics = GameObject.FindGameObjectWithTag("Score").GetComponent<EndScreenStatistics>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Interact Plant 1: Throw");
            rb.velocity = Vector3.up * throwForce; 
            rb.angularVelocity = spinDirection;
            // Rationality Score -2
            statistics.rationalityScore -= 2;
        } 
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact Plant: Ignore");
            objectInteractUI.Hide();
            // Rationality Score +0
        }
    }
}
