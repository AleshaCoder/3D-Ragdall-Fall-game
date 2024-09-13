using UnityEngine;

namespace Assets.Source.Extensions.Scripts
{
    public class ExtendedPlayerPrefs : PlayerPrefs
    {
        private static string TrueValue = "true";
        private static string FalseValue = "false";

        public void SetBool(string key, bool value)
        {
            if (value)
                SetString(key, TrueValue);
            else
                SetString(key, FalseValue);
        }

        public bool GetBool(string key)
        {
            return GetString(key, FalseValue) == TrueValue;
        }
    }
}
