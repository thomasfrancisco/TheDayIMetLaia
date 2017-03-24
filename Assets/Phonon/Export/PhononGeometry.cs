﻿//
// Copyright (C) Valve Corporation. All rights reserved.
//

using UnityEngine;

using System.Collections.Generic;

namespace Phonon
{

    [AddComponentMenu("Phonon/Phonon Geometry")]
    public class PhononGeometry : MonoBehaviour
    {
        public int GetNumVertices()
        {
            var numVertices = GetNumVerticesForGameObject(gameObject);

            if (exportAllChildren)
            {
                var meshes = GetComponentsInChildren<MeshFilter>();
                foreach (var mesh in meshes)
                    if (mesh.gameObject.GetComponent<PhononGeometry>() == null)
                        numVertices += GetNumVerticesForMesh(mesh);

                var terrains = GetComponentsInChildren<Terrain>();
                foreach (var terrain in terrains)
                    if (terrain.gameObject.GetComponent<PhononGeometry>() == null)
                        numVertices += GetNumVerticesForTerrain(terrain);
            }

            return numVertices;
        }

        public int GetNumTriangles()
        {
            var numTriangles = GetNumTrianglesForGameObject(gameObject);

            if (exportAllChildren)
            {
                var meshes = GetComponentsInChildren<MeshFilter>();
                foreach (var mesh in meshes)
                    if (mesh.gameObject.GetComponent<PhononGeometry>() == null)
                        numTriangles += GetNumTrianglesForMesh(mesh);

                var terrains = GetComponentsInChildren<Terrain>();
                foreach (var terrain in terrains)
                    if (terrain.gameObject.GetComponent<PhononGeometry>() == null)
                        numTriangles += GetNumTrianglesForTerrain(terrain);
            }

            return numTriangles;
        }

        public void GetGeometry(Vector3[] vertices, int vertexOffset, Triangle[] triangles, int triangleOffset)
        {
            var localVertexOffset = 0;
            var localTriangleOffset = 0;

            var numVerticesExported = GetVerticesForGameObject(gameObject, vertices, vertexOffset, localVertexOffset);
            var numTrianglesExported = GetTrianglesForGameObject(gameObject, triangles, triangleOffset,
                localTriangleOffset);

            FixupTriangleIndices(triangles, triangleOffset, localTriangleOffset, numTrianglesExported,
                vertexOffset + localVertexOffset);

            localVertexOffset += numVerticesExported;
            localTriangleOffset += numTrianglesExported;

            if (exportAllChildren)
            {
                var meshes = GetComponentsInChildren<MeshFilter>();
                foreach (var mesh in meshes)
                {
                    if (mesh.gameObject.GetComponent<PhononGeometry>() == null)
                    {
                        numVerticesExported = GetVerticesForMesh(mesh, vertices, vertexOffset, localVertexOffset);
                        numTrianglesExported = GetTrianglesForMesh(mesh, triangles, triangleOffset,
                            localTriangleOffset);

                        FixupTriangleIndices(triangles, triangleOffset, localTriangleOffset, numTrianglesExported,
                            vertexOffset + localVertexOffset);

                        localVertexOffset += numVerticesExported;
                        localTriangleOffset += numTrianglesExported;
                    }
                }

                var terrains = GetComponentsInChildren<Terrain>();
                foreach (var terrain in terrains)
                {
                    if (terrain.gameObject.GetComponent<PhononGeometry>() == null)
                    {
                        numVerticesExported = GetVerticesForTerrain(terrain, vertices, vertexOffset,
                            localVertexOffset);
                        numTrianglesExported = GetTrianglesForTerrain(terrain, triangles, triangleOffset,
                            localTriangleOffset);

                        FixupTriangleIndices(triangles, triangleOffset, localTriangleOffset, numTrianglesExported,
                            vertexOffset + localVertexOffset);

                        localVertexOffset += numVerticesExported;
                        localTriangleOffset += numTrianglesExported;
                    }
                }
            }
        }

        public void GetMaterial(ref Material material)
        {
            var attachedMaterial = GetComponent<PhononMaterial>();
            var materialValue = (attachedMaterial == null) ? PhononMaterialSettings.GetDefaultMaterial().Value :
                attachedMaterial.Value;

            material.absorptionLow = materialValue.LowFreqAbsorption;
            material.absorptionMid = materialValue.MidFreqAbsorption;
            material.absorptionHigh = materialValue.HighFreqAbsorption;
            material.scattering = materialValue.Scattering;
        }

        int GetNumVerticesForMesh(MeshFilter mesh)
        {
            return mesh.sharedMesh.vertexCount;
        }

        int GetVerticesForMesh(MeshFilter mesh, Vector3[] vertices, int offset, int localOffset)
        {
            var vertexArray = mesh.sharedMesh.vertices;

            for (var i = 0; i < vertexArray.Length; ++i)
            {
                vertices[offset + localOffset + i] =
                    Common.ConvertVector(mesh.transform.TransformPoint(vertexArray[i]));
            }

            return vertexArray.Length;
        }

        int GetNumVerticesForTerrain(Terrain terrain)
        {
            int w = terrain.terrainData.heightmapWidth;
            int h = terrain.terrainData.heightmapHeight;
            int s = Mathf.Min(w - 1, Mathf.Min(h - 1, (int)Mathf.Pow(2.0f, (float)TerrainSimplificationLevel)));

            if (s == 0)
                s = 1;

            w = ((w - 1) / s) + 1;
            h = ((h - 1) / s) + 1;

            return (w * h);
        }

        int GetVerticesForTerrain(Terrain terrain, Vector3[] vertices, int offset, int localOffset)
        {
            int w = terrain.terrainData.heightmapWidth;
            int h = terrain.terrainData.heightmapHeight;
            int s = Mathf.Min(w - 1, Mathf.Min(h - 1, (int)Mathf.Pow(2.0f, (float)TerrainSimplificationLevel)));

            if (s == 0)
                s = 1;

            w = ((w - 1) / s) + 1;
            h = ((h - 1) / s) + 1;

            UnityEngine.Vector3 position = terrain.transform.position;
            float[,] heights = terrain.terrainData.GetHeights(0, 0, terrain.terrainData.heightmapWidth,
                terrain.terrainData.heightmapHeight);

            var vertexIndex = 0;
            for (int v = 0; v < terrain.terrainData.heightmapHeight; v += s)
            {
                for (int u = 0; u < terrain.terrainData.heightmapWidth; u += s)
                {
                    float height = heights[v, u];

                    float x = position.x + (((float)u / terrain.terrainData.heightmapWidth) * 
                        terrain.terrainData.size.x);

                    float y = position.y + (height * terrain.terrainData.size.y);

                    float z = position.z + (((float)v / terrain.terrainData.heightmapHeight) * 
                        terrain.terrainData.size.z);

                    vertices[offset + localOffset + vertexIndex++] = 
                        Common.ConvertVector(new UnityEngine.Vector3 { x = x, y = y, z = z });
                }
            }

            return vertexIndex;
        }

        int GetNumVerticesForGameObject(GameObject obj)
        {
            var mesh = obj.GetComponent<MeshFilter>();
            var terrain = obj.GetComponent<Terrain>();

            if (mesh != null)
                return GetNumVerticesForMesh(mesh);
            else if (terrain != null)
                return GetNumVerticesForTerrain(terrain);
            else
                return 0;
        }

        int GetVerticesForGameObject(GameObject obj, Vector3[] vertices, int offset, int localOffset)
        {
            var mesh = obj.GetComponent<MeshFilter>();
            var terrain = obj.GetComponent<Terrain>();

            if (mesh != null)
                return GetVerticesForMesh(mesh, vertices, offset, localOffset);
            else if (terrain != null)
                return GetVerticesForTerrain(terrain, vertices, offset, localOffset);
            else
                return 0;
        }

        int GetNumTrianglesForMesh(MeshFilter mesh)
        {
            return mesh.sharedMesh.triangles.Length / 3;
        }

        int GetTrianglesForMesh(MeshFilter mesh, Triangle[] triangles, int offset, int localOffset)
        {
            var triangleArray = mesh.sharedMesh.triangles;

            for (var i = 0; i < triangleArray.Length / 3; ++i)
            {
                triangles[offset + localOffset + i].index0 = triangleArray[3 * i + 0];
                triangles[offset + localOffset + i].index1 = triangleArray[3 * i + 1];
                triangles[offset + localOffset + i].index2 = triangleArray[3 * i + 2];
            }

            return triangleArray.Length / 3;
        }

        int GetNumTrianglesForTerrain(Terrain terrain)
        {
            int w = terrain.terrainData.heightmapWidth;
            int h = terrain.terrainData.heightmapHeight;
            int s = Mathf.Min(w - 1, Mathf.Min(h - 1, (int)Mathf.Pow(2.0f, (float)TerrainSimplificationLevel)));

            if (s == 0)
                s = 1;

            w = ((w - 1) / s) + 1;
            h = ((h - 1) / s) + 1;

            return ((w - 1) * (h - 1) * 2);
        }

        int GetTrianglesForTerrain(Terrain terrain, Triangle[] triangles, int offset, int localOffset)
        {
            int w = terrain.terrainData.heightmapWidth;
            int h = terrain.terrainData.heightmapHeight;
            int s = Mathf.Min(w - 1, Mathf.Min(h - 1, (int)Mathf.Pow(2.0f, (float)TerrainSimplificationLevel)));

            if (s == 0)
                s = 1;

            w = ((w - 1) / s) + 1;
            h = ((h - 1) / s) + 1;

            int triangleIndex = 0;
            for (int v = 0; v < h - 1; ++v)
            {
                for (int u = 0; u < w - 1; ++u)
                {
                    int i0 = v * w + u;
                    int i1 = (v + 1) * w + u;
                    int i2 = v * w + (u + 1);

                    triangles[offset + localOffset + triangleIndex++] = new Triangle { index0 = i0, index1 = i1,
                        index2 = i2 };

                    i0 = v * w + (u + 1);
                    i1 = (v + 1) * w + u;
                    i2 = (v + 1) * w + (u + 1);

                    triangles[offset + localOffset + triangleIndex++] = new Triangle { index0 = i0, index1 = i1,
                        index2 = i2 };
                }
            }

            return triangleIndex;
        }

        int GetNumTrianglesForGameObject(GameObject obj)
        {
            var mesh = obj.GetComponent<MeshFilter>();
            var terrain = obj.GetComponent<Terrain>();

            if (mesh != null)
                return GetNumTrianglesForMesh(mesh);
            else if (terrain != null)
                return GetNumTrianglesForTerrain(terrain);
            else
                return 0;
        }

        int GetTrianglesForGameObject(GameObject obj, Triangle[] triangles, int offset, int localOffset)
        {
            var mesh = obj.GetComponent<MeshFilter>();
            var terrain = obj.GetComponent<Terrain>();

            if (mesh != null)
                return GetTrianglesForMesh(mesh, triangles, offset, localOffset);
            else if (terrain != null)
                return GetTrianglesForTerrain(terrain, triangles, offset, localOffset);
            else
                return 0;
        }

        void FixupTriangleIndices(Triangle[] triangles, int offset, int localOffset, int numTriangles, int indexOffset)
        {
            for (var i = 0; i < numTriangles; ++i)
            {
                triangles[offset + localOffset + i].index0 += indexOffset;
                triangles[offset + localOffset + i].index1 += indexOffset;
                triangles[offset + localOffset + i].index2 += indexOffset;
            }
        }

        [Range(0, 10)]
        public int TerrainSimplificationLevel = 0;      // Simplification level for terrain mesh.
        public bool exportAllChildren;                  // Export all children with Mesh Filter. Children with Terrain are excluded.
    }
}