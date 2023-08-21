using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 10f;
    [SerializeField] GameObject startPositionObject;
    [SerializeField] float defaultSpeed = 10f;
    [SerializeField] float boostSpeed = 20f;

    private bool controlsEnabled = true;
    private Rigidbody2D rb2d;
    private SurfaceEffector2D se2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        se2d = FindObjectOfType<SurfaceEffector2D>();
        if (startPositionObject) {
            GetComponent<Transform>().position = startPositionObject.transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (controlsEnabled)
        {
            RotatePlayer();
            BoostPlayer();
        }
    }

    public void DisableControls()
    {
        controlsEnabled = false;
    }

    private void BoostPlayer()
    {
        if (Input.GetAxis("Vertical") > 0)
        {
            se2d.speed = boostSpeed;
        }
        else
        {
            se2d.speed = defaultSpeed;
        }
    }

    private void RotatePlayer()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb2d.AddTorque(-torqueAmount);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rb2d.AddTorque(torqueAmount);
        }
    }
}
