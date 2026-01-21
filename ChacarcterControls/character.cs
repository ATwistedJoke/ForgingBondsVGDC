// CharacterBase.cs
public abstract class CharacterBase
{
    public string Id { get; protected set; }
    public string DisplayName { get; protected set; }
    public int Age { get; protected set; }
    public int Level { get; protected set; }

    public AttractionLevel Attraction { get; protected set; }
    public int Trust { get; protected set; }
    public int Stress { get; protected set; }

    protected CharacterBase(
        string id,
        string displayName,
        int age,
        int level = 1
    )
    {
        Id = id;
        DisplayName = displayName;
        Age = age;
        Level = level;

        // Initialize stats
        Attraction = new AttractionLevel(startValue: 0, minValue: 0, maxValue: 100);
    }

    public void IncreaseAttraction(int amount){
        Attraction += amount;
    }
}
