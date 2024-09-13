using System;
using UnityEngine;

namespace Assets.Source.GravityGunSystem.Scripts
{
    internal abstract class GravityGunState
    {
        protected GravityGunHandler _handler;

        protected Rigidbody GrabbedObject => _handler.CurrentGrabbedObject.Rigidbody;

        protected GravityGunState(GravityGunHandler handler) => _handler = handler ?? throw new ArgumentNullException(nameof(handler));

        internal virtual void Enter() { }
        internal virtual void Tick() { }
        internal virtual void FixedTick() { }
        internal virtual void OnDoubleClick() { }
        internal virtual void OnLongClick(Vector3 position) { }
        internal virtual void Exit() { }
    }
}