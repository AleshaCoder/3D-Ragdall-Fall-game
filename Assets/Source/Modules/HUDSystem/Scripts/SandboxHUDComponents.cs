using Assets.Source.InputService.Scripts;
using Assets.Source.RayCasterSystem.Scripts;
using RootMotion.Demos;
using RotationSystem;

namespace Assets.Source.HUDSystem.Scripts
{
    public struct SandboxHUDComponents
    {
        public bool IsMobile;
        public IInputMap Input;
        public IRayCaster RayCaster;
        public Joystick Joystick;
        public CharacterResetter CharacterResetter;
        public ItemRotator ItemRotator;
    }
}
