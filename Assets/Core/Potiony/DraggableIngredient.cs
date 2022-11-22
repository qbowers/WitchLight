using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class DraggableIngredient : MonoBehaviour
{
    public Ingredient ingredientName;
    public Cauldron cauldron;

    private Vector3 snapBackPosition;

    public void Start()
    {
        if (cauldron == null) cauldron = transform.GetComponent<Cauldron>();
    }

    public void BeginDragHandler(BaseEventData basedata)
    {
        snapBackPosition = transform.position;
    }

    public void DragHandler(BaseEventData basedata)
    {
        PointerEventData data = (PointerEventData) basedata;
        transform.position = data.position;
    }

    // actually uses EndDragHandler event trigger
    public void DropHandler(BaseEventData basedata)
    {
        PointerEventData data = (PointerEventData) basedata;
        // if drop failed (not close enough to cauldron)
        if (!cauldron.DropIngredient(this))
        {
            // go back to shelf
            transform.position = snapBackPosition;
        }
    }
}
