#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class FamilyTherapyPracticumBuilder
{
    private const string ScenePath = "Assets/Scenes/FamilyTherapyPracticum.unity";

    [MenuItem("Family Therapy Practicum/Create Main Scene")]
    public static void CreateMainScene()
    {
        Directory.CreateDirectory("Assets/Scenes");
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var root = new GameObject("Family Therapy Practicum App");
        root.AddComponent<FamilyTherapyPracticumGame>();
        EditorSceneManager.SaveScene(scene, ScenePath);
        var buildScene = new EditorBuildSettingsScene(ScenePath, true);
        EditorBuildSettings.scenes = new[] { buildScene };
        Debug.Log("Family Therapy Practicum scene created: " + ScenePath);
    }

    [MenuItem("Family Therapy Practicum/Build Windows")]
    public static void BuildWindows()
    {
        if (!File.Exists(ScenePath))
        {
            CreateMainScene();
        }

        PlayerSettings.defaultScreenWidth = 1600;
        PlayerSettings.defaultScreenHeight = 900;
        PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
        PlayerSettings.resizableWindow = false;

        Directory.CreateDirectory("Builds/Windows");
        var options = new BuildPlayerOptions
        {
            scenes = new[] { ScenePath },
            locationPathName = "Builds/Windows/FamilyTherapyPracticum.exe",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.CleanBuildCache
        };

        var report = BuildPipeline.BuildPlayer(options);
        Debug.Log("Family Therapy Practicum build result: " + report.summary.result);
    }
}
#endif
