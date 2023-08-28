using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "LevelSettings", order = 51)]
public class LevelList : ScriptableObject
{
    [SerializeField] private List<SceneReference> _levels;

    public IReadOnlyList<SceneReference> Levels => _levels;
}
