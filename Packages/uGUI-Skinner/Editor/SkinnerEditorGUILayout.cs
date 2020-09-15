using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// <see cref="EditorGUILayout"/>の拡張
	/// </summary>
	public static class SkinnerEditorGUILayout
	{

		#region UnityEngine.Object

		/// <summary>
		/// <see cref="EditorGUILayout.ObjectField"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="type">型</param>
		/// <param name="options">レイアウト設定</param>
		public static void ObjectField(GUIContent label, SerializedProperty property, System.Type type, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			bool isComponent = type == typeof(Component) || type.IsSubclassOf(typeof(Component));
			bool isGameObject = type == typeof(GameObject);
			var result = EditorGUILayout.ObjectField(label, property.objectReferenceValue, type, isComponent || isGameObject, options);
			if (result != property.objectReferenceValue)
			{
				property.objectReferenceValue = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		#endregion

		#region float

		/// <summary>
		/// <see cref="EditorGUILayout.FloatField"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void FloatField(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var result = EditorGUILayout.FloatField(label, property.floatValue, options);
			if (!Mathf.Approximately(result, property.floatValue))
			{
				property.floatValue = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		/// <summary>
		/// <see cref="EditorGUILayout.IntField"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void IntField(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var result = EditorGUILayout.IntField(label, property.floatValue.ToInt(), options);
			if (!Mathf.Approximately(result, property.floatValue))
			{
				property.floatValue = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		/// <summary>
		/// <see cref="EditorGUILayout.Toggle"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void Toggle(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var result = EditorGUILayout.Toggle(label, property.floatValue.ToBool(), options).ToFloat();
			if (!Mathf.Approximately(result, property.floatValue))
			{
				property.floatValue = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		/// <summary>
		/// <see cref="EditorGUILayout.EnumPopup"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="enumType">Enumの型</param>
		/// <param name="options">レイアウト設定</param>
		public static void EnumPopup(GUIContent label, SerializedProperty property, System.Type enumType, params GUILayoutOption[] options)
		{
			var popupDisplayName = enumType.GetEnumNames().Select(n => new GUIContent(n)).ToArray();
			var popupValue = enumType.GetEnumValues().Cast<int>().ToArray();
			IntPopup(label, property, popupDisplayName, popupValue, options);
		}

		/// <summary>
		/// <see cref="EditorGUILayout.IntPopup"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void IntPopup(GUIContent label, SerializedProperty property, GUIContent[] displayOptions, int[] optionValues, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var result = EditorGUILayout.IntPopup(label, property.floatValue.ToInt(), displayOptions, optionValues, options);
			if (!Mathf.Approximately(result, property.floatValue))
			{
				property.floatValue = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		#endregion

		#region vector4

		/// <summary>
		/// <see cref="EditorGUILayout.ColorField"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void ColorField(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			Vector4 result = EditorGUILayout.ColorField(label, property.vector4Value.ToColor(), options).ToVector();
			if (result != property.vector4Value)
			{
				property.vector4Value = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		/// <summary>
		/// <see cref="EditorGUILayout.Vector2Field"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void Vector2Field(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			Vector4 result = EditorGUILayout.Vector2Field(label, property.vector4Value, options);
			if (result != property.vector4Value)
			{
				property.vector4Value = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		/// <summary>
		/// <see cref="EditorGUILayout.Vector3Field"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void Vector3Field(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			Vector4 result = EditorGUILayout.Vector3Field(label, property.vector4Value, options);
			if (result != property.vector4Value)
			{
				property.vector4Value = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		/// <summary>
		/// <see cref="EditorGUILayout.Vector4Field"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void Vector4Field(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			Vector4 result = EditorGUILayout.Vector4Field(label, property.vector4Value, options);
			if (result != property.vector4Value)
			{
				property.vector4Value = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		#endregion

		#region string

		/// <summary>
		/// <see cref="EditorGUILayout.TextField"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void TextField(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var result = EditorGUILayout.TextField(label, property.stringValue, options);
			if (result != property.stringValue)
			{
				property.stringValue = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		/// <summary>
		/// <see cref="EditorGUILayout.TextField"/>をChar用モードで表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void CharField(GUIContent label, SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var str = property.stringValue;
			if (str.Length > 0) str = str[0].ToString();
			var resultStr = EditorGUILayout.TextField(label, str, options);
			if (resultStr.Length > 0) resultStr = resultStr[0].ToString();
			else resultStr = str;
			if (resultStr != property.stringValue)
			{
				property.stringValue = resultStr;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		#endregion

	}

}
