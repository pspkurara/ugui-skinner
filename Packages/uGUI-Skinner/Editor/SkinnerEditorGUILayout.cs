using UnityEditor;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// <see cref="EditorGUILayout"/>の拡張
	/// </summary>
	public static class SkinnerEditorGUILayout
	{

		public static void Toggle(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			property.floatValue = Toggle(label, property.floatValue, options);
			EditorGUI.showMixedValue = showMixedValue;
		}

		public static float Toggle(GUIContent label, float value, params GUILayoutOption[] options)
		{
			return EditorGUILayout.Toggle(label, value.ToBool(), options).ToFloat();
		}

		public static void ColorField(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			property.vector4Value = ColorField(label, property.vector4Value.ToColor(), options).ToColor();
			EditorGUI.showMixedValue = showMixedValue;
		}

		public static Vector4 ColorField(GUIContent label, Vector4 vector, params GUILayoutOption[] options)
		{
			return EditorGUILayout.ColorField(label, vector.ToColor(), options).ToVector();
		}

	}

}
