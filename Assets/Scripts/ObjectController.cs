using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Each animals controller. I included a mouse down and drag so this demo can be played with out needing to use Unity's scene editor.
/// 
/// The main important piece of this is the object prefab enum
/// </summary>
public class ObjectController : MonoBehaviour
{
    public ObjectPrefab objectPrefab;

    private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int floorLayer = 1 << 9;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, floorLayer))
        {
            transform.position = hit.point;
        }
    }
}
