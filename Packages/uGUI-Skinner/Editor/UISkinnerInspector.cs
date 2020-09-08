using Pspkurara.UI.Skinner;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pspkurara.UI
{

	/// <summary>
	/// <see cref="UISkinner"/>のインスペクター
	/// </summary>
	[CanEditMultipleObjects]
	[CustomEditor(typeof(UISkinner))]
	internal partial class UISkinnerInspector : Editor
	{

		const string FOLDOUT_EDITOR_PREFS_KEY = "COMPONENT_SKINNER_FOLDOUT_";

		public static class FieldName
		{
			public const string StyleIndex = "m_StyleIndex";
			public const string Styles = "m_Styles";
			public const string StyleKey = "m_StyleKey";
			public const string Parts = "m_Parts";
			public const string Type = "m_Type";
			public const string Property = "m_Property";
		}

		#region TempField

		private SerializedProperty m_StyleIndex;
		private SerializedProperty m_SkinStyles;

		private EditorSkinPartsPropertry m_SkinPartsProperty = null;

		private int[] m_SkinnerPartsOptionValues = null;
		private GUIContent[] m_SkinnerPartsDisplayNames = null;
		private GUIContent m_SkinFoldoutTitle = null;
		private GUIContent m_CurrentSelectStyleTitle = null;

		#endregion

		#region プロパティ

		/// <summary>
		/// スキンスタイルの総数
		/// </summary>
		private int skinLength { get { return m_SkinStyles.arraySize; } }

		/// <summary>
		/// 現在選択しているスキンスタイルの番号
		/// </summary>
		private int currentStyleIndex { get { return m_StyleIndex.intValue; } set { m_StyleIndex.intValue = value; } }

		#endregion

		#region メソッド

		private void OnEnable()
		{
			m_SkinnerPartsOptionValues = SkinPartsAccess.GetAllSkinPartsIds();
			m_SkinnerPartsDisplayNames = m_SkinnerPartsOptionValues.Select(id => new GUIContent(SkinnerEditorUtility.GetEditorName(SkinPartsAccess.GetSkinPartsRootType(id).Name))).ToArray();

			m_SkinFoldoutTitle = new GUIContent();
			m_CurrentSelectStyleTitle = new GUIContent();

			m_SkinStyles = serializedObject.FindProperty(FieldName.Styles);
			m_StyleIndex = serializedObject.FindProperty(FieldName.StyleIndex);

			m_SkinPartsProperty = new EditorSkinPartsPropertry();
		}

		public override void OnInspectorGUI()
		{

			serializedObject.Update();

			int edittedCurrentStyle = currentStyleIndex;
			GUILayout.Label(EditorConst.CurrentSelectStyleTitle);
			GUILayout.BeginHorizontal();
			{
				if (GUILayout.Button(EditorConst.LeftSkinSelectArrow, EditorConst.SkinSelectArrowMaxWidth))
				{
					edittedCurrentStyle--;
				}
				GUILayout.FlexibleSpace();
				m_CurrentSelectStyleTitle.text = m_StyleIndex.hasMultipleDifferentValues ? EditorConst.CurrentSkinHasMultipleDifferentValue : edittedCurrentStyle.ToString();
				GUILayout.Label(m_CurrentSelectStyleTitle);
				GUILayout.FlexibleSpace();
				if (GUILayout.Button(EditorConst.RightSkinSelectArrow, EditorConst.SkinSelectArrowMaxWidth))
				{
					edittedCurrentStyle++;
				}
			}
			GUILayout.EndHorizontal();

			SkinnerEditorUtility.DrawLine();

			if (currentStyleIndex != edittedCurrentStyle)
			{
				if (edittedCurrentStyle < 0 || edittedCurrentStyle >= m_SkinStyles.arraySize) return;
				currentStyleIndex = edittedCurrentStyle;
				ApplySkin();
			}

			for (int skinStylesIndex = 0; skinStylesIndex < m_SkinStyles.arraySize; skinStylesIndex++)
			{

				SerializedProperty objProp = m_SkinStyles.GetArrayElementAtIndex(skinStylesIndex).FindPropertyRelative(FieldName.Parts);
				SerializedProperty styleKey = m_SkinStyles.GetArrayElementAtIndex(skinStylesIndex).FindPropertyRelative(FieldName.StyleKey);

				GUIStyle style = (edittedCurrentStyle == skinStylesIndex) ? EditorConst.HighLightFoldoutStyle : EditorConst.NormalFoldoutStyle;

				bool hasStyleKey = !string.IsNullOrEmpty(styleKey.stringValue);

				EditorGUILayout.BeginHorizontal();
				m_SkinFoldoutTitle.text = hasStyleKey ? string.Format(EditorConst.SkinFoldTitleHasStyleKey, skinStylesIndex, styleKey.stringValue) : string.Format(EditorConst.SkinFoldTitle, skinStylesIndex);
				bool foldOut = EditorGUILayout.Foldout(GetFoldOut(skinStylesIndex), m_SkinFoldoutTitle, style);
				SetFoldOut(skinStylesIndex, foldOut);

				if (GetFoldOut(skinStylesIndex))
				{
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.PropertyField(styleKey, EditorConst.SkinnerStyleKeyFieldTitle);

					for (int skinPartsIndex = 0; skinPartsIndex < objProp.arraySize; skinPartsIndex++)
					{

						SerializedProperty partsProp = objProp.GetArrayElementAtIndex(skinPartsIndex);
						SerializedProperty uiSkinnedPartsTypeProperty = partsProp.FindPropertyRelative(FieldName.Type);
						int uiSkinnerPartsType = uiSkinnedPartsTypeProperty.intValue;
						uiSkinnedPartsTypeProperty.intValue = EditorGUILayout.IntPopup(uiSkinnerPartsType, m_SkinnerPartsDisplayNames, m_SkinnerPartsOptionValues);

						m_SkinPartsProperty.MapProperties(partsProp.FindPropertyRelative(FieldName.Property));

						var rootType = SkinPartsAccess.GetSkinPartsRootType(uiSkinnerPartsType);
						var inspector = SkinPartsInspectorAccess.GetSkinInspector(rootType);

						EditorGUI.indentLevel++;

						EditorGUI.BeginChangeCheck();
						inspector.DrawInspector(m_SkinPartsProperty);
						if (EditorGUI.EndChangeCheck())
						{
							if (skinStylesIndex == m_StyleIndex.intValue)
							{
								ApplySkin();
							}
						}

						EditorGUI.indentLevel--;

						if (SkinnerEditorUtility.DrawRemoveButton(EditorConst.RemovePartsButtonTitle, () => {
							objProp.DeleteArrayElementAtIndex(skinPartsIndex);
							serializedObject.ApplyModifiedProperties();
						})) return;
					}

					EditorGUILayout.Space();
					EditorGUILayout.BeginHorizontal();
					if (SkinnerEditorUtility.DrawAddButton(EditorConst.AddPartsButtonTitle, () => {
						objProp.InsertArrayElementAtIndex(objProp.arraySize);
						serializedObject.ApplyModifiedProperties();
					})) return;

					EditorGUILayout.Space();
				}

				if (SkinnerEditorUtility.DrawRemoveButton(EditorConst.RemoveSkinButtonTitle, () => {
					m_SkinStyles.DeleteArrayElementAtIndex(skinStylesIndex);
					if (currentStyleIndex >= skinLength)
					{
						// スキンがゼロの場合
						if (skinLength == 0)
						{
							// 規定値としてゼロを入れておく
							currentStyleIndex = 0;
						}
						// 設定された番号をスキンの数が下回った場合
						else
						{
							// とりあえずサイズより小さくしておく
							currentStyleIndex = m_SkinStyles.arraySize - 1;
							// 反映
							ApplySkin();
						}
					}
					serializedObject.ApplyModifiedProperties();
				})) return;
				EditorGUILayout.EndHorizontal();

				if (foldOut)
				{
					EditorGUILayout.Space();
					SkinnerEditorUtility.DrawLine();
					EditorGUILayout.Space();
				}
				serializedObject.ApplyModifiedProperties();
			}

			EditorGUILayout.BeginHorizontal();

			if (SkinnerEditorUtility.DrawAddButton(EditorConst.AddSkinButtonTitle, () => {
				m_SkinStyles.InsertArrayElementAtIndex(m_SkinStyles.arraySize);
				serializedObject.ApplyModifiedProperties();
			})) return;

			EditorGUILayout.Space();

			if (SkinnerEditorUtility.DrawCleanupButton(EditorConst.CleanupButtonTitle, () => {
				Cleanup();
				serializedObject.ApplyModifiedProperties();
			})) return;

			EditorGUILayout.EndHorizontal();

			serializedObject.ApplyModifiedProperties();

		}

		/// <summary>
		/// 現在のスキンを反映する
		/// </summary>
		private void ApplySkin()
		{
			foreach (Object t in serializedObject.targetObjects)
			{
				UISkinner skinnerObj = t as UISkinner;
				skinnerObj.SetSkin(Mathf.Clamp(currentStyleIndex, 0, skinLength - 1));
			}
		}
		
		private void Cleanup()
		{
			for (int skinStylesIndex = 0; skinStylesIndex < m_SkinStyles.arraySize; skinStylesIndex++)
			{
				SerializedProperty objProp = m_SkinStyles.GetArrayElementAtIndex(skinStylesIndex).FindPropertyRelative(FieldName.Parts);
				for (int skinPartsIndex = 0; skinPartsIndex < objProp.arraySize; skinPartsIndex++)
				{
					SerializedProperty partsProp = objProp.GetArrayElementAtIndex(skinPartsIndex);
					SerializedProperty uiSkinnedPartsTypeProperty = partsProp.FindPropertyRelative(FieldName.Type);
					int uiSkinnerPartsType = uiSkinnedPartsTypeProperty.intValue;

					var rootType = SkinPartsAccess.GetSkinPartsRootType(uiSkinnerPartsType);
					var inspector = SkinPartsInspectorAccess.GetSkinInspector(rootType);

					m_SkinPartsProperty.MapProperties(partsProp.FindPropertyRelative(FieldName.Property));

					inspector.CleanupFields(m_SkinPartsProperty);

				}
			}
		}

		#region EditorPrefs

		private static string GetFoldoutKey(int foldOutIndex)
		{
			return FOLDOUT_EDITOR_PREFS_KEY + foldOutIndex;
		}

		private static bool GetFoldOut(int foldOutIndex)
		{
			return EditorPrefs.GetBool(GetFoldoutKey(foldOutIndex), true);
		}

		private static void SetFoldOut(int foldOutIndex, bool value)
		{
			EditorPrefs.SetBool(GetFoldoutKey(foldOutIndex), value);
		}

		#endregion

		#endregion

	}

}