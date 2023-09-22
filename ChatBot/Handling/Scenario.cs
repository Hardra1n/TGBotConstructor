using ChatBot.Handling.Actions;

namespace ChatBot.Handling;

public class Scenario
{
    public int Id { get; private set; }

    public HashSet<ScenarioStep> Steps;

    public Scenario(int id)
    {
        Id = id;
        Steps = new HashSet<ScenarioStep>();
    }

    public override bool Equals(object? obj)
    {
        return obj == null ? false : Equals(obj as Scenario);
    }

    public bool Equals(Scenario? scenario)
    {
        return scenario != null && Id == scenario.Id;
    }

    public override int GetHashCode()
    {
        return Id;
    }
}