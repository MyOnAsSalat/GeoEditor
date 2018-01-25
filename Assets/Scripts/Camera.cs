
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject sphere;
    public float Speed = 4;
    public GameObject Point;
    void Start()
    {
        GeoLines earthLines = new GeoLines(25);
       // Instantiate(Point, TestConverter.SphericalToCartesian(new Vector3(5, 0 * Mathf.Deg2Rad, 180f * Mathf.Deg2Rad)),Quaternion.identity);
    }

    private float a = 0;

    private void cp()
    {
        Destroy(GameObject.Find("point(Clone)"));
        Instantiate(Point, Converter.SphericalToCartesian(new Vector3(5, Mathf.Sin(a)*10 * Mathf.Deg2Rad, a * Mathf.Deg2Rad)), Quaternion.identity);
        Debug.Log(a);
        a += Time.deltaTime*20;
        
    }
    void Update()
    {
     //   cp();

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
            Vector3 v = transform.position + transform.forward * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * 50;
            if (this.transform.position.magnitude > v.magnitude)
            this.transform.position = (v.magnitude < 5.5f) ? this.transform.position : v;
            if (this.transform.position.magnitude < v.magnitude)
                this.transform.position = (v.magnitude > 15f) ? this.transform.position : v;
            //     if (Vector3.Distance(v, pointer.P) < pointer.R * 3 && Vector3.Distance(v, pointer.P) > pointer.R * 1.7f)
            //      transform.position += transform.forward * Time.deltaTime * Input.GetAxis("Mouse ScrollWheel") * 10;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = this.GetComponent<UnityEngine.Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit info; 
            Physics.Raycast(ray,out info);
            var sc = Converter.CartesianToSpherical(info.point);
            var cc = Converter.SphericalToCartesian(sc);
           // Debug.Log("Декартовы координаты точки: " + info.point);
            Debug.Log("Сферические координаты точки: " + sc.y * Mathf.Rad2Deg + " " + sc.z * Mathf.Rad2Deg);
          //  Debug.Log("Декартовы координаты точки: " + cc);
            Instantiate(Point,cc,Quaternion.identity);
        }
    }
    
}
