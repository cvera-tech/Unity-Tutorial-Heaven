// TODO: Add a license notice to this file. It's from Unity's open project:
// https://github.com/UnityTechnologies/open-project-1/
// The project presented under the Apache 2.0 License.

using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Integer Event Channel")]
public class IntEventChannelSO : ScriptableObject
{
    public UnityAction<int> OnEventRaised;

    public void RaiseEvent(int value) => OnEventRaised?.Invoke(value);
}
