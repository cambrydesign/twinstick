public interface IState {
    public string stateName { get; }
    public void Enter();
    public void Execute();
    public void Exit();
}