namespace PassiveEffects
{
    public class HealthRegeneration : PassiveEffect
    {
        private string _name = "Health Regeneration";
        private static float _value = 25;
        private static float _time = 25;
        private float _valuePerSecond;
        private string _description = $"Restore {_value}% health in {_time} seconds";
        public HealthRegeneration(PlayerParameters currentPlayerParameters, float value = 25, float time = 25) : base(currentPlayerParameters)
        {
            Name = _name;
            _value = value;
            _time = time;
            _valuePerSecond = _value/_time;
            Description = _description;
        }

        public override PlayerParameters OnEnter()
        {
            return CurrentPlayerParameters;
        }

        public override PlayerParameters PeriodicEffect()
        {
            var periodicEffect = new PlayerParameters();
            periodicEffect.Health = CurrentPlayerParameters.Health * _valuePerSecond;
            return periodicEffect;
        }
    }
}
