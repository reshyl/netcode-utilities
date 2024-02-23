using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace ReshylEditor.NetcodeUtils
{
    public static class BuildHelper
    {
        [MenuItem("Build/Build Client")]
        public static void BuildClient()
        {
            var scenes = EditorBuildSettings.scenes.Select(s => s.path).ToArray();

            Build(scenes, "Build/Client Build/Client.exe", false, false);
        }

        [MenuItem("Build/Build Server")]
        public static void BuildServer()
        {
            var scenes = EditorBuildSettings.scenes.Select(s => s.path).ToArray();

            Build(scenes, "Build/Server Build/Server.exe", true, false);
        }

        [MenuItem("Build/Build and Run Client")]
        public static void BuildAndRunClient()
        {
            var scenes = EditorBuildSettings.scenes.Select(s => s.path).ToArray();

            Build(scenes, "Build/Client Build/Client.exe", false, true);
        }

        [MenuItem("Build/Build and Run Server")]
        public static void BuildAndRunServer()
        {
            var scenes = EditorBuildSettings.scenes.Select(s => s.path).ToArray();

            Build(scenes, "Build/Server Build/Server.exe", true, true);
        }

        [MenuItem("Build/Build Both")]
        public static void BuildBoth()
        {
            BuildServer();
            BuildClient();
        }

        [MenuItem("Build/Build and Run Both")]
        public static void BuildAndRunBoth()
        {
            BuildAndRunServer();
            BuildAndRunClient();
        }

        public static void Build(string[] scenes, string path, bool server, bool run)
        {
            var buildPlayerOptions = new BuildPlayerOptions();

            buildPlayerOptions.scenes = scenes;
            buildPlayerOptions.locationPathName = path;
            buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
            buildPlayerOptions.subtarget = (int)(server ? StandaloneBuildSubtarget.Server : StandaloneBuildSubtarget.Player);
            buildPlayerOptions.options = run ? BuildOptions.AutoRunPlayer : BuildOptions.None;

            var report = BuildPipeline.BuildPlayer(buildPlayerOptions);

            if (report.summary.result == BuildResult.Succeeded)
                Debug.Log("Build to '" + path + "' succeeded: " + report.summary.totalSize + " bytes");

            if (report.summary.result == BuildResult.Failed)
                Debug.Log("Build failed");
        }
    }
}
