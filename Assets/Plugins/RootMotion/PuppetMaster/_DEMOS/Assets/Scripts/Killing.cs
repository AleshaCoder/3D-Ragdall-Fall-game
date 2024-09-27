using UnityEngine;
using System.Collections;
using RootMotion.Dynamics;
using System.Collections.Generic;
using System.Linq;
using Analytics;

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
//Muscle muscle = puppetMaster.muscles.Where(m => m.props.group == Muscle.Group.Spine).Last();

            //muscle.rigidbody.AddForce((_character.transform.forward * _killForce*3), ForceMode.Impulse);

            foreach (var m in puppetMaster.muscles)
            {
                if (m.props.group == Muscle.Group.Spine)
                    m.rigidbody.AddForce((_character.transform.forward * _killForce * 3), ForceMode.Impulse);
				else if (m.props.group == Muscle.Group.Head)
                    m.rigidbody.AddForce((-_character.transform.forward * _killForce), ForceMode.Impulse);
                else if (m.props.group == Muscle.Group.Leg)
                    m.rigidbody.AddForce(( _character.transform.up * _killForce), ForceMode.Impulse);
                //m.AddForce((_character.transform.forward * _killForce + _character.transform.up * _killForce / 2), ForceMode.Impulse);
            }

			AnalyticsSender.KillCharacter();

		}

		void Update () {
			if (Input.GetKeyDown(KeyCode.F)) puppetMaster.Freeze(stateSettings);
			if (Input.GetKeyDown(KeyCode.R)) puppetMaster.Resurrect();
		}
	}
}
