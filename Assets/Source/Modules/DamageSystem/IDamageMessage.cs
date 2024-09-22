using System;

namespace DamageSystem
{
    public interface IDamageMessage
    {
        event Action<string> OnDamageMessage;
    }
}