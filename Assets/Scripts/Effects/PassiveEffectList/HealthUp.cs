namespace PassiveEffects
{
    public class HealthUp : PassiveEffect
    {
        private string _name = "Health+";
        private static float value = 10;
        private string _description = $"Health increases by {value}%";

        public HealthUp(PlayerParameters currentPlayerParameters) : base(currentPlayerParameters)
        {
            Name = _name;
            Description = _description;
        }

        public override PlayerParameters OnEnter()
        {
            var newPlayerParameters = CurrentPlayerParameters;
            newPlayerParameters.Health *= 1.1f;
            return newPlayerParameters;
        }

        public override PlayerParameters PeriodicEffect()
        {
            return CurrentPlayerParameters;
        }
    }
}

