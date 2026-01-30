public class CharacterModel
{
    public string Name{get; protected set;}
    public string Pronouns{get; protected set;}
    public int Age{get; protected set;}
    public int Height{get; protected set;}
    public string[] Traits{ get; protected set;}


    //Affection
    public AffectionModel Affection{get; private set;}

    public CharacterModel(string name, int age, string pronouns, int height, string[] traits)
    {
        Name = name;
        Pronouns = pronouns;
        Age = age;
        Height = height;
        Traits = traits; 

        //Affection
        Affection = new AffectionModel();
    }

    public void IncreaseAffection()
    {
        Affection.Increase();
        System.Console.WriteLine($"{Name}'s affection is now {Affection.Current}");
    }

    public void DecreaseAffection()
    {
        Affection.Decrease();
        System.Console.WriteLine($"{Name}'s affection is now {Affection.Current}");
    }

}