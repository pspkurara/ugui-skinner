using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 配列でフレキシブルに切り替えられる用のスキナーパーツ向けインスペクター
	/// <see cref="SkinLogicOnArray{T}"/>を使っている際に使用する
	/// </summary>
	/// <typeparam name="T">配列で表示されるオブジェクトの型</typeparam>
	/// <seealso cref="SkinLogicOnArray{T}"/>
	public abstract class SkinPartsOnArrayInspector<T> : ISkinPartsInspector where T : Object
	{

		private const int DefaultArrayLength = 1;

		private GUIContent m_AddFieldButtonTitle = new GUIContent();
		private GUIContent m_FieldNumberTitle = new GUIContent();

		protected virtual string displayObjectTypeName { get { return SkinnerEditorUtility.GetEditorName(typeof(T).Name); } }

		public void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanObjectReferenceArrayWithFlexibleSize<T>(property.objectReferenceValues, DefaultArrayLength);
			CleanupFieldsOtherThanObjectReference(property);
		}

		public void DrawInspector(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.ResetArray(property.objectReferenceValues, DefaultArrayLength, false);
			ResetArrayOtherThanObjectReference(property);

			int indent = EditorGUI.indentLevel;

			for (int iz = 0; iz < property.objectReferenceValues.arraySize; iz++)
			{
				EditorGUILayout.BeginHorizontal();
				SerializedProperty gameObjectProperty = property.objectReferenceValues.GetArrayElementAtIndex(iz);
				m_FieldNumberTitle.text = string.Format(EditorConst.FieldNumberTitle, iz);
				SkinnerEditorGUILayout.ObjectField(m_FieldNumberTitle, gameObjectProperty, typeof(T));

				EditorGUI.indentLevel = 0;
				if (SkinnerEditorUtility.DrawRemoveButton(EditorConst.RemoveFieldButtonTitle, () => {
					property.objectReferenceValues.GetArrayElementAtIndex(iz).objectReferenceValue = null;
					property.objectReferenceValues.DeleteArrayElementAtIndex(iz);
					property.objectReferenceValues.serializedObject.ApplyModifiedProperties();
				})) return;
				EditorGUI.indentLevel = indent;
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.BeginHorizontal();

			bool guiEnabled = GUI.enabled;
			bool showMixedValue = EditorGUI.showMixedValue;
			DrawOptionProperty(property);
			EditorGUI.showMixedValue = showMixedValue;
			GUI.enabled = guiEnabled;

			m_AddFieldButtonTitle.text = string.Format(EditorConst.AddFieldButtonTitle, displayObjectTypeName);

			EditorGUI.indentLevel = 0;
			bool isClicked = SkinnerEditorUtility.DrawAddButton(m_AddFieldButtonTitle, () => {
				property.objectReferenceValues.InsertArrayElementAtIndex(property.objectReferenceValues.arraySize);
				property.objectReferenceValues.serializedObject.ApplyModifiedProperties();
			});
			EditorGUI.indentLevel = indent;

			if (isClicked) return;
			EditorGUILayout.EndHorizontal();
		}

		protected abstract void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property);
		
		protected abstract void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property);

		protected abstract void DrawOptionProperty(EditorSkinPartsPropertry property);

	}

}
