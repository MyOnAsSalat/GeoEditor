
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject sphere;
    public float Speed = 4;
    public GameObject Point;
    void Start()
    {
        GeoLines earthLines = new GeoLines(2);
        Instantiate(Point, TestConverter.SphericalToCartesian(new Vector3(5, 0 * Mathf.Deg2Rad, 180f * Mathf.Deg2Rad)),Quaternion.identity);
    }



    void Update()
    {


        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-Vector3.right * Time.deltaTime * Speed);
            transform.LookAt(sphere.transform);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
            transform.LookAt(sphere.transform);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime * Speed);
            transform.LookAt(sphere.transform);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-Vector3.up * Time.deltaTime * Speed);
            transform.LookAt(sphere.transform);
        }
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
           // Vector3 v = transform.position + transform.forward * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * 10;
       //     if (Vector3.Distance(v, pointer.P) < pointer.R * 3 && Vector3.Distance(v, pointer.P) > pointer.R * 1.7f)
          //      transform.position += transform.forward * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * 10;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = this.GetComponent<UnityEngine.Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit info; 
            Physics.Raycast(ray,out info);
            var sc = SphericalConverter.CartesianToSpherical(info.point);
            Debug.Log(sc * Mathf.Rad2Deg);
            var cc = SphericalConverter.SphericalToCartesian(sc);
            Instantiate(Point,cc,Quaternion.identity);
        }
    }
    
}
