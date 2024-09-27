using UnityEngine;
using UnityEngine.UI;

namespace TimeSystem
{
    public class TimeController : MonoBehaviour
    {
        [SerializeField] private Slider timeSlider; // Slider для регулировки скорости времени
        [SerializeField] private GravityScaler gravityScaler;

        private void Start()
        {
            // Устанавливаем начальные значения для Slider
            timeSlider.minValue = 0.2f;  // Минимальное значение (0.1 - минимальная скорость)
            timeSlider.maxValue = 1f;    // Максимальное значение (1 - нормальная скорость)
            timeSlider.value = 1f;       // Стартовое значение (1 - нормальная скорость)

            // Подписываемся на событие изменения значения Slider
            timeSlider.onValueChanged.AddListener(OnTimeSliderChanged);
            gravityScaler.Scale(TimeService.Scale);
        }

        // Метод, вызываемый при изменении значения Slider
        private void OnTimeSliderChanged(float value)
        {
            TimeService.Scale = value;
            Time.timeScale = value;
            Time.fixedDeltaTime = 0.02f * TimeService.Scale;
            //gravityScaler.Scale(TimeService.Scale);
        }

        private void OnDestroy()
        {
            // Отписываемся от события при удалении объекта
            timeSlider.onValueChanged.RemoveListener(OnTimeSliderChanged);
            TimeService.Scale = 1;
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f;
        }
    }
}
