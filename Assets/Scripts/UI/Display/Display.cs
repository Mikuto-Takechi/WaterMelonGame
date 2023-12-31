using UniRx;

public class Display
{
    public enum State
    {
        Main,
        Option,
        License,
    }

    ReactiveProperty<State> _currentDisplayState = new();
    public IReadOnlyReactiveProperty<State> CurrentDisplayState => _currentDisplayState;
    public void ChangeDisplay(State state) => _currentDisplayState.Value = state;
}
