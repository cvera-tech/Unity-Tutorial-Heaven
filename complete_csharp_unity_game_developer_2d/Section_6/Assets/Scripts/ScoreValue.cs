using UnityEngine;

public class ScoreValue : MonoBehaviour
{
    // [SerializeField] private bool _shouldGrantPointsOnDestroy = true;
    [SerializeField] private IntEventChannelSO _scoreChangeEventChannel;
    [SerializeField] private int _amount = 0;

    private void OnEnable()
    {
        if (_scoreChangeEventChannel == null)
        {
            Debug.Log(gameObject.name + " has no assigned Score Change Event Channel!");
        }
    }

    // private void OnDestroy()
    // {
    //     if (_shouldGrantPointsOnDestroy)
    //     {
    //         SendScoreChangeEvent();
    //     }
    // }

    public void SendScoreChangeEvent()
    {
        if (_scoreChangeEventChannel != null)
        {
            _scoreChangeEventChannel.RaiseEvent(_amount);
        }
    }
}
