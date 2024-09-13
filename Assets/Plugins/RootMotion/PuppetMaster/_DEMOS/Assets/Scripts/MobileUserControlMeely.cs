using Assets.Source.InputService.Scripts;
using RootMotion.Demos;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RootMotion.Demos
{
    public class MobileUserControlMeely : UserControlThirdPerson
    {
        private IInputMap _input;

        public void Construct(IInputMap input)
        {
            _input = input ?? throw new ArgumentNullException(nameof(input));

            //_input.Moving += OnMove;
            //_input.AlternativePointerMoving += OnRotate;
            //_input.AlternativePointerUp += OnRotateEnded;
        }
    }
}
