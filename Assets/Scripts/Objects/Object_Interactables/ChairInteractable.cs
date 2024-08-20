using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairInteractable : MonoBehaviour
{
    private Rigidbody rb;
    private float throwForce = 6f;
    private float pushForce = 3f;
    private Vector3 spinDirection = new Vector3(1f, -0.5f, 3f);
    private float rationalityScore;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact Chair 1: Throw Chair");
            rb.velocity = Vector3.up * throwForce; 
            rb.angularVelocity = spinDirection;
            // Rationality Score -1
            statistics.rationalityScore += CalculateRationality("Throw");
        } else if (Input.GetKeyDown(KeyCode.R)){
            Debug.Log("Interact Chair 2: Push Chair");
            rb.velocity = Vector3.left * pushForce;
            // Rationality Score +0
        }
    }

    private float CalculateRationality(string choice = "")
    {
        if (choice == "Throw") 
        {
            rationalityScore = -1f;
        }

        return rationalityScore;
    }
}
