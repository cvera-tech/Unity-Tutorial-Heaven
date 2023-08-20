using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 10f;
    [SerializeField] GameObject startPositionObject;

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        if (startPositionObject) {
            GetComponent<Transform>().position = startPositionObject.transform.position;
        }
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
