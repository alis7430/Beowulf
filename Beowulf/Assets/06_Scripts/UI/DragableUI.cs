using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragableUI : MonoBehaviour
{
    private Vector3 offset;

    public RectTransform parent;

    [HideInInspector]
    public Camera UIcamera;

    // Start is called before the first frame update
    void Start()
    {
        UIcamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginDrag()
    {
        offset = parent.position - UIcamera.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag()
    {
        if (Input.mousePosition.x == 0 || Input.mousePosition.y == 0 ||
            Input.mousePosition.x == Screen.width - 1 || Input.mousePosition.y == Screen.height - 1) return;

        
        parent.position = UIcamera.ScreenToWorldPoint(Input.mousePosition) + offset; 
    }
}
