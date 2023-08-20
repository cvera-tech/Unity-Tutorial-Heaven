using System;
using UnityEngine;

public class Driver : MonoBehaviour
{

    [SerializeField] float steerSpeed = 150;
    [SerializeField] float defaultMoveSpeed = 12;
    [SerializeField] float slowSpeedMultiplier = 0.5f;
    [SerializeField] float fastSpeedMultiplier = 1.5f;
    [SerializeField] float speedChangeDuration = 2f;

    private float currentMoveSpeed;
    private float speedChangeCooldown;

    void Start()
    {
        currentMoveSpeed = defaultMoveSpeed;
        speedChangeCooldown = 0;
    }

    void Update()
    {
        float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime;
        float moveAmount = Input.GetAxis("Vertical") * currentMoveSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -steerAmount);
        transform.Translate(0, moveAmount, 0);

        speedChangeCooldown = Math.Max(speedChangeCooldown - Time.deltaTime, 0);
        if (speedChangeCooldown == 0)
            currentMoveSpeed = defaultMoveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Speedup"))
        {
            currentMoveSpeed = defaultMoveSpeed * fastSpeedMultiplier;
            speedChangeCooldown = speedChangeDuration;
            Debug.Log("Time to haul mass!");
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Obstacle"))
        {
            currentMoveSpeed = defaultMoveSpeed * slowSpeedMultiplier;
            speedChangeCooldown = speedChangeDuration;
            Debug.Log("Cargo Slowly");
        }
    }
}
