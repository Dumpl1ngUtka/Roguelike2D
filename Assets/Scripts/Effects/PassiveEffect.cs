using UnityEngine;

namespace PassiveEffects
{
    public abstract class PassiveEffect
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        protected PlayerParameters CurrentPlayerParameters;

        public PassiveEffect(PlayerParameters currentPlayerParameters)
        {
            CurrentPlayerParameters = currentPlayerParameters;
        }

        public abstract PlayerParameters OnEnter();

        public abstract PlayerParameters PeriodicEffect();
    }
}