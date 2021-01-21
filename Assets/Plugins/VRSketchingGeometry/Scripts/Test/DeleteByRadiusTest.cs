﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRSketchingGeometry.SketchObjectManagement;
using VRSketchingGeometry.Export;
using System.Xml;
using System.Xml.Serialization;
using VRSketchingGeometry.Meshing;
using VRSketchingGeometry.Serialization;

public class DeleteByRadiusTest : MonoBehaviour
{
    public GameObject selectionPrefab;
    public GameObject LineSketchObjectPrefab;
    private LineSketchObject lineSketchObject;
    public GameObject controlPointParent;
    public GameObject deletePoint;
    public float deleteRadius;

    private bool ranOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        lineSketchObject = Instantiate(LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        lineSketchObject.setLineDiameter(.5f);
    }

    IEnumerator changeDiameter() {
        yield return new WaitForSeconds(5);
        lineSketchObject.DeleteControlPoints(deletePoint.transform.position, deleteRadius);
        OBJExporter exporter = new OBJExporter();
        string exportPath = OBJExporter.GetDefaultExportPath();
        //exporter.ExportGameObject(lineSketchObject.gameObject, exportPath);
        Debug.Log(JsonUtility.ToJson(lineSketchObject));
        //XMLSerializeTest();
        //XMLSerializeTest2();
        //exporter.ExportGameObject(controlPointParent, exportPath);
        lineSketchObject.RefineMesh();

        Debug.Log(exportPath);
        //lineSketchObject.setLineDiameter(.1f);
        //yield return new WaitForSeconds(2);
        //lineSketchObject.deleteControlPoint();
        //lineSketchObject.deleteControlPoint();
    }

    private void XMLSerializeTest() {
        //LineSketchObject lso = new LineSketchObject();

        string path = System.IO.Path.Combine(Application.dataPath, "test_sketch.xml");
        Debug.Log(path);
        // Serialize the object to a file.
        // First write something so that there is something to read ...  
        var writer = new System.Xml.Serialization.XmlSerializer(typeof(Vector3[]));
        var wfile = new System.IO.StreamWriter(path);
        writer.Serialize(wfile, lineSketchObject);
        wfile.Close();

        // Now we can read the serialized book ...  
        System.Xml.Serialization.XmlSerializer reader =
            new System.Xml.Serialization.XmlSerializer(typeof(Vector3[]));
        System.IO.StreamReader file = new System.IO.StreamReader(
            path);
        Vector3[] overview = (Vector3[]) reader.Deserialize(file);
        file.Close();
    }

    private void XMLSerializeTest2() {
        string path = Serializer.WriteTestXmlFile<VRSketchingGeometry.Serialization.SerializableComponentData>
            (lineSketchObject.GetData());
        Serializer.DeserializeFromXmlFile(out LineSketchObjectData readData, System.IO.Path.Combine(Application.dataPath, "TestSerialization.xml"));
        LineSketchObject deserLine = Instantiate(LineSketchObjectPrefab).GetComponent<LineSketchObject>();
        readData.sketchMaterial.AlbedoColor = Color.red;
        deserLine.ApplyData(readData);

        deserLine.transform.position += new Vector3(0,2,0);

        Debug.Log(readData.ControlPoints.Count);
    }

    IEnumerator deactivateSelection(SketchObjectSelection selection) {
        yield return new WaitForSeconds(3);
        selection.deactivate();
    }

    private void lineSketchObjectTest() {

        foreach (Transform controlPoint in controlPointParent.transform) {
            lineSketchObject.addControlPoint(controlPoint.position);
        }

        //lineSketchObject.setLineDiameter(.7f);
        StartCoroutine(changeDiameter());

        //StartCoroutine(deactivateSelection(selection));
    }

    // Update is called once per frame
    void Update()
    {
        if (!ranOnce) {
            lineSketchObjectTest();
            ranOnce = true;
        }
    }
}
