using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bear
{
    public class ScannerPainter : MonoBehaviour
    {
        public bool drawRange;
        public int cuts = 1;
        public Scanner sc;
        public List<Vector3> nodes;
        public List<int> triangles;
        
        Mesh mesh;

        public Mesh CreateMesh(float degree, float radius)
        {
            Mesh mesh = new Mesh();

            nodes = new List<Vector3>();
            triangles = new List<int>();

            Vector3 rootVec = Vector3.zero;
            Vector3 startVec = Quaternion.AngleAxis(-degree / 2, Vector2.up) * Vector3.forward * radius;
            int rootIndex = 0;
            int startIndex = 1;

            nodes.Add(rootVec);
            nodes.Add(startVec);

            float cuttedDegree = degree / cuts;

            for (int i = 0; i < cuts; i++)
            {
                startIndex = AddTriangle(startIndex, rootIndex, cuttedDegree);

            }
            mesh.vertices = nodes.ToArray();
            mesh.triangles = triangles.ToArray();

            mesh.RecalculateNormals();


            return mesh;
        }

        public int AddTriangle(int startIndex, int rootIndex, float degree)
        {
            Quaternion rotation = Quaternion.AngleAxis(degree, Vector3.up);
            Vector3 curr = nodes[startIndex];
            Vector3 newPos = rotation * curr;

            int index = nodes.Count;
            nodes.Add(newPos);

            triangles.Add(rootIndex);
            triangles.Add(startIndex);
            triangles.Add(index);

            return index;
        }

        private void OnValidate()
        {
            float degree = sc.degree;
            float radius = sc.radius;
            mesh = CreateMesh(degree, radius);            
        }

        private void OnDrawGizmos()
        {
            if (!drawRange || !sc.isScanning) {
                return;
            }

            if (mesh)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
            }

            Gizmos.DrawWireSphere(transform.position,sc.radius);
            foreach (Collider c in sc.visible) {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(c.transform.position,1);
            }

        }

    }
}