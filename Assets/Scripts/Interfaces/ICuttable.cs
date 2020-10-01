using EzySlice;
using  UnityEngine;

interface ICuttable
{
    void SaveInitialMesh();
    void Cut(Vector3 planeWorldPosition, Vector3 planeWorldDirection);
    SlicedHull GetHull(Vector3 planeWorldPosition, Vector3 planeWorldDirection, TextureRegion textureRegion);
    void AddComponents(GameObject gameObject, Vector3 planeWorldDirection);
}