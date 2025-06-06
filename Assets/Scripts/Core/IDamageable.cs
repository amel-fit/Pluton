namespace Core
{
    public interface IDamageable
    {
        public float Health { get; set; }
        public void TakeDamage();
    }
}