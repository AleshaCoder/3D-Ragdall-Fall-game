using UnityEngine;

public static class ScreenExtensions
{
    /// <summary>
    /// Возвращает центр экрана как Vector3.
    /// </summary>
    public static Vector3 ScreenCenter => new(Screen.width / 2f, Screen.height / 2f, 0f);
}
