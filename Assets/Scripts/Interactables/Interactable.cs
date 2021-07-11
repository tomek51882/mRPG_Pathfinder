using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public float radius = 3f;
    public Transform interactedBy;
    public OutlineStatusColor outlineColor;
    public virtual void Interact()
    {

    }
    public void InitiateInteraction(Transform transform)//called by played after left mouse click
    {
        float distance = Vector3.Distance(transform.position, this.transform.position);
        interactedBy = transform;
        Interact();
        //if (distance <= radius)
        //{
        //    //Debug.Log("Interaction accepted");
            
        //}
        //else
        //{
        //    //Debug.Log("Interaction denied");
        //    //OnDefocus();
        //}
    }
    public virtual void OnFocus()
    {
    }
    public virtual void OnDefocus()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.GetComponent<Outline>() == null)
        {
            this.gameObject.AddComponent<Outline>().OutlineColor = new Color(0f,0f,0f,0f);
            Outline outline = this.gameObject.GetComponent<Outline>();

            switch (outlineColor)
            {
                case OutlineStatusColor.Red:

                    outline.OutlineColor = new Color(0.7f, 0f, 0f, 1f);
                    break;
                case OutlineStatusColor.Yellow:
                    outline.OutlineColor = new Color(1f, 0.65f, 0f, 1f);
                    break;
                case OutlineStatusColor.Green:
                    outline.OutlineColor = new Color(0f, 0.8f, 0.15f, 1f);
                    break;
            }

            outline.OutlineWidth = 3f;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(this.gameObject.GetComponent<Outline>());
    }
}
public enum OutlineStatusColor { Red, Yellow, Green }
