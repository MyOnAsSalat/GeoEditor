

using UnityEngine;
using UnityEngine.Internal.Experimental.UIElements;
using UnityEngine.UI;

public class Camera : MonoBehaviour
{
    public GameObject sphere;   
    public GameObject rot;
    public GameObject scroll_view_content;
    public PanelWrapper panel;
    public GameObject plane;
    public float Speed = 4;
    public GameObject Point;
    public IReceiver manager;
    private int pointIndex = 0;

    public GameObject geo_catalog;
    public GameObject spere_mesh;
    void Start()
    {
        manager = GameObject.Find("Canvas").GetComponent<UIManager>();
        GeoLines earthLines = new GeoLines(25);
        earthLines.plane = plane;
    }

  


    void Update()
    {
        CameraManager();    
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = this.GetComponent<UnityEngine.Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit info; 
            Physics.Raycast(ray,out info);
            if (info.transform != null)
            {
                manager.Set(new PointC(info.point,InputType.Cartesian));
            }
        }
    }


    void CameraManager()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            rot.transform.Rotate(Vector3.up, -Input.GetAxis("Horizontal") * Speed * Time.deltaTime * 10, Space.World);           
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            if (Input.GetAxis("Vertical") > 0)
                rot.transform.Rotate(Vector3.right, (Mathf.Repeat(rot.transform.rotation.eulerAngles.x + 180, 360) - 90 < 170) ? Input.GetAxis("Vertical") * Speed * Time.deltaTime * 10 : 0, Space.Self);
            else
                rot.transform.Rotate(Vector3.right, (Mathf.Repeat(rot.transform.rotation.eulerAngles.x + 180, 360) - 90 > 10) ? Input.GetAxis("Vertical") * Speed * Time.deltaTime * 10 : 0, Space.Self);
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Vector3 v = transform.position + transform.forward * (Time.deltaTime * transform.position.magnitude * transform.position.magnitude) * Input.GetAxis("Mouse ScrollWheel") * 5;
            if (this.transform.position.magnitude > v.magnitude)
                this.transform.position = (v.magnitude < 5.5f) ? this.transform.position : v;
            if (this.transform.position.magnitude < v.magnitude)
                this.transform.position = (v.magnitude > 15f) ? this.transform.position : v;
        }
    }
    
}
