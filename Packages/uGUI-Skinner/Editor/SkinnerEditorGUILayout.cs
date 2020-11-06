using System.Linq;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

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
		/// 重複アタッチ可能なコンポーネントの場合はアタッチ順を元に切り替え可能な<see cref="EditorGUILayout.IntField"/>を併せて描画
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

			// コンポーネントの情報を確認し、複数アタッチできるかチェック
			bool isGameObject = type == typeof(GameObject);
			var componentInfo = SkinnerEditorUtility.GetComponentInfos(type);
			bool showComponentIndex = componentInfo.isComponent && componentInfo.allowMultiplyComponent;

			var rect = EditorGUILayout.GetControlRect(options);

			var objectFieldRect = rect;

			// コンポーネントインデックス表示の場合はオブジェクトフィールドがその分縮む
			if (showComponentIndex)
			{
				objectFieldRect.width -= EditorConst.ComponentIndexFieldWidth + EditorGUIUtility.standardVerticalSpacing;
			}

			var result = EditorGUI.ObjectField(objectFieldRect, label, property.objectReferenceValue, type, componentInfo.isComponent || isGameObject);

			// コンポーネントインデックス表示が有効
			if (showComponentIndex)
			{
				// 1度キャストして本当にコンポーネントがアタッチされているか検証
				Component castedResult = result as Component;
				int componentIndex = -1;
				List<Component> componentList = null;
				// null以外 & 指定された型の継承クラスかを調べる
				if (result != null && (result.GetType() == type || type.IsSubclassOf(result.GetType())))
				{
					// 同じオブジェクトから全ての同一の型のコンポーネントを取得
					var castedResultGameObject = (castedResult as Component).gameObject;
					componentList = castedResultGameObject
						.GetComponentsInChildren(type, true)
						.Where(component => component.gameObject == castedResultGameObject)
						.ToList();
					// 順番を取得 (アタッチされた順番が配列の順番となる)
					componentIndex = componentList.IndexOf(castedResult);
				}

				bool guiEnabled = GUI.enabled;

				// 不正なアタッチ (見つからない場合) だったらフィールドは非アクティブ
				if (componentIndex < 0)
				{
					componentIndex = 0;
					GUI.enabled = false;
				}

				int indent = EditorGUI.indentLevel;
				EditorGUI.indentLevel = 0;

				var indexRect = new Rect(rect.xMax - EditorConst.ComponentIndexFieldWidth, rect.y, EditorConst.ComponentIndexFieldWidth, rect.height);

				int editIndex = EditorGUI.IntField(indexRect, GUIContent.none, componentIndex);
				if (editIndex != componentIndex)
				{
					// 設定された数字を切り替えたら同一オブジェクトのコンポーネントに差し替える
					editIndex = Mathf.Clamp(editIndex, 0, componentList.Count - 1);
					result = componentList[editIndex];
				}

				EditorGUI.indentLevel = indent;

				GUI.enabled = guiEnabled;
			}

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
		/// <see cref="EditorGUILayout.Slider"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="leftValue">左の値</param>
		/// <param name="rightValue">右の値</param>
		/// <param name="options">レイアウト設定</param>
		public static void Slider(GUIContent label, SerializedProperty property, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var result = EditorGUILayout.Slider(label, property.floatValue, leftValue, rightValue, options);
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
		/// <see cref="EditorGUILayout.IntSlider"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="leftValue">左の値</param>
		/// <param name="rightValue">右の値</param>
		/// <param name="options">レイアウト設定</param>
		public static void IntSlider(GUIContent label, SerializedProperty property, int leftValue, int rightValue, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var result = EditorGUILayout.IntSlider(label, property.floatValue.ToInt(), leftValue, rightValue, options);
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
			string[] popupDisplayName;
			int[] popupValue;
			SkinnerEditorUtility.GetPopupOptionsFromEnum(enumType, out popupDisplayName, out popupValue);
			IntPopup(label, property, popupDisplayName, popupValue, options);
		}

		/// <summary>
		/// <see cref="EditorGUILayout.IntPopup"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void IntPopup(GUIContent label, SerializedProperty property, string[] displayOptions, int[] optionValues, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var result = EditorGUILayout.IntPopup(label.text, property.floatValue.ToInt(), displayOptions, optionValues, options);
			if (!Mathf.Approximately(result, property.floatValue))
			{
				property.floatValue = result;
			}
			EditorGUI.showMixedValue = showMixedValue;
		}

		/// <summary>
		/// <see cref="EditorGUILayout.MaskField"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="displayOptions">表示するテキスト類</param>
		/// <param name="options">レイアウト設定</param>
		public static void MaskField(GUIContent label, SerializedProperty property, string[] displayOptions, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var result = EditorGUILayout.MaskField(label, property.floatValue.ToInt(), displayOptions, options);
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
			ColorField(label, property, false, true, false, options);
		}

		/// <summary>
		/// <see cref="EditorGUILayout.ColorField"/>を表示
		/// </summary>
		/// <param name="label">ラベル</param>
		/// <param name="property">プロパティ</param>
		/// <param name="showEyedropper">Eyedropperの表示を有効</param>
		/// <param name="showAlpha">アルファの表示を有効</param>
		/// <param name="hdr">HDR表示を有効</param>
		/// <param name="options">レイアウト設定</param>
		public static void ColorField(GUIContent label, SerializedProperty property, bool showEyedropper, bool showAlpha, bool hdr, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			Vector4 result = EditorGUILayout.ColorField(label, property.vector4Value.ToColor(), showEyedropper, showAlpha, hdr, options).ToVector();
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
		/// <see cref="EditorGUILayout.TextArea"/>を表示
		/// </summary>
		/// <param name="property">プロパティ</param>
		/// <param name="options">レイアウト設定</param>
		public static void TextArea(SerializedProperty property, params GUILayoutOption[] options)
		{
			bool showMixedValue = EditorGUI.showMixedValue;
			if (property.hasMultipleDifferentValues)
			{
				EditorGUI.showMixedValue = true;
			}
			var result = EditorGUILayout.TextArea(property.stringValue, options);
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
