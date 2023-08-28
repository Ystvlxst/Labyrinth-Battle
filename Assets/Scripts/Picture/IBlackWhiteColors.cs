using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlackWhiteColors
{
    Vector2 TextureSize { get; }
    int StartBlackColors { get; }
    int StartWhiteColors { get; }
    int BlackColors { get; }
    int WhiteColors { get; }
}
