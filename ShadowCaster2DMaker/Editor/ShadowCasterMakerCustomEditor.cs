// Copyright(c) 2025 SerhiiStudio
// See LICENSE file for details

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SerhiiStudio
{
	[CustomEditor(typeof(ShadowCaster2DMaker))]
	public class ShadowCasterMakerCustomEditor : Editor
	{
		private bool showAdvanced = false;

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();

			EditorGUILayout.Space(20);
			EditorGUILayout.LabelField("Shadow Generator Tool", EditorStyles.boldLabel);

			if (GUILayout.Button("Generate ShadowCaster"))
			{
				var targetScript = (ShadowCaster2DMaker)target;
				targetScript.GenerateShadows();
			}
			if (GUILayout.Button("Clear instantiated game objects")) 
			{
				var targetScript = (ShadowCaster2DMaker)target;
				targetScript.ClearMadeGameObjects();
			}
			EditorGUILayout.HelpBox("You should clear game objects only if \"doNotSaveToFolder\" is enabled", MessageType.Info);

			Advanced();

			serializedObject.ApplyModifiedProperties();
		}


		private void Advanced()
		{
			showAdvanced = EditorGUILayout.Foldout(showAdvanced, "Advanced");

			if (showAdvanced)
			{
				EditorGUI.indentLevel++;
				EditorGUILayout.PropertyField(serializedObject.FindProperty("doNotSaveToFolder"));
				// TODO: Add other advanced options here if needed.
				EditorGUI.indentLevel--;
			}
		}
	}
}
#endif
