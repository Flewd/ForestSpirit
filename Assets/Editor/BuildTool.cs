#if UNITY_EDITOR
//using UnityEditor.AddressableAssets.Settings;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditor.AddressableAssets;
using System.Diagnostics;

public class BuildTool
{
    private static string _GAME_NAME = "Iku";

    [MenuItem("Build/Build All Platforms")]
    public static void BuildGame()
    {
        Stopwatch watch = new Stopwatch();
        watch.Reset();
        watch.Start();

        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64)
        {
            buildWindows();

            UnityEngine.Debug.Log("switch to mac platform");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
            UnityEngine.Debug.Log("switch to mac platform complete");

            buildMac();

            UnityEngine.Debug.Log("switch to linux platform");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64);
            UnityEngine.Debug.Log("switch to linux platform complete");

            buildLinux();
        }
        else if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneOSX)
        {
            buildMac();

            UnityEngine.Debug.Log("switch to linux platform");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64);
            UnityEngine.Debug.Log("switch to linux platform complete");

            buildLinux();

            UnityEngine.Debug.Log("Switch to windows");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
            UnityEngine.Debug.Log("Switch to windows complete");

            buildWindows();
        }
        else
        {
            buildLinux();

            UnityEngine.Debug.Log("switch to mac platform");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
            UnityEngine.Debug.Log("switch to mac platform complete");

            buildMac();

            UnityEngine.Debug.Log("Switch to windows");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
            UnityEngine.Debug.Log("Switch to windows complete");

            buildWindows();
        }

        watch.Stop();
        UnityEngine.Debug.Log("Build finished in " + watch.Elapsed.TotalMinutes + " minutes");
    }


    [MenuItem("Build/Build Windows")]
    public static void BuildWindows()
    {
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneWindows64)
        {
            UnityEngine.Debug.Log("Switch to windows");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneWindows);
            UnityEngine.Debug.Log("Switch to windows complete");
        }
        buildWindows();
    }

    [MenuItem("Build/Build Mac")]
    public static void BuildMac()
    {
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneOSX)
        {
            UnityEngine.Debug.Log("switch to mac platform");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneOSX);
            UnityEngine.Debug.Log("switch to mac platform complete");
        }
        buildMac();
    }

    [MenuItem("Build/Build Linux")]
    public static void BuildLinux()
    {
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneLinux64)
        {
            UnityEngine.Debug.Log("switch to linux platform");
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTargetGroup.Standalone, BuildTarget.StandaloneLinux64);
            UnityEngine.Debug.Log("switch to linux platform complete");
        }
        buildLinux();
    }

    private static void buildWindows()
    {
        System.DateTime dateTime = System.DateTime.Now;
        string dateStamp = dateTime.Month + "." + dateTime.Day + "." + dateTime.Year;

  //      UnityEngine.Debug.Log("BuildAddressablesProcessor.PreExport start");
  //       AddressableAssetSettings.CleanPlayerContent(AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder);
  //       AddressableAssetSettings.BuildPlayerContent();
  //       UnityEngine.Debug.Log("BuildAddressablesProcessor.PreExport done");

        // Build player.
        UnityEngine.Debug.Log("build windows");

        BuildPipeline.BuildPlayer(
            GetAllScenes(),
            "builds/windows/" + _GAME_NAME + "." + dateStamp + "/" + _GAME_NAME + ".exe",
            BuildTarget.StandaloneWindows64,
            BuildOptions.None);


        UnityEngine.Debug.Log("build windows complete");

        UnityEngine.Debug.Log("Cleaning Player Content After Build");
  //      AddressableAssetSettings.CleanPlayerContent(AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder);
    }

    private static void buildMac()
    {
        System.DateTime dateTime = System.DateTime.Now;
        string dateStamp = dateTime.Month + "." + dateTime.Day + "." + dateTime.Year;

 //       UnityEngine.Debug.Log("BuildAddressablesProcessor.PreExport start");
 //       AddressableAssetSettings.CleanPlayerContent(AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder);
  //      AddressableAssetSettings.BuildPlayerContent();
 //       UnityEngine.Debug.Log("BuildAddressablesProcessor.PreExport done");


        UnityEngine.Debug.Log("build mac");

        BuildPipeline.BuildPlayer(
            GetAllScenes(),
            "builds/mac/" + _GAME_NAME + "." + dateStamp + "/" + _GAME_NAME + ".app",
            BuildTarget.StandaloneOSX,
            BuildOptions.None);

        UnityEngine.Debug.Log("build mac complete");
        UnityEngine.Debug.Log("Cleaning Player Content After Build");
  //      AddressableAssetSettings.CleanPlayerContent(AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder);
    }

    private static void buildLinux()
    {
        System.DateTime dateTime = System.DateTime.Now;
        string dateStamp = dateTime.Month + "." + dateTime.Day + "." + dateTime.Year;

    //    UnityEngine.Debug.Log("BuildAddressablesProcessor.PreExport start");
   //     AddressableAssetSettings.CleanPlayerContent(AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder);
   //     AddressableAssetSettings.BuildPlayerContent();
   //     UnityEngine.Debug.Log("BuildAddressablesProcessor.PreExport done");


        UnityEngine.Debug.Log("build linux");

        BuildPipeline.BuildPlayer(
            GetAllScenes(),
            "builds/linux/" + _GAME_NAME + "." + dateStamp + "/" + _GAME_NAME + ".x86_64",
            BuildTarget.StandaloneLinux64,
            BuildOptions.None);

        UnityEngine.Debug.Log("build linux complete");
        UnityEngine.Debug.Log("Cleaning Player Content After Build");
  //      AddressableAssetSettings.CleanPlayerContent(AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder);
    }

    public static string[] GetAllScenes()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

        List<string> newScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in scenes)
        {
            if (scene.enabled)
            {
                newScenes.Add(scene.path);
            }
        }
        return newScenes.ToArray();
    }
}

#endif