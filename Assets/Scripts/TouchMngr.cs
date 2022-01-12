using UnityEngine;

public class TouchMngr : MonoBehaviour
{
    private GameObject _gObj;
    private Plane _objPlane;
    private Vector3 _mouseOffset;

    private Ray GenerateMouseRay()
    {
        Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane); 
        Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        
        mousePosFar = Camera.main.ScreenToWorldPoint(mousePosFar); 
        mousePosNear = Camera.main.ScreenToWorldPoint(mousePosNear);
        
        return new Ray(mousePosNear, mousePosFar - mousePosNear);
        }

    // Update is called once per frame
    private void Update()
    {
        Touch touch;
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }
        if (Input.GetMouseButtonDown(0))// || touch.phase == TouchPhase.Began)
        {
            Ray mouseRay = GenerateMouseRay();

            if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out var hit))
            {
                _gObj = hit.transform.gameObject;
                var position = _gObj.transform.position;
                _objPlane = new Plane(Camera.main.transform.forward * -1, position);

                Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                _objPlane.Raycast(mRay, out var rayDistance);
                _mouseOffset = position - mRay.GetPoint(rayDistance);
            }
        }
        else if (_gObj && (Input.GetMouseButton(0)))// || touch.phase == TouchPhase.Moved))
        {
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_objPlane.Raycast(mRay, out var rayDistance))
            {
                _gObj.transform.position = mRay.GetPoint(rayDistance) + _mouseOffset;
            }
        }
        else if (Input.GetMouseButtonUp(0))// || touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            _gObj = null;
        }
    }
}
