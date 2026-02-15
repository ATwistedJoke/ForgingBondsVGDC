using System;

public class PlayerModel : CharacterModel
{
    public PlayerModel(string name, int age, string pronouns)
        // we can add more traits later just a template for now
        : base(name, age, pronouns, height: 0, traits: Array.Empty<string>())
    {
        if (age < 18)
        {
            throw new ArgumentException("Player must be at least 18 years old.");
        }
    }
}
