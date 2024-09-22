using Assets.Source.Structs.Scripts;
using DamageSystem;
using RootMotion.Demos;
using UnityEngine;

namespace SkinsSystem
{
    public class Skin : MonoBehaviour
    {
        [field: SerializeField] public RagdollType RagdollType { get; private set; }
        [field: SerializeField] public Transform Root { get; private set; }
        [field: SerializeField] public CharacterMeleeDemo CharacterMeleeDemo { get; private set; }
        [field: SerializeField] public CameraCharacterController CameraCharacterController { get; private set; }
        [field: SerializeField] public MobileUserControlMelee MobileUserControlMelee { get; private set; }
        [field: SerializeField] public CharacterResetter CharacterResetter { get; private set; }
        [field: SerializeField] public Damage Damage { get; private set; }

        [field: SerializeField] public Vector3 DefaultPosition { get; private set; }
        [field: SerializeField] public Quaternion DefaultRotation { get; private set; }
        [field: SerializeField] public Vector3 DefaultPositionChar { get; private set; }
        [field: SerializeField] public Quaternion DefaultRotationChar { get; private set; }

        public void SetDefaultPositionAndRotation()
        {
            DefaultPosition = Root.transform.position;
            DefaultRotation = Root.transform.rotation;
            DefaultPositionChar = CharacterMeleeDemo.transform.position;
            DefaultRotationChar = CharacterMeleeDemo.transform.rotation;
        }

        private void OnValidate()
        {
            Root = transform;
            CharacterMeleeDemo = GetComponentInChildren<CharacterMeleeDemo>();
            CameraCharacterController = GetComponentInChildren<CameraCharacterController>();
            MobileUserControlMelee = GetComponentInChildren<MobileUserControlMelee>();
            CharacterResetter = GetComponentInChildren<CharacterResetter>();
            Damage = GetComponentInChildren<Damage>();
            SetDefaultPositionAndRotation();
        }
    }
}
