using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CraftedPotion : MonoBehaviour
{
    public string potionName;
    public Image sprite;

    public void Set(string _name, Color _color)
    {
        potionName = _name;
        sprite.color = _color;
    }
}
