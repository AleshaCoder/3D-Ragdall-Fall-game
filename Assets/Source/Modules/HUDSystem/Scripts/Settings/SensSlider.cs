using GlobalSettings;
using UnityEngine.UI;

public class SensSlider : Slider
{
    protected override void OnEnable()
    {
        base.OnEnable();
        onValueChanged.AddListener(ChangeSensetivity);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onValueChanged.RemoveListener(ChangeSensetivity);
    }

    private void ChangeSensetivity(float value)
    {
        Settings.Sensetivity = value;
    }
}
