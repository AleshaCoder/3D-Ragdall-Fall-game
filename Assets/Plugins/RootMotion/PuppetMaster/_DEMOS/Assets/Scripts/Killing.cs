using UnityEngine;
using System.Collections;
using RootMotion.Dynamics;

namespace RootMotion.Demos {

	/// <summary>
	/// Switching PuppetMaster.state between Alive, Dead and Frozen
	/// </summary>
	public class Killing : MonoBehaviour {

		[Tooltip("Reference to the PuppetMaster component.")]
		public PuppetMaster puppetMaster;

		[Tooltip("Settings for killing and freezing the puppet.")]
		public PuppetMaster.StateSettings stateSettings = PuppetMaster.StateSettings.Default;

		public void Kill()
        {
			puppetMaster.Kill(stateSettings);
		}

		void Update () {
			if (Input.GetKeyDown(KeyCode.F)) puppetMaster.Freeze(stateSettings);
			if (Input.GetKeyDown(KeyCode.R)) puppetMaster.Resurrect();
		}
	}
}
