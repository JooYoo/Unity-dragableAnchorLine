using System;
using Boo.Lang;
using UnityEngine;

public class SetPoint : MonoBehaviour
{
    private List<GameObject> measureObjs = new List<GameObject>();
    private List<GameObject> connectionLines = new List<GameObject>();
    public GameObject dragableCube;

    Ray ray;
    RaycastHit hit;
    private int nameIndex = 0;
    private int selectedObjIndex;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // drag anchor 
        {
            Ray currentRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit currentHit;
            GameObject hitMeasureCube;
            GameObject findGameObj;
            if (Physics.Raycast(currentRay, out currentHit))
            {
                hitMeasureCube = currentHit.collider.gameObject;
                measureObjs.Find(x => x.name == hitMeasureCube.name, out findGameObj);
                // get selected Obj Index
                selectedObjIndex = Int32.Parse((findGameObj.name[findGameObj.name.Length - 1].ToString()));
            }

        }

        if (Input.GetMouseButtonDown(1)) // create anchor cube and connection lines
        {
            //create measure cube
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject obj = Instantiate(dragableCube, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.identity);
                obj.name = "cube" + nameIndex;
                measureObjs.Add(obj);

                nameIndex++;
            }

            // draw lines
            if (measureObjs.Count > 1)
            {
                var startPoint = measureObjs.Count - 2;
                var endPoint = measureObjs.Count - 1;
                DrawLine(measureObjs[startPoint].transform.position, measureObjs[endPoint].transform.position);
            }
        }
      

        if (Input.GetMouseButton(0)) // change drag the 
        {
            if (selectedObjIndex != 0) // the first measureCube without left line
            {
                // left Line: start                
                connectionLines[selectedObjIndex - 1].GetComponent<LineRenderer>().SetPosition(0, measureObjs[selectedObjIndex - 1].transform.position);
                // end
                connectionLines[selectedObjIndex - 1].GetComponent<LineRenderer>().SetPosition(1, measureObjs[selectedObjIndex].transform.position);
            }

            if (selectedObjIndex != measureObjs.Count -1) // the last measureCube without left line
            {
                // right Line: start
                connectionLines[selectedObjIndex].GetComponent<LineRenderer>().SetPosition(0, measureObjs[selectedObjIndex].transform.position);
                // end
                connectionLines[selectedObjIndex].GetComponent<LineRenderer>().SetPosition(1, measureObjs[selectedObjIndex + 1].transform.position);
            }
        }

    }


    void DrawLine(Vector3 start, Vector3 end)
    {
        var myLine = new GameObject();
        myLine.name = "line" + (nameIndex - 2);
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
