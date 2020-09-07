using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;
using System.Text.RegularExpressions;
using System.Text;

namespace Pspkurara.UI.Skinner
{

	public static class SkinnerEditorUtility
	{

		#region TempField

		public static readonly Dictionary<Type, ComponentInfos> componentInfos = new Dictionary<Type, ComponentInfos>();

		public sealed class ComponentInfos
		{
			public bool isComponent = false;
			public bool allowMultiplyComponent = false;
		}

		public static ComponentInfos GetComponentInfos(Type type)
		{
			if (!componentInfos.ContainsKey(type))
			{
				ComponentInfos cInfo = new ComponentInfos();
				cInfo.isComponent = type.IsSubclassOf(typeof(Component));
				if (cInfo.isComponent) { cInfo.allowMultiplyComponent = !System.Attribute.IsDefined(type, typeof(DisallowMultipleComponent), true); }
				componentInfos.Add(type, cInfo);
			}
			return componentInfos[type];
		}

		#endregion

		private static void FieldClean(SerializedProperty arrayObj, object defaultValue)
		{
			bool hasDefaultValue = defaultValue != null;
			switch (arrayObj.propertyType)
			{
				case SerializedPropertyType.Color:
					arrayObj.colorValue = hasDefaultValue ? (Color)defaultValue : SkinDefaultValue.Color;
					break;
				case SerializedPropertyType.Float:
					arrayObj.floatValue = hasDefaultValue ? (float)defaultValue : SkinDefaultValue.Float;
					break;
				case SerializedPropertyType.Integer:
					arrayObj.intValue = hasDefaultValue ? (int)defaultValue : SkinDefaultValue.Integer;
					break;
				case SerializedPropertyType.Boolean:
					arrayObj.boolValue = hasDefaultValue ? (bool)defaultValue : SkinDefaultValue.Boolean;
					break;
				case SerializedPropertyType.Vector4:
					Vector4 convertedVector4 = SkinDefaultValue.Vector4;
					{
						var type = defaultValue.GetType();
						if (type == typeof(Vector2))
						{
							convertedVector4 = (Vector4)(Vector2)defaultValue;
						}
						if (type == typeof(Vector3))
						{
							convertedVector4 = (Vector4)(Vector3)defaultValue;
						}
						if (type == typeof(Vector4))
						{
							convertedVector4 = (Vector4)defaultValue;
						}
					}
					arrayObj.vector4Value = hasDefaultValue ? convertedVector4 : SkinDefaultValue.Vector4;
					break;
				case SerializedPropertyType.ObjectReference:
					arrayObj.objectReferenceValue = hasDefaultValue ? (Object)defaultValue : SkinDefaultValue.Object;
					break;
			}
		}

		public static void ResetArray(SerializedProperty prop, int arraySize, bool isCorrect = true, object defaultValue = null)
		{
			if (isCorrect && prop.arraySize != arraySize || !isCorrect && prop.arraySize < arraySize)
			{
				prop.ClearArray();
				for (int i = 0; i < arraySize; i++)
				{
					prop.InsertArrayElementAtIndex(prop.arraySize);
					var arrayObj = prop.GetArrayElementAtIndex(i);
					FieldClean(arrayObj, defaultValue);
				}
			}
		}


		#region EditorFunctions

		public static bool DrawAddButton(GUIContent title, Action function)
		{
			Color guiColor = GUI.color;
			GUI.color = EditorConst.AddButtonColor;
			if (GUILayout.Button(title, EditorConst.SkinAddOrRemoveButtonMaxWidth))
			{
				function();
				return true;
			}
			GUI.color = guiColor;
			return false;
		}

		public static bool DrawRemoveButton(GUIContent title, Action function)
		{
			Color guiColor = GUI.color;
			GUI.color = EditorConst.RemoveButtonColor;
			if (GUILayout.Button(title, EditorConst.SkinAddOrRemoveButtonMaxWidth))
			{
				function();
				return true;
			}
			GUI.color = guiColor;
			return false;
		}

		public static bool DrawCleanupButton(GUIContent title, Action function)
		{
			Color guiColor = GUI.color;
			GUI.color = EditorConst.CleanupButtonColor;
			if (GUILayout.Button(title, EditorConst.SkinAddOrRemoveButtonMaxWidth))
			{
				function();
				return true;
			}
			GUI.color = guiColor;
			return false;
		}

		#endregion

		public static void CleanArray(SerializedProperty prop, int arraySize = 0, object defaultValue = null)
		{
			CleanArray<Object>(prop, arraySize, defaultValue);
		}

		public static void CleanArray<T>(SerializedProperty prop, int arraySize = 0, object defaultValue = null) where T : Object
		{
			int listCount = prop.arraySize;
			for (int i = listCount; i < arraySize; i++)
			{
				prop.InsertArrayElementAtIndex(prop.arraySize);
				var arrayObj = prop.GetArrayElementAtIndex(i);
				FieldClean(arrayObj, defaultValue);
			}
			int currentArraySize = prop.arraySize;
			for (int i = currentArraySize - 1; i >= 0; i--)
			{
				var arrayObj = prop.GetArrayElementAtIndex(i);
				switch (arrayObj.propertyType)
				{
					case SerializedPropertyType.ObjectReference:
						{
							if (arrayObj.objectReferenceValue is T) break;
							arrayObj.objectReferenceValue = (Object)defaultValue;
						}
						break;
				}
				if (i >= arraySize)
				{
					prop.DeleteArrayElementAtIndex(i);
					continue;
				}
			}
		}

		public static void CleanObjectReferenceArrayWithFlexibleSize<T>(SerializedProperty prop, int minArraySize = 1, T defaultValue = null) where T : Object
		{
			HashSet<Object> cleanupCheckedObjects = new HashSet<Object>();
			int currentArraySize = prop.arraySize;
			for (int i = currentArraySize - 1; i >= 0; i--)
			{
				var arrayObj = prop.GetArrayElementAtIndex(i);
				bool removeArray = false;
				if (cleanupCheckedObjects.Contains(arrayObj.objectReferenceValue))
				{
					removeArray = true;
				}
				if (arrayObj.objectReferenceValue == null || !arrayObj.objectReferenceValue is T)
				{
					removeArray = i >= minArraySize;
				}
				cleanupCheckedObjects.Add(arrayObj.objectReferenceValue);
				if (removeArray)
				{
					arrayObj.objectReferenceValue = null;
					prop.DeleteArrayElementAtIndex(i);
					continue;
				}
			}
			int listCount = prop.arraySize;
			for (int i = listCount; i < minArraySize; i++)
			{
				prop.InsertArrayElementAtIndex(prop.arraySize);
				var arrayObj = prop.GetArrayElementAtIndex(i);
				FieldClean(arrayObj, defaultValue);
			}
		}

		public static void CleanObject<T>(SerializedProperty prop, int index, T defaultValue = null) where T : Object
		{
			if (prop.GetArrayElementAtIndex(index).objectReferenceValue is T) return;
			prop.GetArrayElementAtIndex(index).objectReferenceValue = defaultValue ? defaultValue : SkinDefaultValue.Object;
		}

		public static void DrawLine()
		{
			GUILayout.Box(string.Empty, EditorConst.LineBoxStyle);
		}

		public static string GetEditorName(string name)
		{
			var m = Regex.Matches(name, "[\\x41-\\x5a]+[\\x61-\\x7a]*");
			StringBuilder builder = new StringBuilder();
			for (int i = 0; i < m.Count; i++)
			{
				builder.Append(m[i].Value);
				builder.Append(" ");
			}
			return builder.ToString();
		}

	}

}
