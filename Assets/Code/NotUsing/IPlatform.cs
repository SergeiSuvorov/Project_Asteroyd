namespace Asteroids.Abstrac_Factory
{
    public interface IPlatform
    {
        IInput Input { get; }
        IWindow Window { get; }
    }
}
