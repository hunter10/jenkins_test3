using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyEditorScript
{
    static string[] SCENES = FindEnableEditorScenes();

    static string gameCode = "JenkinsTest3";
    static string TARGET_DIR = "0Bin";
    static string APP_NAME = "/{0}.apk";

    [MenuItem ("Custom/TestBuild")]
    static void TestBuild()
    {
        //string BUILD_TARGET_PATH = TARGET_DIR + string.Format(APP_NAME, gameCode);
        //Debug.Log("BUILD_TARGET_PATH : " + BUILD_TARGET_PATH);


        string BUILD_TARGET_PATH = gameCode + ".apk";
        GenericBuild(SCENES, BUILD_TARGET_PATH, BuildTarget.Android, BuildOptions.None);
    }


    private static string[] FindEnableEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }

        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
        string res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
        if(res.Length > 0)
        {
            throw new System.Exception("BuildPlayer failure: " + res);
        }
    }
}


