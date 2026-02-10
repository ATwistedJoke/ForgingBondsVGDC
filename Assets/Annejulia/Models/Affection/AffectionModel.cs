public class AffectionModel
{
    public int Current { get; private set; }

    public AffectionModel(int startingValue = 0)
    {
        Current = startingValue;
    }

    public void Increase()
    {
        Current += GameConstants.ATTRACTION_INCREASE;

        if (Current > GameConstants.ATTRACTION_MAX)
            Current = GameConstants.ATTRACTION_MAX;
    }

    public void Decrease()
    {
        Current -= GameConstants.ATTRACTION_DECREASE;  // subtract the positive number

        if (Current < GameConstants.ATTRACTION_MIN)
            Current = GameConstants.ATTRACTION_MIN;
    }
}
