using Assets.Source.InputService.Scripts;
using Assets.Source.RayCasterSystem.Scripts;
using DamageSystem;
using RootMotion.Demos;
using RotationSystem;
using SkinsSystem;

namespace Assets.Source.HUDSystem.Scripts
{
    public struct SandboxHUDComponents
    {
        public bool IsMobile;
        public IInputMap Input;
        public IRayCaster RayCaster;
        public Joystick Joystick;
        public ItemRotator ItemRotator;
        public SkinSelector SkinSelector;
        public IScore Score;
        public IDamageMessage Damage;
    }
}
