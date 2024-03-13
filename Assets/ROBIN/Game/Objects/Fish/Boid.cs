using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public float SPEED = 2f;
    public float speed = 5f;
    public float rotationSpeed = 1f;
    public float neighborRadius = 3f;
    public float separationDistance = 1f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;
    public float separationWeight = 1f;
    public Vector3 boundarySize = new Vector3(10f, 10f, 10f);

    public GameObject shark;
    public float sharkSeparationDistance = 1f; 

    private List<Boid> neighbors = new List<Boid>();
    private Vector3 cohesionVector;
    private Vector3 separationVector;
    private Vector3 alignmentVector;

    private void Start() {
        speed = SPEED;
    }
    void Update()
    {
        FindNeighbors();
        CalculateBehaviors();
        Move();
    }

    void FindNeighbors()
    {
        neighbors.Clear();
        Collider[] colliders = Physics.OverlapSphere(transform.position, neighborRadius);
        foreach (Collider collider in colliders)
        {
            if (collider != null && collider != this.GetComponent<Collider>())
            {
                Boid neighbor = collider.GetComponent<Boid>();
                if (neighbor != null)
                {
                    neighbors.Add(neighbor);
                }
            }
        }
    }

    void CalculateBehaviors()
    {
        cohesionVector = Vector3.zero;
        separationVector = Vector3.zero;
        alignmentVector = Vector3.zero;

        foreach (Boid neighbor in neighbors)
        {
            cohesionVector += neighbor.transform.position;
            alignmentVector += neighbor.transform.forward;

            if (Vector3.Distance(transform.position, neighbor.transform.position) < separationDistance)
            {
                separationVector -= (neighbor.transform.position - transform.position);
            }
        }
        if (neighbors.Count > 0)
        {
            cohesionVector /= neighbors.Count;
            cohesionVector = cohesionVector - transform.position;
            alignmentVector /= neighbors.Count;
        }

        SharkAvoidance();
    }
    void SharkAvoidance()
    {
        if (shark != null && Vector3.Distance(transform.position, shark.transform.position) < sharkSeparationDistance)
        {
            float distanceToShark = Vector3.Distance(transform.position, shark.transform.position);
            float speedFactor = Mathf.Lerp(1f, 2f, 1f - (distanceToShark / sharkSeparationDistance));
            speed = SPEED * speedFactor;
            Vector3 awayFromSharkDirection = transform.position - shark.transform.position;
            awayFromSharkDirection.Normalize();
            Quaternion awayFromSharkRotation = Quaternion.LookRotation(awayFromSharkDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, awayFromSharkRotation, rotationSpeed * 2f * Time.deltaTime);
        }
        else
        {
            speed = SPEED;
        }
    }

    void Move()
    {
        Vector3 direction = (alignmentVector.normalized * alignmentWeight +
                            cohesionVector.normalized * cohesionWeight +
                            separationVector.normalized * separationWeight).normalized;

        direction += Random.insideUnitSphere * 0.5f;

        Vector3 newPosition = transform.position + transform.forward * speed * Time.deltaTime;

        // Calculate the direction to avoid walls
        Vector3 wallAvoidanceDirection = Vector3.zero;

        if (Mathf.Abs(newPosition.x) > boundarySize.x)
            wallAvoidanceDirection += -Mathf.Sign(newPosition.x) * Vector3.right / Mathf.Abs(newPosition.x);
        
        if (Mathf.Abs(newPosition.y) > boundarySize.y)
            wallAvoidanceDirection += -Mathf.Sign(newPosition.y) * Vector3.up / Mathf.Abs(newPosition.y);
        
        if (Mathf.Abs(newPosition.z) > boundarySize.z)
            wallAvoidanceDirection += -Mathf.Sign(newPosition.z) * Vector3.forward / Mathf.Abs(newPosition.z);

        // Weight the avoidance direction based on the distance from walls
        direction += wallAvoidanceDirection.normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);

        // Move the fish smoothly
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
