using ChatBot.Model;
using Telegram.Bot.Types;

public class UserStateRepository
{
    private Dictionary<long, UserState> userStates = new Dictionary<long, UserState>();

    public UserStateRepository()
    {

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