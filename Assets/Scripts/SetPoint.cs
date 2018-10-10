using Boo.Lang;
using UnityEngine;

public class SetPoint : MonoBehaviour
{
    private List<GameObject> measureObjs = new List<GameObject>();
    private List<GameObject> connectionLines = new List<GameObject>();
    public GameObject dragableCube;


    private Vector3 dist;
    private float posX;
    private float posY;

    Ray ray;
    RaycastHit hit;

    private bool isEnter = false;

  

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("clicked left btn!!!");
            //get mouse position
            dist = Camera.main.WorldToScreenPoint(transform.position);
            posX = Input.mousePosition.x - dist.x;
            posY = Input.mousePosition.y - dist.y;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                //create measure cube
                GameObject obj = Instantiate(dragableCube, new Vector3(hit.point.x, hit.point.y, hit.point.z),Quaternion.identity);
                measureObjs.Add(obj);
            }

            // draw lines
            if (measureObjs.Count > 1)
            {
                var startPoint = measureObjs.Count - 2;
                var endPoint = measureObjs.Count - 1;

                DrawLine(measureObjs[startPoint].transform.position, measureObjs[endPoint].transform.position);
            }
        }

        //if (Input.GetKeyDown("return"))
        //{
        //    isEnter = true;
        //    Debug.Log(isEnter);
        //}

        if (measureObjs.Count >1)
        {
            //connectionLines[0].transform.position = measureObjs[0].transform.position;
            connectionLines[0].GetComponent<LineRenderer>().SetPosition(1, measureObjs[1].transform.position);
        }
    }



    void DrawLine(Vector3 start, Vector3 end)
    {
        var myLine = new GameObject();
        connectionLines.Add(myLine);

        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();

        lr.startColor = new Color(0.0f, 0.0f, 1.0f);
        lr.endColor = new Color(0.0f, 0.0f, 1.0f);
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }

}
