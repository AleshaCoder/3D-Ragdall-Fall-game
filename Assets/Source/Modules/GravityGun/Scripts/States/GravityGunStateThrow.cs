namespace Assets.Source.GravityGunSystem.Scripts
{
    internal sealed class GravityGunStateThrow : GravityGunState
    {
        internal GravityGunStateThrow(GravityGunHandler handler) : base(handler) { }

        internal override void Enter()
        {
            _handler.GraviGun.Throw(GrabbedObject, _handler.CameraDirection);
            _handler.SetState(_handler.Idle);
        }
    }
}