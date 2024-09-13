using UnityEngine;

namespace Assets.Source.CameraSystem.Scripts
{
    [CreateAssetMenu(fileName = "CameraMoverConfig", menuName = "Configs/CameraMoverConfig", order = 51)]
    internal class CameraMoverConfig : ScriptableObject
    {
        [Header("Config Type")]
        [SerializeField] internal CameraConfigType Type;
        [Header("Sensitivity")]
        [SerializeField] internal float MovingSpeed;
        [SerializeField] internal float HorizontalRotateSensitivity;
        [SerializeField] internal float VerticalRotateSensitivity;
        [Header("Limits")]
        [SerializeField] internal float MinYAngle;
        [SerializeField] internal float MaxYAngle;
    }
}