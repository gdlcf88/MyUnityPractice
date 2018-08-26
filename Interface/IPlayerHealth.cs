namespace Assets.Scripts.Interface
{
    public interface IPlayerHealth
    {
        int CurrentHealth { get; set; }

        void TakeDamage(int attackDamage);
    }
}
