using TimeSystem;
using UnityEngine;

namespace Assets.Source.InputService.Scripts
{
    public class InputManager
    {
        private IInputMap _input;

        public InputManager(IInputMap input) => _input = input;

        public void Tick() => _input.Tick(TimeService.Delta);
    }
}