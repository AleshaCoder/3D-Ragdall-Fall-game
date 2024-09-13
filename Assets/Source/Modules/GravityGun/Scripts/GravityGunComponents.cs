using System;
using UnityEngine;

namespace Assets.Source.GravityGunSystem.Scripts
{
    public struct GravityGunComponents
    {
        internal IGravityGunInput Input { get; private set; }
        internal Camera Camera { get; private set; }
        internal GravityGunHandler Handler { get; private set; }
        internal GravityGunState Idle { get; private set; }
        internal GravityGunState Drag { get; private set; }
        internal GravityGunState Throw { get; private set; }

        public GravityGunComponents(IGravityGunInput input, Camera camera, GravityGunHandler handler)
        {
            Input = input ?? throw new ArgumentNullException(nameof(input));
            Camera = camera ?? throw new ArgumentNullException(nameof(camera));
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));

            Idle = new GravityGunStateIdle(Handler);
            Drag = new GravityGunStateDrag(Handler);
            Throw = new GravityGunStateThrow(Handler);
        }
    }
}