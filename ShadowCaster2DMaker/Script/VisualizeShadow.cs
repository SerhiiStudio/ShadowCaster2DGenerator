// Copyright(c) 2025 SerhiiStudio
// See LICENSE file for details

using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace SerhiiStudio
{
	public class VisualizeShadow : MonoBehaviour
	{
		[SerializeField] private ShadowCaster2D shadowCaster;
		private Vector3[] path;
		private void OnDrawGizmos()
		{
			if (shadowCaster != null)
			{
				var casterFieldInfo = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.Instance | BindingFlags.NonPublic);
				if (casterFieldInfo == null) { return; }

				path = casterFieldInfo.GetValue(shadowCaster) as Vector3[];
				if (path == null || path.Length < 2) { return; }

				Gizmos.color = Color.grey;

				DrawLines();
			}
		}

		private void DrawLines()
		{
			for (int i = 0; i < path.Length; i++)
			{
				Vector3 currentPoint = shadowCaster.transform.TransformPoint(path[i]);
				Vector3 nextPoint = shadowCaster.transform.TransformPoint(path[(i + 1) % path.Length]);
				Gizmos.DrawLine(currentPoint, nextPoint);
			}
		}
	}
}