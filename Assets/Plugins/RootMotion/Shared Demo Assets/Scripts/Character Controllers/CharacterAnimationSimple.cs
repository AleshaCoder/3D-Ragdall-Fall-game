﻿using UnityEngine;
using System.Collections;
using TimeSystem;

namespace RootMotion.Demos {
	
	/// <summary>
	/// Contols animation for a simple Mecanim character
	/// </summary>
	public class CharacterAnimationSimple: CharacterAnimationBase {

        public CharacterThirdPerson characterController;
        public float pivotOffset; // Offset of the rotating pivot point from the root
        public AnimationCurve moveSpeed; // The moving speed relative to input forward

		private Animator animator;
		
		protected override void Start() {
			base.Start();

			animator = GetComponentInChildren<Animator>();
		}
		
		public override Vector3 GetPivotPoint() {
			if (pivotOffset == 0) return transform.position;
			return transform.position + transform.forward * pivotOffset;
		}
		
		// Update the Animator with the current state of the character controller
		void Update() {
			float speed = moveSpeed.Evaluate(characterController.animState.moveDirection.z);

			// Locomotion
			animator.SetFloat("Speed", speed);

			// Movement
			characterController.Move(characterController.transform.forward * TimeService.Delta * speed, Quaternion.identity);
		}
	}
}

