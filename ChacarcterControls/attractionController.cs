public class AttractionLevel
{
    public int Value { get; private set; }

    public AttractionLevel(
        int startValue = GameConstants.ATTRACTION_MIN
    )
    {
        Value = startValue;
    }

    public void Increase(int amount)
    {
        Value += amount;
        if (Value > GameConstants.ATTRACTION_MAX)
            Value = GameConstants.ATTRACTION_MAX;
    }

    public void Decrease(int amount)
    {
        Value -= amount;
        if (Value < GameConstants.ATTRACTION_MIN)
            Value = GameConstants.ATTRACTION_MIN;
    }

    public void Reset()
    {
        Value = GameConstants.ATTRACTION_MIN;
    }
}
