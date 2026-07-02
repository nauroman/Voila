using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Voila.Editor
{
    public static class BuildVoila
    {
        private const string MainScenePath = "Assets/Voila/Scenes/Main.unity";
        private const string WindowsBuildPath = "Builds/Windows/Voila.exe";

        [MenuItem("Voila/Build/Windows")]
        public static void BuildWindows()
        {
            string outputPath = Path.GetFullPath(Path.Combine(Application.dataPath, "..", WindowsBuildPath));
            string outputDirectory = Path.GetDirectoryName(outputPath);

            if (string.IsNullOrEmpty(outputDirectory))
            {
                throw new BuildFailedException($"Invalid build output path: {outputPath}");
            }

            Directory.CreateDirectory(outputDirectory);

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = GetBuildScenes(),
                locationPathName = outputPath,
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result != BuildResult.Succeeded)
            {
                throw new BuildFailedException(
                    $"Windows build failed: {summary.result} with {summary.totalErrors} errors.");
            }

            Debug.Log($"Windows build completed: {summary.outputPath}");
        }

        private static string[] GetBuildScenes()
        {
            string[] enabledScenes = EditorBuildSettings.scenes
                .Where(scene => scene.enabled)
                .Select(scene => scene.path)
                .ToArray();

            return enabledScenes.Length > 0 ? enabledScenes : new[] { MainScenePath };
        }
    }
}
