namespace Gater.Framework
{
    public interface IStateManager
    {
        void RegisterNextState(IState nextState);
    }
}
