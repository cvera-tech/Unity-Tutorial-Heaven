using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 10f;

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
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
