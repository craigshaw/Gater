namespace Gater.Framework
{
    public interface IState
    {
        void Initialise();
        void ProcessFrame();
    }
}
