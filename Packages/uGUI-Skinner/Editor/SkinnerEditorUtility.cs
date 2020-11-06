using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using Object = UnityEngine.Object;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Pspkurara.UI.Skinner
{

	public static class SkinnerEditorUtility
	{

		#region TempField

		/// <summary>
		/// 複数アタッチ不可能なコンポーネント類
		/// </summary>
		private static readonly List<Type> specificSingleComponents = new List<Type>(new Type[]
		{
			typeof(Transform),
			typeof(CanvasGroup),
			typeof(CanvasRenderer),
			typeof(Canvas),
		});

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
				cInfo.isComponent = type == typeof(Component) || type.IsSubclassOf(typeof(Component));
				if (cInfo.isComponent)
				{
					// 複数不可能なコンポーネントはあらかじめ指定されている
					if (specificSingleComponents.Exists(t => { return t == type || type.IsSubclassOf(t); }))
					{
						cInfo.allowMultiplyComponent = false;
					}
					else
					{
						cInfo.allowMultiplyComponent = IsDefinedDisallowMultiplyComponent(type);
					}
				}
				componentInfos.Add(type, cInfo);
			}
			return componentInfos[type];
		}

		/// <summary>
		/// 指定したコンポーネントタイプが<see cref="DisallowMultipleComponent"/>属性を持つか取得する
		/// </summary>
		/// <param name="type">調べたいコンポーネントの型</param>
		/// <returns>
		/// 自身か何らかの親クラスに属性を持つ場合は真
		/// </returns>
		private static bool IsDefinedDisallowMultiplyComponent(Type type)
		{
			if (type == null) return false;

			if (type.IsDefined(typeof(DisallowMultipleComponent)))
			{
				return true;
			}

			return IsDefinedDisallowMultiplyComponent(type.BaseType);
		}

		#endregion

		private static void FieldClean(SerializedProperty arrayObj, object defaultValue)
		{
			bool hasDefaultValue = defaultValue != null;
			switch (arrayObj.propertyType)
			{
				case SerializedPropertyType.Float:
					float convertedFloat = SkinDefaultValue.Float;
					if (hasDefaultValue)
					{
						var type = defaultValue.GetType();
						if (type == typeof(float))
						{
							convertedFloat = (float)defaultValue;
						}
						if (type == typeof(int))
						{
							convertedFloat = (int)defaultValue;
						}
						if (type == typeof(bool))
						{
							convertedFloat = ((bool)defaultValue).ToFloat();
						}
					}
					arrayObj.floatValue = convertedFloat;
					break;
				case SerializedPropertyType.Vector4:
					Vector4 convertedVector4 = SkinDefaultValue.Vector4;
					if (hasDefaultValue)
					{
						var type = defaultValue.GetType();
						if (type == typeof(Color))
						{
							convertedVector4 = ((Color)defaultValue).ToVector();
						}
						if (type == typeof(Color32))
						{
							convertedVector4 = ((Color32)defaultValue).ToVector();
						}
						if (type == typeof(Vector2))
						{
							convertedVector4 = (Vector2)defaultValue;
						}
						if (type == typeof(Vector3))
						{
							convertedVector4 = (Vector3)defaultValue;
						}
						if (type == typeof(Vector4))
						{
							convertedVector4 = (Vector4)defaultValue;
						}
					}
					arrayObj.vector4Value = convertedVector4;
					break;
				case SerializedPropertyType.String:
					string convertedString = SkinDefaultValue.String;
					if (hasDefaultValue)
					{
						var type = defaultValue.GetType();
						if (type == typeof(char))
						{
							convertedString = ((char)defaultValue).ToString();
						}
						if (type == typeof(string))
						{
							convertedString = (string)defaultValue;
						}
					}
					arrayObj.stringValue = convertedString;
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

		public static void ResetArray(SerializedProperty prop, int arraySize, object defaultValue)
		{
			ResetArray(prop, arraySize, true, defaultValue);
		}


		#region EditorFunctions

		public static bool DrawAddButton(GUIContent title, Action function)
		{
			Color guiColor = GUI.color;
			GUI.color = EditorConst.AddButtonColor;
			var rect = EditorGUILayout.GetControlRect(EditorConst.SkinAddOrRemoveButtonMaxWidth);
			rect = EditorGUI.IndentedRect(rect);
			if (GUI.Button(rect, title, EditorStyles.miniButton))
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
			var rect = EditorGUILayout.GetControlRect(EditorConst.SkinAddOrRemoveButtonMaxWidth);
			rect = EditorGUI.IndentedRect(rect);
			if (GUI.Button(rect, title, EditorStyles.miniButton))
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
			var rect = EditorGUILayout.GetControlRect(EditorConst.SkinAddOrRemoveButtonMaxWidth);
			rect = EditorGUI.IndentedRect(rect);
			if (GUI.Button(rect, title, EditorStyles.miniButton))
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
			StripArray<T>(prop, arraySize, defaultValue);
		}

		public static void StripArray<T>(SerializedProperty prop, int arraySize = 0, object defaultValue = null) where T : Object
		{
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
			currentArraySize = prop.arraySize;
			for (int i = currentArraySize; i < arraySize; i++)
			{
				prop.InsertArrayElementAtIndex(i);
				FieldClean(prop.GetArrayElementAtIndex(i), defaultValue);
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
				else
				{
					cleanupCheckedObjects.Add(arrayObj.objectReferenceValue);
				}

				if (arrayObj.objectReferenceValue == null)
				{
					removeArray = true;
				}
				if (!(arrayObj.objectReferenceValue is T))
				{
					removeArray = true;
				}
				if (removeArray)
				{
					arrayObj.objectReferenceValue = null;
					prop.DeleteArrayElementAtIndex(i);
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

		public static void CleanObject(SerializedProperty prop, Type objectType, int index, Object defaultValue = null)
		{
			var objectReference = prop.GetArrayElementAtIndex(index).objectReferenceValue;
			if (objectReference != null &&(
				objectReference.GetType() == objectType ||
				objectType.IsSubclassOf(objectReference.GetType()))) return;
			prop.GetArrayElementAtIndex(index).objectReferenceValue = defaultValue ? defaultValue : SkinDefaultValue.Object;
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

		/// <summary>
		/// <see cref="EditorGUI.IntPopup"/>等で使う表示名や値をEnumから取得する
		/// </summary>
		/// <param name="enumType">Enumの型</param>
		/// <param name="displayOptions">表示名配列</param>
		/// <param name="optionValues">値配列</param>
		public static void GetPopupOptionsFromEnum(Type enumType, out GUIContent[] displayOptions, out int[] optionValues)
		{
			displayOptions = enumType.GetEnumNames().Select(n => new GUIContent(GetEditorName(n))).ToArray();
			optionValues = enumType.GetEnumValues().Cast<int>().ToArray();
		}

		/// <summary>
		/// <see cref="EditorSkinPartsPropertry">を<see cref="SkinPartsPropertry"/>にマップする
		/// </summary>
		/// <param name="mapTarget">マップ対象となるオブジェクト</param>
		/// <param name="mapSource">マップ元となるオブジェクト</param>
		public static void MapRuntimePropertyFromEditorProperty(SkinPartsPropertry mapTarget, EditorSkinPartsPropertry mapSource)
		{
			mapTarget.Clear();
			MapRuntimeFromEditorSingleProperty(mapTarget.objectReferenceValues, mapSource.objectReferenceValues, (p) => p.objectReferenceValue);
			MapRuntimeFromEditorSingleProperty(mapTarget.floatValues, mapSource.floatValues, (p) => p.floatValue);
			MapRuntimeFromEditorSingleProperty(mapTarget.vector4Values, mapSource.vector4Values, (p) => p.vector4Value);
			MapRuntimeFromEditorSingleProperty(mapTarget.stringValues, mapSource.stringValues, (p) => p.stringValue);
		}

		/// <summary>
		/// <see cref="SkinPartsPropertry">を<see cref="EditorSkinPartsPropertry"/>にマップする
		/// </summary>
		/// <param name="mapTarget">マップ対象となるオブジェクト</param>
		/// <param name="mapSource">マップ元となるオブジェクト</param>
		public static void MapRuntimePropertyFromEditorProperty(EditorSkinPartsPropertry mapTarget, SkinPartsPropertry mapSource)
		{
			MapEditorFromRuntimeSingleProperty(mapTarget.objectReferenceValues, mapSource.objectReferenceValues, (v, p) => p.objectReferenceValue = v);
			MapEditorFromRuntimeSingleProperty(mapTarget.floatValues, mapSource.floatValues, (v, p) => p.floatValue = v);
			MapEditorFromRuntimeSingleProperty(mapTarget.vector4Values, mapSource.vector4Values, (v, p) => p.vector4Value = v);
			MapEditorFromRuntimeSingleProperty(mapTarget.stringValues, mapSource.stringValues, (v, p) => p.stringValue = v);
		}

		private static void MapRuntimeFromEditorSingleProperty<T>(List<T> mapTarget, SerializedProperty mapSource, Func<SerializedProperty, T> convertFunction)
		{
			for (int i = 0; i < mapSource.arraySize; i++)
			{
				var element = mapSource.GetArrayElementAtIndex(i);
				mapTarget.Add(convertFunction(element));
			}
		}

		private static void MapEditorFromRuntimeSingleProperty<T>(SerializedProperty mapTarget, List<T> mapSource, Action<T, SerializedProperty> mapFunction)
		{
			int mapTargetArraySize = mapTarget.arraySize;
			for (int i = mapTargetArraySize; i < mapSource.Count; i++)
			{
				mapTarget.InsertArrayElementAtIndex(mapTarget.arraySize);
			}
			for (int i = 0; i < mapSource.Count; i++)
			{
				var element = mapTarget.GetArrayElementAtIndex(i);
				mapFunction(mapSource[i], element);
			}
		}

	}

}
