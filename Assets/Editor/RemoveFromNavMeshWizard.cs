using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class RemoveFromNavMeshWizard : ScriptableWizard
{
    [MenuItem("My Tools/Mass NavMesh Settings")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<RemoveFromNavMeshWizard>("NavMesh Settings", "Add Everything",
            "Remove Everything");
    }

    private void OnWizardCreate()
    {
        RelevantGameObjects().ForEach(gameObject =>
        {
            var flags = GameObjectUtility.GetStaticEditorFlags(gameObject);
            flags |= ~StaticEditorFlags.NavigationStatic;
            GameObjectUtility.SetStaticEditorFlags(gameObject, flags);
        });
    }

    private void OnWizardOtherButton()
    {
        RelevantGameObjects().ForEach(gameObject =>
        {
            var flags = GameObjectUtility.GetStaticEditorFlags(gameObject);
            flags &= ~StaticEditorFlags.NavigationStatic;
            GameObjectUtility.SetStaticEditorFlags(gameObject, flags);
        });
    }

    private List<GameObject> RelevantGameObjects()
    {
        var meshRenderers = GameObjectsWithMeshRenderer();
        var terrains = TerrainGameObjects();
        meshRenderers.AddRange(terrains);

        return meshRenderers;
    }

    private List<GameObject> GameObjectsWithMeshRenderer()
    {
        return FindObjectsOfType<MeshRenderer>().Select(renderer => renderer.gameObject).ToList();
    }
    
    private List<GameObject> TerrainGameObjects()
    {
        return FindObjectsOfType<Terrain>().Select(terrain => terrain.gameObject).ToList();
    }
}
