using AkashaRecords.Models.ProjectModels;

namespace AkashaRecords.Models.ViewModels;

public class HomeVM
{
    public int CharacterCount { get; set; }
    public int WeaponCount { get; set; }
    public int BuildCount { get; set; }
    public IReadOnlyList<Character> LatestCharacters { get; set; } = Array.Empty<Character>();
    public IReadOnlyList<Build> LatestBuilds { get; set; } = Array.Empty<Build>();
}