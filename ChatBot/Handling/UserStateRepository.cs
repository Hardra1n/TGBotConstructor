using ChatBot.Model;
using LiteDB;
using ChatBot.Data;

public class UserStateRepository : IDisposable
{
    private IDictionary<long, UserState> userStates = new Dictionary<long, UserState>();

    public UserStateRepository()
    {

        userStates = DBExtensions.GetUserStatesFromDB();
    }

    public void Dispose()
    {
        DBExtensions.UpdateUsers(userStates.Select(state => new User(state.Key, state.Value.ScenarioId, state.Value.StepId)));
    }

    public UserState GetOrCreate(long id)
    {
        if (userStates.TryGetValue(id, out UserState? state))
        {
            return state;
        }
        else
        {
            var userState = new UserState();
            userStates.Add(id, userState);
            return userState;
        }
    }
}