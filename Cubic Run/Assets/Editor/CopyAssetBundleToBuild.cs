
// Example script to copy asset bundles to StreamingAssets
using UnityEditor;
using UnityEngine;

public class CopyAssetBundleToBuild
{
    [MenuItem("Assets/Copy AssetBundles to StreamingAssets")]
    static void CopyBundles()
    {
        string sourcePath = "Assets/AssetBundles";
        string destinationPath = Application.streamingAssetsPath;

        if (!System.IO.Directory.Exists(destinationPath))
        {
            System.IO.Directory.CreateDirectory(destinationPath);
        }

        foreach (var file in System.IO.Directory.GetFiles(sourcePath))
        {
            var fileName = System.IO.Path.GetFileName(file);
            var destFile = System.IO.Path.Combine(destinationPath, fileName);
            System.IO.File.Copy(file, destFile, true);
        }

        Debug.Log("Asset bundles copied to StreamingAssets.");
        Debug.Log(destinationPath);
    }
}

