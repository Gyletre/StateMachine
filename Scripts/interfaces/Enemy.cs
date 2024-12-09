using Godot;

namespace Interface
{
    public interface Enemy
    {
        void TakeDamage(Node2D instigator, int amount);
    }
}
