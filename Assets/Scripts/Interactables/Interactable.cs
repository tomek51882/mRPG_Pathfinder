using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float radius = 3f;
    public Transform focusedBy;
    public virtual void Interact()
    {

    }
    public void OnFocus(Transform transform)
    {
        focusedBy = transform;
        float distance = Vector3.Distance(focusedBy.position, this.transform.position);
        if (distance <= radius)
        {
            Interact();
        }
        else
        {
            OnDefocus();
        }
    }
    public void OnDefocus()
    {
        focusedBy = null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.gameObject.GetComponent<Outline>() == null)
        {
            this.gameObject.AddComponent<Outline>().OutlineColor = new Color(1f, 0.65f, 0f);
            this.gameObject.GetComponent<Outline>().OutlineWidth = 3f;
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(this.gameObject.GetComponent<Outline>());
    }
}
