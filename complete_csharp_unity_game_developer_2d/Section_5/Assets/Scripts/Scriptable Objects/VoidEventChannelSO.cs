// TODO: Add a license notice to this file. It's from Unity's open project:
// https://github.com/UnityTechnologies/open-project-1/tree/main
// The project presented under the Apache 2.0 License

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent() => OnEventRaised?.Invoke();
}