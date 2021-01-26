using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace HeyEscape.Core.Player
{
    [RequireComponent(typeof(MeshFilter))]
    public class PlayerFieldOfView : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float raycastHitAdditionalDistance = 0.2f;
        [SerializeField] private float viewDistance = 5f;
        float fov = 360f;
        private RaycastHit2D[] results = new RaycastHit2D[20];

        [SerializeField] private int rayCount = 60;

        Mesh mesh;
        Vector3[] vertices;
        Vector2[] uv;
        int[] triangles;
        Vector3 origin;

        private void Start()
        {
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;

            vertices = new Vector3[rayCount + 1 + 1];
            uv = new Vector2[vertices.Length];
            triangles = new int[rayCount * 3];
        }
        private void Update() 
        { 
            float angle = 0f;
            float angleIncrease = fov / rayCount;

            origin = playerTransform.position;

            vertices[0] = origin;

            int vertexIndex = 1;
            int triangleIndex = 0;
            for (int i = 0; i <= rayCount; i++)
            {
                Vector3 vertex;
                Vector3 vectorFromAngle = GetVectorFromAngle(angle);

                int hits = Physics2D.RaycastNonAlloc(origin, vectorFromAngle, results, viewDistance, layerMask);
                if (hits == 0 || results[0].collider == null)
                {
                    vertex = origin + vectorFromAngle * viewDistance;
                }
                else
                {
                    vertex = results[0].point - (results[0].normal * raycastHitAdditionalDistance);
                }

                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }

                vertexIndex++;
                angle -= angleIncrease;
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;
            mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
        }


        private Vector3 GetVectorFromAngle(float angle)
        {
            float angleRad = angle * (Mathf.PI / 180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }
    }
}