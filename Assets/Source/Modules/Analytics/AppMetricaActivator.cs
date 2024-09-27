using Io.AppMetrica;
using UnityEngine;

namespace Analytics
{
    public static class AppMetricaActivator
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Activate()
        {
            AppMetrica.Activate(new AppMetricaConfig("7d9ac9e8-c4c3-4a25-b531-c80561ea84d8"));
        }
    }
}
