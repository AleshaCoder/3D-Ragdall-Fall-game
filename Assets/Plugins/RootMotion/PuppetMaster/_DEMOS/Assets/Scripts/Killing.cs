using UnityEngine;
using System.Collections;
using RootMotion.Dynamics;
using System.Collections.Generic;

namespace RootMotion.Demos {

	/// <summary>
	/// Switching PuppetMaster.state between Alive, Dead and Frozen
	/// </summary>
	public class Killing : MonoBehaviour {

		[Tooltip("Reference to the PuppetMaster component.")]
		public PuppetMaster puppetMaster;

		[Tooltip("Settings for killing and freezing the puppet.")]
		public PuppetMaster.StateSettings stateSettings = PuppetMaster.StateSettings.Default;
		[SerializeField] private float _killForce = 5;
		[SerializeField] private Transform _character;
		[SerializeField] private List<Rigidbody> _boostBones;

		public void Kill()
        {
			puppetMaster.Kill(stateSettings);

            foreach (var m in _boostBones)
            {
				m.AddForce((_character.transform.forward * _killForce -_character.transform.up * _killForce/3), ForceMode.Impulse);
            }
		}

		void Update () {
			if (Input.GetKeyDown(KeyCode.F)) puppetMaster.Freeze(stateSettings);
			if (Input.GetKeyDown(KeyCode.R)) puppetMaster.Resurrect();
		}
	}
}
