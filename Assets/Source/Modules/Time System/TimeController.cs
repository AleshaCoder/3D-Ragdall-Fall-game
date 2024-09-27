using UnityEngine;
using UnityEngine.UI;

namespace TimeSystem
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private Slider timeSlider; // Slider ��� ����������� �������� �������
        [SerializeField] private GravityScaler gravityScaler;

        private void Start()
        {
            // ������������� ��������� �������� ��� Slider
            timeSlider.minValue = 0.2f;  // ����������� �������� (0.1 - ����������� ��������)
            timeSlider.maxValue = 1f;    // ������������ �������� (1 - ���������� ��������)
            timeSlider.value = 1f;       // ��������� �������� (1 - ���������� ��������)

            // ������������� �� ������� ��������� �������� Slider
            timeSlider.onValueChanged.AddListener(OnTimeSliderChanged);
            gravityScaler.Scale(TimeService.Scale);
        }

        // �����, ���������� ��� ��������� �������� Slider
        private void OnTimeSliderChanged(float value)
        {
            TimeService.Scale = value;
            Time.timeScale = value;
            Time.fixedDeltaTime = 0.02f * TimeService.Scale;
            //gravityScaler.Scale(TimeService.Scale);
        }

        private void OnDestroy()
        {
            // ������������ �� ������� ��� �������� �������
            timeSlider.onValueChanged.RemoveListener(OnTimeSliderChanged);
            TimeService.Scale = 1;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}
