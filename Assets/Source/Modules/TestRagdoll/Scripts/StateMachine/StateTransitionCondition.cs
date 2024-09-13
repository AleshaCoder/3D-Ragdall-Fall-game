using UnityEngine;

public abstract class StateTransitionCondition : MonoBehaviour
{
    [SerializeField] private bool _invertResult;
    public bool CanEntry()
    {
        bool result = OnCanEntry();
        return _invertResult ? !result : result;
    }

    protected abstract bool OnCanEntry();
}
