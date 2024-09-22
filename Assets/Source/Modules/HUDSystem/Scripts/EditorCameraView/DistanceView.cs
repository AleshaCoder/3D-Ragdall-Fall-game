using UnityEngine;
using UnityEngine.UI;

public class DistanceView : MonoBehaviour
{
    [SerializeField] private Slider _distanceSlider;
    [SerializeField] private TMPro.TMP_Text _text;

    private void OnEnable()
    {
        _distanceSlider.onValueChanged.AddListener(ChangeText);
    }

    private void OnDisable()
    {
        _distanceSlider.onValueChanged.RemoveListener(ChangeText);
    }

    private void ChangeText(float arg0)
    {
        _text.text = $"{arg0:F2} m";
    }
}
