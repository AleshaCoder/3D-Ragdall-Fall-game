using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TimeSystem
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private Slider timeSlider; // Slider ��� ����������� �������� �������

        private void Start()
        {
            // ������������� ��������� �������� ��� Slider
            timeSlider.minValue = 0.2f;  // ����������� �������� (0.1 - ����������� ��������)
            timeSlider.maxValue = 1f;    // ������������ �������� (1 - ���������� ��������)
            timeSlider.value = 1f;       // ��������� �������� (1 - ���������� ��������)

            // ������������� �� ������� ��������� �������� Slider
            timeSlider.onValueChanged.AddListener(OnTimeSliderChanged);
        }

        // �����, ���������� ��� ��������� �������� Slider
        private void OnTimeSliderChanged(float value)
        {
            TimeService.Scale = value;
            Time.fixedDeltaTime = 0.02f * TimeService.Scale;
            Physics.gravity = new(0, -10f * TimeService.Scale * TimeService.Scale, 0);
        }

        private void OnDestroy()
        {
            // ������������ �� ������� ��� �������� �������
            timeSlider.onValueChanged.RemoveListener(OnTimeSliderChanged);
        }
    }
}
