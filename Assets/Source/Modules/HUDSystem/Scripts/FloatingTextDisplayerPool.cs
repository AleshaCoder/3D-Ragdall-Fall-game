using System.Collections.Generic;
using UnityEngine;

namespace Assets.Source.HUDSystem.Scripts
{
    public class FloatingTextDisplayerPool : MonoBehaviour
    {
        public List<FloatingTextDisplayer> displayers;
        private Queue<FloatingTextDisplayer> displayerQueue;

        private void Start()
        {
            displayerQueue = new Queue<FloatingTextDisplayer>(displayers);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
                DisplayText("Arm +100");
        }

        public void DisplayText(string text)
        {
            FloatingTextDisplayer displayer = GetAvailableDisplayer();
            displayer.DisplayText(text);

            MoveToBackOfQueue(displayer);
        }

        private FloatingTextDisplayer GetAvailableDisplayer()
        {
            foreach (var displayer in displayerQueue)
            {
                if (!displayer.Busy)
                {
                    return displayer;
                }
            }

            return displayerQueue.Peek();
        }

        private void MoveToBackOfQueue(FloatingTextDisplayer displayer)
        {
            displayerQueue.Dequeue();
            displayerQueue.Enqueue(displayer);
        }
    }
}
