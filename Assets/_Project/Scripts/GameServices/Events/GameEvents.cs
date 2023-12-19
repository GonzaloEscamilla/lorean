namespace _Project.Scripts.GameServices.Events
{
    public class CharacterLostHealth
    {
        public readonly int CurrentHealth;
        public readonly int PreviousHealth;
        public readonly int HealthLost;

        public CharacterLostHealth(int currentHealth, int previousHealth)
        {
            CurrentHealth = currentHealth;
            PreviousHealth = previousHealth;

            HealthLost = previousHealth - currentHealth;
        }
    }
}