using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TimeSystem
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private Slider timeSlider; // Slider для регулировки скорости времени

        private void Start()
        {
            // Устанавливаем начальные значения для Slider
            timeSlider.minValue = 0.2f;  // Минимальное значение (0.1 - минимальная скорость)
            timeSlider.maxValue = 1f;    // Максимальное значение (1 - нормальная скорость)
            timeSlider.value = 1f;       // Стартовое значение (1 - нормальная скорость)

            // Подписываемся на событие изменения значения Slider
            timeSlider.onValueChanged.AddListener(OnTimeSliderChanged);
        }

        // Метод, вызываемый при изменении значения Slider
        private void OnTimeSliderChanged(float value)
        {
            TimeService.Scale = value;
            Time.fixedDeltaTime = 0.02f * TimeService.Scale;
            Physics.gravity = new(0, -10f * TimeService.Scale * TimeService.Scale, 0);
        }

        private void OnDestroy()
        {
            // Отписываемся от события при удалении объекта
            timeSlider.onValueChanged.RemoveListener(OnTimeSliderChanged);
        }
    }
}
