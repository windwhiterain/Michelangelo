namespace Michelangelo;
public interface IAgents<T>
{
    IEnumerable<T> agents { get; }
}
public interface IAgent<T>
{
    T agent { get; }
}
public static class Agent
{
    public static T AsAgent<T>(this T _) where T : IAgent<T> => _.agent;
    public static IEnumerable<T> AsAgents<T>(this T _) where T : IAgents<T> => _.agents;
}