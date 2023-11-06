using ChatBot.Model;

namespace ChatBot.Data;

public class User
{
    public long Id { get; set; }
    
    public int? StepId { get; set; }

    public int? ScenarioId { get; set; }
    
    public User() { }

    public User(long id, int? scenarioId, int? stepId)
    {
        Id = id;
        StepId = stepId;
        ScenarioId = scenarioId;
    }
}