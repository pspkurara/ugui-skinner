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

		public static void ResetArray(SerializedProperty prop, int arraySize, bool isCorrect = true)
		{
			if (isCorrect && prop.arraySize != arraySize || !isCorrect && prop.arraySize < arraySize)
			{
				prop.ClearArray();
				for (int i = 0; i < arraySize; i++)
				{
					prop.InsertArrayElementAtIndex(prop.arraySize);
					var arrayObj = prop.GetArrayElementAtIndex(i);
					switch (arrayObj.propertyType)
					{
						case SerializedPropertyType.Color:
							arrayObj.colorValue = SkinDefaultValue.Color;
							break;
						case SerializedPropertyType.Float:
							arrayObj.floatValue = SkinDefaultValue.Float;
							break;
						case SerializedPropertyType.Integer:
							arrayObj.intValue = SkinDefaultValue.Integer;
							break;
						case SerializedPropertyType.Boolean:
							arrayObj.boolValue = SkinDefaultValue.Boolean;
							break;
						case SerializedPropertyType.ObjectReference:
							arrayObj.objectReferenceValue = SkinDefaultValue.Object;
							break;
					}
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

		public static void CleanArray(SerializedProperty prop, int arraySize = 0)
		{
			CleanArray<Object>(prop, arraySize);
		}

		public static void CleanArray<T>(SerializedProperty prop, int arraySize = 0) where T : Object
		{
			int listCount = prop.arraySize;
			for (int i = listCount; i < arraySize; i++)
			{
				prop.InsertArrayElementAtIndex(prop.arraySize);
				var arrayObj = prop.GetArrayElementAtIndex(i);
				switch (arrayObj.propertyType)
				{
					case SerializedPropertyType.Color:
						arrayObj.colorValue = SkinDefaultValue.Color;
						break;
					case SerializedPropertyType.Float:
						arrayObj.floatValue = SkinDefaultValue.Float;
						break;
					case SerializedPropertyType.Integer:
						arrayObj.intValue = SkinDefaultValue.Integer;
						break;
					case SerializedPropertyType.Boolean:
						arrayObj.boolValue = SkinDefaultValue.Boolean;
						break;
					case SerializedPropertyType.ObjectReference:
						arrayObj.objectReferenceValue = SkinDefaultValue.Object;
						break;
				}
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
							arrayObj.objectReferenceValue = SkinDefaultValue.Object;
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

		public static void CleanObject<T>(SerializedProperty prop, int index)
		{
			if (prop.GetArrayElementAtIndex(index).objectReferenceValue is T) return;
			prop.GetArrayElementAtIndex(index).objectReferenceValue = SkinDefaultValue.Object;
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
