using TextProject.Utils;

namespace TextProject.Player
{
    public class PlayerProfile
    {
        public SubscriptionProperty<GameState> CurrentState { get; }

        public PlayerProfile(GameState initialState)
        {
            CurrentState = new SubscriptionProperty<GameState>();
            CurrentState.Value = initialState;
        }
    }
}