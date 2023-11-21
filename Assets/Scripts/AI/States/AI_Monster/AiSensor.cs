using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AiSensor : MonoBehaviour
{
    [Header("General Detection")]
    public Transform castPoint;

    [Range (0,360)]
    public float angle;
    public float closeDetectionDistance;
    public float distance;    
    public float maxHeight;
    public float minHeight;
    public float scanFrequency;
    private float detectDelay;
    public Color meshColour = Color.red;

    [Header("Layers/List")]
    public LayerMask layers;
    public LayerMask occlusionLayers;
    public List<GameObject> objects = new List<GameObject>();
    Collider[] colliders = new Collider[50];
    private int count;
    private float scanTimer;
    private AiAgent agent;
    private Mesh mesh;
    void Start()
    {
        agent = GetComponent<AiAgent>();
        detectDelay = agent.config.detectedCoolOff;
        scanTimer = scanFrequency;
    }
    void Update()
    {
        mesh = CreateWedgeMesh();
        scanTimer -= Time.deltaTime;
        if(scanTimer < 0)
        {
            scanTimer += scanFrequency;
            Scan();
        }
    }
    private void Scan()
    {
        count = Physics.OverlapSphereNonAlloc(castPoint.position, distance, colliders, layers, QueryTriggerInteraction.Ignore);
        objects.Clear();

        for (int i = 0; i < count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if(IsInSight(obj, true))
            {
                objects.Add(obj);
            }
        }
        count = Physics.OverlapSphereNonAlloc(transform.position, closeDetectionDistance, colliders, layers, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < count; i++)
        {
            GameObject obj = colliders[i].gameObject;
            if(IsInSight(obj, false))
            {
                objects.Add(obj);
            }
        }
    }
    public bool IsInSight(GameObject obj, bool useAngle)
    {
        Vector3 origin = castPoint.transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;
        if(direction.y < minHeight || direction.y > maxHeight)
        {
            return false;
        }
        
        direction.y = 0;
        
        if(useAngle)
        {
            float deltaAngle = Vector3.Angle(direction, castPoint.transform.forward);
            if(deltaAngle > angle)
            {
                return false;
            }
        }
        
        origin.y += maxHeight / 2;
        dest.y = origin.y;
        if(Physics.Linecast(castPoint.transform.position, dest, occlusionLayers, QueryTriggerInteraction.Ignore))
        {
            return false;
        }
        return true;
    }
    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = (segments * 4) + 2 + 2;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 bottomCenter = Vector3.zero;
        Vector3 bottomLeft = Quaternion.Euler(0, -angle, 0) * Vector3.forward * distance;
        Vector3 bottomRight = Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;

        Vector3 topCenter = bottomCenter + Vector3.up * maxHeight;
        Vector3 topRight = bottomRight + Vector3.up * maxHeight;
        Vector3 topLeft = bottomLeft + Vector3.up * maxHeight;

        int vert = 0;

        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        for (int i = 0; i < segments; i++)
        {
            bottomLeft = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * distance;
            bottomRight = Quaternion.Euler(0, currentAngle + deltaAngle, 0) * Vector3.forward * distance;

            topRight = bottomRight + Vector3.up * maxHeight;
            topLeft = bottomLeft + Vector3.up * maxHeight;

            vertices[vert++] = bottomLeft;
            vertices[vert++] = bottomRight;
            vertices[vert++] = topRight;

            vertices[vert++] = topRight;
            vertices[vert++] = topLeft;
            vertices[vert++] = bottomLeft;

            vertices[vert++] = topCenter;
            vertices[vert++] = topLeft;
            vertices[vert++] = topRight;

            vertices[vert++] = bottomCenter;
            vertices[vert++] = bottomRight;
            vertices[vert++] = bottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        scanTimer = scanFrequency;
    }
    private void OnDrawGizmos()
    {
        if(mesh)
        {
            Gizmos.color = meshColour;
            Gizmos.DrawMesh(mesh, castPoint.position, castPoint.rotation);
        }

        Gizmos.DrawWireSphere(transform.position, distance);
        for (int i = 0; i < count; i++)
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 1);
        }

       
        // Gizmos.color = Color.green;
        // foreach (var obj in objects)
        // {
        //     Gizmos.DrawSphere(obj.transform.position, 1);
        // }
        

        Gizmos.DrawWireSphere(transform.position, closeDetectionDistance);
        
        // Gizmos.color = Color.yellow;
        // Gizmos.DrawSphere(agent.transform.position, agent.config.attackRadius);
    }
}
