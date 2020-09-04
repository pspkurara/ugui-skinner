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

		public void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanArray<T>(property.objectReferenceValues, DefaultArrayLength);
			CleanupFieldsOtherThanObjectReference(property);
		}

		public void DrawInspector(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.ResetArray(property.objectReferenceValues, DefaultArrayLength, false);
			ResetArrayOtherThanObjectReference(property);

			var componentInfo = SkinnerEditorUtility.GetComponentInfos(typeof(T));


			for (int iz = 0; iz < property.objectReferenceValues.arraySize; iz++)
			{
				EditorGUILayout.BeginHorizontal();
				SerializedProperty gameObjectProperty = property.objectReferenceValues.GetArrayElementAtIndex(iz);
				m_FieldNumberTitle.text = string.Format(EditorConst.FieldNumberTitle, iz);
				gameObjectProperty.objectReferenceValue = EditorGUILayout.ObjectField(m_FieldNumberTitle, gameObjectProperty.objectReferenceValue, typeof(T), true);
				if (componentInfo.isComponent && componentInfo.allowMultiplyComponent)
				{
					T c = gameObjectProperty.objectReferenceValue as T;
					int componentIndex = -1;
					List<T> componentList = null;
					if (c)
					{
						componentList = (c as Component).gameObject.GetComponents<T>().ToList();
						componentIndex = componentList.IndexOf(c);
					}
					bool guiEnabled = GUI.enabled;
					if (componentIndex < 0)
					{
						componentIndex = 0;
						GUI.enabled = false;
					}
					int editIndex = EditorGUILayout.IntField(GUIContent.none, componentIndex, EditorConst.ComponentIndexFieldMaxWidth);
					if (editIndex != componentIndex)
					{
						editIndex = Mathf.Clamp(editIndex, 0, componentList.Count - 1);
						gameObjectProperty.objectReferenceValue = componentList[editIndex];
						gameObjectProperty.serializedObject.ApplyModifiedProperties();
					}
					GUI.enabled = guiEnabled;
				}
				if (SkinnerEditorUtility.DrawAddButton(EditorConst.RemoveFieldButtonTitle, () => {
					property.objectReferenceValues.GetArrayElementAtIndex(iz).objectReferenceValue = null;
					property.objectReferenceValues.DeleteArrayElementAtIndex(iz);
					property.objectReferenceValues.serializedObject.ApplyModifiedProperties();
				})) return;
				EditorGUILayout.EndHorizontal();
			}
			EditorGUILayout.BeginHorizontal();

			DrawOptionProperty(property);

			m_AddFieldButtonTitle.text = string.Format(EditorConst.AddFieldButtonTitle, SkinnerEditorUtility.GetEditorName(typeof(T).Name));

			bool isClicked = SkinnerEditorUtility.DrawAddButton(m_AddFieldButtonTitle, () => {
				property.objectReferenceValues.InsertArrayElementAtIndex(property.objectReferenceValues.arraySize);
				property.objectReferenceValues.serializedObject.ApplyModifiedProperties();
			});

			if (isClicked) return;
			EditorGUILayout.EndHorizontal();
		}

		protected abstract void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property);
		
		protected abstract void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property);

		protected abstract void DrawOptionProperty(EditorSkinPartsPropertry property);

	}

}
