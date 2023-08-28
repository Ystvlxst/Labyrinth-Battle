using UnityEngine;

public class BlackWhiteColors : IBlackWhiteColors
{
    private readonly Vector2 _textureSize;
    private readonly int _startBlackColors;
    private readonly int _startWhiteColors;

    public BlackWhiteColors(Vector2Int textureSize, int blackColors, int whiteColors)
    {
        _textureSize = textureSize;
        _startBlackColors = BlackColors = blackColors;
        _startWhiteColors = WhiteColors = whiteColors;
    }

    public Vector2 TextureSize => _textureSize;
    public int StartBlackColors => _startBlackColors;
    public int StartWhiteColors => _startWhiteColors;
    public int BlackColors { get; private set; }
    public int WhiteColors { get; private set; }

    public void Update(int blackColors, int whiteColors)
    {
        BlackColors = blackColors;
        WhiteColors = whiteColors;
    }
}
