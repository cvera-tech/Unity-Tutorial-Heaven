using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] GameObject thingToFollow;

    void LateUpdate()
    {
        transform.position = new Vector3(
            thingToFollow.transform.position.x,
            thingToFollow.transform.position.y,
            transform.position.z
        );
    }
}
