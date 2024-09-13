using RootMotion.Dynamics;
using UnityEngine;

public class Character : MonoBehaviour, ICharacterTarget
{
    [SerializeField] private TeamType _teamType;
    [SerializeField] private ITarget _target;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] Weapon _weapon;
    [SerializeField] private PuppetMaster _puppetMaster;

    public ITarget Target => _target;
    public Rigidbody RigidBody => _rigidBody;
    public Weapon Weapon => _weapon;
    public TeamType TeamType => _teamType;
    public PuppetMaster PuppetMaster => _puppetMaster;

    public Transform Transform => _rigidBody.transform;

    public bool IsAlive => _puppetMaster.state == PuppetMaster.State.Alive;

    public void SetTarget(ITarget target)
    {
        _target = target;
    }

    private void Update()
    {
        if (_target != null)
            Debug.DrawLine(_rigidBody.transform.position, _target.Transform.position, Color.yellow);
    }
}
