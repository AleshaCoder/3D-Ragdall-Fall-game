using System;
using UnityEngine;

[Serializable]
public class CharacterAnimation
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public string StateName { get; private set; }
    [field: SerializeField] public float CrossFade { get; private set; }
    [field: SerializeField] public int Layer { get; private set; }

    public void PlayAnimation()
    {
        if (Animator != null && StateName != null)
            Animator.CrossFade(StateName, CrossFade, Layer, 0);
    }
}
