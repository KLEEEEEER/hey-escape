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
        Mesh mesh;
        private void Start()
        {
            Debug.Log($"transform.position = {transform.position}");
            mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = mesh;
        }
        private void Update() 
        { 
            float fov = 360f;
            int rayCount = 70;
            float angle = 0f;
            float angleIncrease = fov / rayCount;
            float viewDistance = 5f;
            Vector3 origin = playerTransform.position;

            Vector3[] vertices = new Vector3[rayCount + 1 + 1];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[rayCount * 3];

            vertices[0] = origin;

            int vertexIndex = 1;
            int triangleIndex = 0;
            for (int i = 0; i <= rayCount; i++)
            {
                Vector3 vertex;
                RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
                //Debug.DrawLine(origin, GetVectorFromAngle(angle) * viewDistance, Color.white, 5f);
                //Debug.Log($"RaycastHit2D  origin = {origin} direction = {GetVectorFromAngle(angle)} distance = {viewDistance}");
                if (raycastHit2D.collider == null)
                {
                    vertex = origin + GetVectorFromAngle(angle) * viewDistance;
                }
                else
                {
                    //Debug.Log($"Hit something = {raycastHit2D.point}");
                    vertex = raycastHit2D.point;
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