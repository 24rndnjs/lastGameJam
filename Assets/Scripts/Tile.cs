using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public void Init(bool isoffset)
    {
        _spriteRenderer.color = isoffset ? _offsetColor : _baseColor;
    }

   
}
