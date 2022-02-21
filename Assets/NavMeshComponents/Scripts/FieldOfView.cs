using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


namespace Enemies
{
    public class FieldOfView : MonoBehaviour
    {
        private Mesh _mesh;
        private LayerMask _layerMask;

        public float Fov = 90f;
        public float ViewDistance = 5f;
        public int RayCount = 100;

        private Vector3 _direction;
        private Vector3 _enemyPosition;

        private void Awake()
        {

           //_layerMask = GetComponent<EnemyAiTutorial>().whatIsGround;

            _mesh = new Mesh();
            GetComponent<MeshFilter>().mesh = _mesh;
        }

        private void LateUpdate()
        {
            var rayCount = RayCount;
            
            // Angle deference between 2 rays.
            var angleIncrease = Fov / rayCount;

            // Create arrays
            var vertices = new Vector3[rayCount + 1];
            var uv = new Vector2[vertices.Length];
            var triangles = new int[rayCount * 3];

            
            // First vertice starts from enemy position.
            vertices[0] = _enemyPosition;

            var vertexIndex = 1;
            var triangleIndex = 0;

            // _direction starts from the direction enemy is looking at.
            // Need to move direction by halve of FOV, to get correct direction.
            var direction = Quaternion.Euler(0, -Fov / 2, 0) * _direction;

            for (var i = 0; i < rayCount; i++)
            {
                direction = Quaternion.Euler(0, angleIncrease, 0) * direction;

                var vertex = _enemyPosition + direction * ViewDistance;

                if (Physics.Raycast(_enemyPosition, direction, out var raycastHit, ViewDistance, _layerMask))
                {
                    // Draw vertex until the collision point 
                    vertex = raycastHit.point;
                }

                vertices[vertexIndex] = vertex;

                // First ray won't have previous vertex to connect to (vertexIndex - 1)
                if (i > 0)
                {
                    // Connect vertex to triangle.
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }

                vertexIndex++;
            }

            // creates mesh
            _mesh.vertices = vertices;
            _mesh.uv = uv;
            _mesh.triangles = triangles;
        }

        public void SetEnemyPosition(Vector3 position) => _enemyPosition = position;

        public void SetEnemyDirection(Vector3 direction) => _direction = direction;
    }
}