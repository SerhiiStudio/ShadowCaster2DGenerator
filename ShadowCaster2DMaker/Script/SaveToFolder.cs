// Copyright(c) 2025 SerhiiStudio
// See LICENSE file for details

using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;

namespace SerhiiStudio
{
	public static class SaveToFolder
	{
		private const string PREFAB_EXTENSION = ".prefab";

		private static void SavePrefabsToSubfolder(string path, string folderName, List<GameObject> gameObjects)
		{
			if (gameObjects != null && gameObjects.Count != 0)
			{
				int savedGOs = 0;
				int unsavedGOs = 0;

				string[] way1 = { path, folderName };
				string folderToSave = Path.Combine(way1);

				if (!AssetDatabase.IsValidFolder(folderToSave))
					AssetDatabase.CreateFolder(path, folderName);

				foreach (GameObject gameObject in gameObjects)
				{
					string[] way2 = { folderToSave, gameObject.name + PREFAB_EXTENSION };
					string fullPath = Path.Combine(way2);
					try
					{
						PrefabUtility.SaveAsPrefabAsset(gameObject, fullPath);
						savedGOs++;
					}
					catch (Exception e)
					{
						Debug.LogError(
							"Something went wrong" + "\n" +
							$"Error message: {e.Message}" +
							$"The exception was called while saving: {gameObject.name}");

						unsavedGOs++;
					}
				}
				Debug.Log("Prefabs saved to " + folderToSave + "in count of " + savedGOs);
				if (unsavedGOs > 0)
					Debug.LogWarning("Unsaved prefabs: " + unsavedGOs);
			}
			else
				Debug.LogError("The array of GameObjects to save is null or empty");
		}

		/// <summary>
		/// Saves a GameObject as .prefab to folder
		/// </summary>
		/// <param name="folder"></param>
		/// <param name="targetFolderName"></param>
		/// <param name="objects"></param>
		public static void Save(DefaultAsset folder, string targetFolderName, List<GameObject> objects)
		{
			string baseFolderPath = AssetDatabase.GetAssetPath(folder);
			if (ValidatePath(baseFolderPath, targetFolderName))
			{
				SavePrefabsToSubfolder(baseFolderPath, targetFolderName, objects);
			}
		}

		private static bool ValidatePath(string baseFolderPath, string targetFolderName)
		{
			if (AssetDatabase.IsValidFolder(baseFolderPath))
			{
				Debug.Log("Destination folder is valid");

				if (!string.IsNullOrWhiteSpace(targetFolderName))
				{
					Debug.Log("Target folder name is valid");
					return true;
				}
				else
				{
					Debug.LogError("Target folder name is null or invalid");
					return false;
				}
			}
			else
			{
				Debug.LogError("Folder is invalid");
				return false;
			}
		}
	}
}
#endif
