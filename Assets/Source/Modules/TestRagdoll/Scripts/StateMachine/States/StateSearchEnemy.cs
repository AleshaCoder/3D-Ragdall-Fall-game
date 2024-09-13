using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateSearchEnemy : CharacterState
{
    [SerializeField] private Character _character;
    [SerializeField] private MonoBehaviour _searchTargetMono;
    [SerializeField] private float _maxDistance;

    private Coroutine _searchCoroutine;

    private ISearchTarget _searchTarget => (ISearchTarget)_searchTargetMono;

    protected override void OnEnter()
    {
        _character.SetTarget(null);
        _searchCoroutine = StartCoroutine(SearchDelay());
    }

    protected override void OnExit()
    {
        StopCoroutine(_searchCoroutine);
    }

    private void SearchTargets()
    {
        IReadOnlyList<ICharacterTarget> targets = _searchTarget.GetTargets<ICharacterTarget>(_character.RigidBody.transform.position, _maxDistance);
        IReadOnlyList<ICharacterTarget> enemies = targets.Where(t => t.TeamType != _character.TeamType && t.IsAlive).ToList();
        ICharacterTarget target = _searchTarget.GetNearestTarget(enemies, _character.RigidBody.transform.position, _maxDistance);
        _character.SetTarget(target);
    }

    private IEnumerator SearchDelay()
    {
        while (_character.Target == null)
        {
            //Debug.Log("SearchDelay");
            SearchTargets();
            yield return new WaitForSeconds(1);
        }
    }

    protected override void OnTick()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_character.RigidBody.transform.position, _maxDistance);
    }
}
