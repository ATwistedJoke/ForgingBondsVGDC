using UnityEngine;
using System.Collections.Generic;

public class GameState
{
    // 1. Player info
    public PlayerModel Player { get; set; }

    // 2. Characters & their affection
    //    These are your romanceables / important NPCs
    public List<CharacterModel> Characters { get; set; }

    // 3. Story / progression
    public int DayNumber { get; set; }          // e.g., 1, 2, 3...
    public int ChapterNumber { get; set; }      // e.g., 1, 2, 3...
    public string CurrentSceneId { get; set; }  // e.g., "intro_01", "alex_library_02"

    // 4. Route info (optional for now – can be null / empty)
    public string CurrentRouteId { get; set; }  // e.g., "common", "alex_route", "luna_route"

    // 5. Story flags & past choices

    // Simple yes/no flags like:
    // "met_alex", "attended_festival", "confessed_to_alex"
    public HashSet<string> StoryFlags { get; set; }

    // Records key choices: choiceId -> optionId
    // e.g. "alex_first_meeting" -> "helped_her"
    public Dictionary<string, string> Choices { get; set; }

    // Basic constructor initializes collections so nothing is null
    public GameState()
    {
        Characters = new List<CharacterModel>();
        StoryFlags = new HashSet<string>();
        Choices = new Dictionary<string, string>();
    }
}
