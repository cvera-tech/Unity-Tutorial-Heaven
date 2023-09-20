using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Collision2D Event Channel")]
public class Collision2DEventChannelSO : ScriptableObject
{
    public UnityAction<Collision2D, GameObject> OnEventRaised;

    public void RaiseEvent(Collision2D collision, GameObject caller) => OnEventRaised?.Invoke(collision, caller);
}
