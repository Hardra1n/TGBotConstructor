using ChatBot.Model;
using LiteDB;

namespace ChatBot.Data;

public static class DBExtensions
{
    public static IDictionary<long, UserState> GetUserStatesFromDB()
    {
        Dictionary<long, UserState> userStates = new();
        using (var db = new LiteDatabase(@".\users.db"))
        {
            var col = db.GetCollection<User>("users");
            var users = col.FindAll();
            foreach(var user in users)
            {
                userStates.TryAdd(user.Id, new UserState(user.ScenarioId, user.StepId));
            }
        }
        return userStates;
    }

    public static void UpdateUsers(IEnumerable<User> users)
    {
        using (var db = new LiteDatabase(@".\users.db"))
        {
            var col = db.GetCollection<User>("users");
            col.DeleteAll();
            foreach(var user in users)
            {
                col.Insert(user);
            }
        }
    }

}