using Pspkurara.UI.Skinner;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;
using System;
using System.Collections.Generic;

namespace Pspkurara.UI
{

	/// <summary>
	/// <see cref="UISkinner"/>のインスペクター
	/// </summary>
	[CanEditMultipleObjects]
	[CustomEditor(typeof(UISkinner))]
	internal partial class UISkinnerInspector : Editor
	{

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
		private SkinParts m_RuntimeSkinParts = null;

		private int[] m_SkinnerPartsOptionValues = null;
		private GUIContent[] m_SkinnerPartsDisplayNames = null;
		private GUIContent m_SkinFoldoutTitle = null;
		private GUIContent m_CurrentSelectStyleTitle = null;
		private GUIUniqueIdentifier m_GuiUniqueIdCounter = new GUIUniqueIdentifier();
		private Stack<UISkinner> m_CirculationApplyTrace;
		private EditorSkinPartsPropertry m_CirculationPropertyMapper;

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
			m_RuntimeSkinParts = new SkinParts();

			m_CirculationApplyTrace = new Stack<UISkinner>();
			m_CirculationPropertyMapper = new EditorSkinPartsPropertry();

			m_GuiUniqueIdCounter.ResetLastControlGuiId();
		}

		public override void OnInspectorGUI()
		{
			m_GuiUniqueIdCounter.Initialize();

			serializedObject.Update();

			int edittedCurrentStyle = currentStyleIndex;
			GUILayout.Label(EditorConst.CurrentSelectStyleTitle);
			m_GuiUniqueIdCounter.Next();
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
				m_GuiUniqueIdCounter.RecordLastGuiId();
				if (edittedCurrentStyle < 0 || edittedCurrentStyle >= m_SkinStyles.arraySize) return;
				currentStyleIndex = edittedCurrentStyle;
				ApplySkin();
			}

			m_GuiUniqueIdCounter.BeginNestedLoop();

			for (int skinStylesIndex = 0; skinStylesIndex < m_SkinStyles.arraySize; skinStylesIndex++)
			{

				var skinStyleElementProperty = m_SkinStyles.GetArrayElementAtIndex(skinStylesIndex);

				if (!skinStyleElementProperty.propertyPath.Contains(FieldName.Styles)) continue;

				var skinPartsProperty = skinStyleElementProperty.FindPropertyRelative(FieldName.Parts);
				var styleKey = skinStyleElementProperty.FindPropertyRelative(FieldName.StyleKey);

				GUIStyle style = (edittedCurrentStyle == skinStylesIndex && !m_StyleIndex.hasMultipleDifferentValues) ? EditorConst.HighLightFoldoutStyle : EditorConst.NormalFoldoutStyle;

				bool hasStyleKey = !string.IsNullOrEmpty(styleKey.stringValue);

				EditorGUILayout.BeginHorizontal();
				if (hasStyleKey || styleKey.hasMultipleDifferentValues)
				{
					string styleKeyText;
					if (styleKey.hasMultipleDifferentValues)
					{
						styleKeyText = EditorConst.StyleKeyHasMultipleDifferentValue;
					}
					else
					{
						styleKeyText = styleKey.stringValue;
					}
					m_SkinFoldoutTitle.text = string.Format(EditorConst.SkinFoldTitleHasStyleKey, skinStylesIndex, styleKeyText);
				}
				else
				{
					m_SkinFoldoutTitle.text = string.Format(EditorConst.SkinFoldTitle, skinStylesIndex);
				}
				EditorGUI.BeginChangeCheck();
				m_GuiUniqueIdCounter.Next();
				skinStyleElementProperty.isExpanded = EditorGUILayout.Foldout(skinStyleElementProperty.isExpanded, m_SkinFoldoutTitle, true, style);
				if (EditorGUI.EndChangeCheck())
				{
					m_GuiUniqueIdCounter.RecordLastGuiId();
				}

				if (skinStyleElementProperty.isExpanded)
				{

					EditorGUILayout.EndHorizontal();

					EditorGUI.indentLevel += EditorConst.SkinStyleChildIndent;

					EditorGUILayout.PropertyField(styleKey, EditorConst.SkinnerStyleKeyFieldTitle);

					EditorGUI.indentLevel -= EditorConst.SkinStyleChildIndent;

					m_GuiUniqueIdCounter.BeginNestedLoop();

					for (int skinPartsIndex = 0; skinPartsIndex < skinPartsProperty.arraySize; skinPartsIndex++)
					{

						SerializedProperty skinPartsElementProperty = skinPartsProperty.GetArrayElementAtIndex(skinPartsIndex);

						if (!skinPartsElementProperty.propertyPath.Contains(FieldName.Parts)) continue;

						try
						{
							SerializedProperty skinPartsTypeProperty = skinPartsElementProperty.FindPropertyRelative(FieldName.Type);
							int skinPartsType = skinPartsTypeProperty.intValue;

							EditorGUILayout.BeginHorizontal();

							EditorGUI.BeginChangeCheck();
							m_GuiUniqueIdCounter.Next();
							skinPartsElementProperty.isExpanded = EditorGUILayout.Toggle(GUIContent.none, skinPartsElementProperty.isExpanded, EditorStyles.foldout, GUILayout.Width(12));
							if (EditorGUI.EndChangeCheck())
							{
								m_GuiUniqueIdCounter.RecordLastGuiId();
							}

							m_GuiUniqueIdCounter.Next();
							int skinPartsTypeEditted = EditorGUILayout.IntPopup(skinPartsType, m_SkinnerPartsDisplayNames, m_SkinnerPartsOptionValues);
							if (skinPartsTypeEditted != skinPartsType)
							{
								skinPartsTypeProperty.intValue = skinPartsTypeEditted;
								serializedObject.ApplyModifiedProperties();
								m_GuiUniqueIdCounter.RecordLastGuiId();
								return;
							}
							EditorGUILayout.EndHorizontal();

							if (skinPartsElementProperty.isExpanded)
							{

								EditorGUI.indentLevel += EditorConst.SkinPartsChildIndent;

								// タイプの登録を確認
								if (SkinPartsAccess.IsCorrectSkinPartsId(skinPartsType))
								{
									var rootType = SkinPartsAccess.GetSkinPartsRootType(skinPartsType);

									// インスペクターの登録を確認
									if (SkinPartsInspectorAccess.IsRegistedInspector(rootType))
									{
										var inspector = SkinPartsInspectorAccess.GetSkinInspector(rootType);

										m_SkinPartsProperty.MapProperties(skinPartsElementProperty.FindPropertyRelative(FieldName.Property));

										EditorGUI.BeginChangeCheck();
										m_GuiUniqueIdCounter.Next();
										bool showMixedValue = EditorGUI.showMixedValue;
										inspector.DrawInspector(m_SkinPartsProperty);
										EditorGUI.showMixedValue = showMixedValue;
										if (EditorGUI.EndChangeCheck())
										{
											if (skinStylesIndex == m_StyleIndex.intValue)
											{
												ApplySkin();
												if (skinPartsType == 100) inspector.DrawInspector(m_SkinPartsProperty);
											}
											m_GuiUniqueIdCounter.RecordLastGuiId();
										}

									}
									else
									{
										// 該当インスペクターが存在しない場合は何もしない
										var skinPartsTypeName = SkinnerEditorUtility.GetEditorName(rootType.Name);
										EditorGUILayout.HelpBox(string.Format(EditorConst.MissingSkinPartsInspectorTypeMessage, skinPartsTypeName), EditorConst.MissingSkinPartsInspectorTypeMessageType);
									}

								}
								else
								{
									// 該当IDが存在しない場合は警告を出す
									EditorGUILayout.HelpBox(string.Format(EditorConst.MissingSkinPartsTypeMessage, skinPartsType), EditorConst.MissingSkinPartsTypeMessageType);
								}

								// 循環参照していたら警告を出す
								if (GetCirculationApplyTrace(m_SkinPartsProperty.objectReferenceValues))
								{
									EditorGUILayout.HelpBox(EditorConst.CirculationReferenceMessage, EditorConst.CirculationReferenceMessageType, true);
								}

								EditorGUI.indentLevel -= EditorConst.SkinPartsChildIndent;

								EditorGUI.indentLevel += EditorConst.SkinStyleChildIndent;

								m_GuiUniqueIdCounter.Next();
								if (SkinnerEditorUtility.DrawRemoveButton(EditorConst.RemovePartsButtonTitle, () =>
								{
									skinPartsProperty.DeleteArrayElementAtIndex(skinPartsIndex);
									serializedObject.ApplyModifiedProperties();
									m_GuiUniqueIdCounter.RecordLastGuiId();
								})) return;

								EditorGUI.indentLevel -= EditorConst.SkinStyleChildIndent;

								EditorGUILayout.Space();

							}

						}
						catch (InvalidOperationException) { }
					}

					EditorGUI.indentLevel += EditorConst.SkinStyleChildIndent;

					EditorGUILayout.BeginHorizontal();

					m_GuiUniqueIdCounter.EndNestedLoop();

					m_GuiUniqueIdCounter.Next();
					if (SkinnerEditorUtility.DrawAddButton(EditorConst.AddPartsButtonTitle, () => {
						skinPartsProperty.InsertArrayElementAtIndex(skinPartsProperty.arraySize);
						// 同じボタンを連打した場合は2回目以降Expandedの変更はしない
						if (!m_GuiUniqueIdCounter.isCurrentLastControlGuiId)
						{
							skinPartsProperty.GetArrayElementAtIndex(skinPartsProperty.arraySize - 1).isExpanded = true;
						}
						serializedObject.ApplyModifiedProperties();
						m_GuiUniqueIdCounter.RecordLastGuiId();
					})) return;

					EditorGUILayout.Space();

					EditorGUI.indentLevel -= EditorConst.SkinStyleChildIndent;

				}

				m_GuiUniqueIdCounter.Next();
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
					m_GuiUniqueIdCounter.RecordLastGuiId();
				})) return;
				EditorGUILayout.EndHorizontal();

				if (skinStyleElementProperty.isExpanded)
				{
					EditorGUILayout.Space();
					SkinnerEditorUtility.DrawLine();
					EditorGUILayout.Space();
				}
				serializedObject.ApplyModifiedProperties();
			}

			m_GuiUniqueIdCounter.EndNestedLoop();

			EditorGUILayout.BeginHorizontal();

			m_GuiUniqueIdCounter.Next();
			if (SkinnerEditorUtility.DrawAddButton(EditorConst.AddSkinButtonTitle, () => {
				m_SkinStyles.InsertArrayElementAtIndex(m_SkinStyles.arraySize);
				var addedStyle = m_SkinStyles.GetArrayElementAtIndex(m_SkinStyles.arraySize - 1);
				bool addedStyleExpanded = true;
				// 2つ目以降のスタイルを増やそうとしたときは増やした側にコピー元のExpandedの設定を継承させる
				if (m_SkinStyles.arraySize > 1)
				{
					var preStyle = m_SkinStyles.GetArrayElementAtIndex(m_SkinStyles.arraySize - 2);
					var preStylePartsArray = preStyle.FindPropertyRelative(FieldName.Parts);
					var addedStylePartsArray = addedStyle.FindPropertyRelative(FieldName.Parts);
					for (int i = 0; i < preStylePartsArray.arraySize; i++)
					{
						var addedStyleParts = addedStylePartsArray.GetArrayElementAtIndex(i);
						var preStyleParts = preStylePartsArray.GetArrayElementAtIndex(i);
						addedStyleParts.isExpanded = preStyleParts.isExpanded;
					}

					addedStyleExpanded = preStyle.isExpanded;
				}

				if (!m_GuiUniqueIdCounter.isCurrentLastControlGuiId)
				{
					addedStyle.isExpanded = addedStyleExpanded;
				}

				serializedObject.ApplyModifiedProperties();
				m_GuiUniqueIdCounter.RecordLastGuiId();
			})) return;

			EditorGUILayout.Space();

			m_GuiUniqueIdCounter.Next();
			if (SkinnerEditorUtility.DrawCleanupButton(EditorConst.CleanupButtonTitle, () => {
				Cleanup();
				serializedObject.ApplyModifiedProperties();
				m_GuiUniqueIdCounter.RecordLastGuiId();
			})) return;

			EditorGUILayout.EndHorizontal();

			serializedObject.ApplyModifiedProperties();

		}

		/// <summary>
		/// 現在のスキンを反映する
		/// </summary>
		private void ApplySkin()
		{
			serializedObject.ApplyModifiedProperties();
			foreach (Object t in serializedObject.targetObjects)
			{
				UISkinner skinner = t as UISkinner;
				skinner.SetSkin(Mathf.Clamp(currentStyleIndex, 0, skinLength - 1));
			}
			serializedObject.Update();
		}
		
		private void Cleanup()
		{
			var checkedSkinParts = new List<SkinParts>();
			for (int skinStylesIndex = 0; skinStylesIndex < m_SkinStyles.arraySize; skinStylesIndex++)
			{
				var skinStyleElement = m_SkinStyles.GetArrayElementAtIndex(skinStylesIndex);

				// 複数オブジェクトのマルチ値が適応されている可能性
				if (!skinStyleElement.propertyPath.Contains(FieldName.Styles)) continue;

				SerializedProperty skinPartsProperty = skinStyleElement.FindPropertyRelative(FieldName.Parts);

				// 複数オブジェクトのマルチ値が適応されている可能性
				if (!skinPartsProperty.propertyPath.Contains(FieldName.Parts)) continue;

				checkedSkinParts.Clear();

				for (int skinPartsIndex = skinPartsProperty.arraySize-1; skinPartsIndex >= 0; skinPartsIndex--)
				{
					SerializedProperty partsProp = skinPartsProperty.GetArrayElementAtIndex(skinPartsIndex);

					// 複数オブジェクトのマルチ値が適応されている可能性
					if (!partsProp.propertyPath.Contains(FieldName.Parts)) continue;

					SerializedProperty skinPartsTypeProperty = partsProp.FindPropertyRelative(FieldName.Type);
					int skinPartsType = skinPartsTypeProperty.intValue;
					
					// 該当IDが存在しない場合は何もしない
					if (!SkinPartsAccess.IsCorrectSkinPartsId(skinPartsType))
					{
						// ID存在しないなら不正なデータなのでいらないと思う
						skinPartsProperty.DeleteArrayElementAtIndex(skinPartsIndex);
						continue;
					}

					var rootType = SkinPartsAccess.GetSkinPartsRootType(skinPartsType);

					// 該当インスペクターが存在しない場合は何もしない
					if (!SkinPartsInspectorAccess.IsRegistedInspector(rootType))
					{
						// インスペクター存在しないなら使えないデータなのでいらないと思う
						skinPartsProperty.DeleteArrayElementAtIndex(skinPartsIndex);
						continue;
					}

					var inspector = SkinPartsInspectorAccess.GetSkinInspector(rootType);

					m_SkinPartsProperty.MapProperties(partsProp.FindPropertyRelative(FieldName.Property));

					inspector.CleanupFields(m_SkinPartsProperty);

					// オブジェクト参照の数を確認 (全てnullかどうか)
					bool isExistObject = m_SkinPartsProperty.objectReferenceValues
						.ArrayToEnumerable()
						.Any(p => p.objectReferenceValue != null);

					// ひとつも参照が設定されていない
					if (!isExistObject)
					{
						// オブジェクト参照がない場合何にも影響しないのでいらないと思う
						skinPartsProperty.DeleteArrayElementAtIndex(skinPartsIndex);
						continue;
					}

					// まったく同じ設定のスキンパーツがないか調べる
					m_RuntimeSkinParts.type = skinPartsType;
					SkinnerEditorUtility.MapRuntimePropertyFromEditorProperty(m_RuntimeSkinParts.property, m_SkinPartsProperty);
					if (checkedSkinParts.Any(p => p.Equals(m_RuntimeSkinParts)))
					{
						// 同じ設定はいらないと思う
						skinPartsProperty.DeleteArrayElementAtIndex(skinPartsIndex);
					}
					else
					{
						// ユニークなものとしてチェックリストに増やす
						var cacheParts = new SkinParts();
						cacheParts.type = skinPartsType;
						SkinnerEditorUtility.MapRuntimePropertyFromEditorProperty(cacheParts.property, m_SkinPartsProperty);
						checkedSkinParts.Add(cacheParts);
					}

					// 循環参照チェック
					if (GetCirculationApplyTrace(m_SkinPartsProperty.objectReferenceValues))
					{
						// している場合は大本から切り離してしまったほうがよさそう
						skinPartsProperty.DeleteArrayElementAtIndex(skinPartsIndex);
					}

				}
			}
		}

		/// <summary>
		/// <see cref="SkinPartsPropertry.objectReferenceValues"/>が循環参照しているかを取得する
		/// </summary>
		/// <param name="property">探す対象のプロパティ</param>
		/// <param name="applyTrace">スタックリスト (初期値は自動取得)</param>
		/// <param name="propertyMapper"><see cref="EditorSkinPartsPropertry.objectReferenceValues"/>をアンパックするための変数 (初期値は自動取得)</param>
		/// <returns>循環参照をしている</returns>
		private bool GetCirculationApplyTrace(SerializedProperty property, Stack<UISkinner> applyTrace = null, EditorSkinPartsPropertry propertyMapper = null)
		{
			if (applyTrace == null) applyTrace = m_CirculationApplyTrace;
			if (propertyMapper == null) propertyMapper = m_CirculationPropertyMapper;

			var propertySkinner = property.serializedObject.targetObject as UISkinner;

			if (applyTrace.Contains(propertySkinner))
			{
				return true;
			}

			applyTrace.Push(propertySkinner);

			var skinners = property.ArrayToEnumerable()
				.Where(p => !p.hasMultipleDifferentValues)
				.Select(p => p.objectReferenceValue as UISkinner)
				.Where(po => po != null);

			bool isCirculation = false;

			foreach (var skinner in skinners)
			{
				var childProperty = new SerializedObject(skinner);
				var styles = childProperty.FindProperty(FieldName.Styles)
					.ArrayToEnumerable()
					.Where(p => !p.hasMultipleDifferentValues);

				foreach (var style in styles)
				{
					var parts = style.FindPropertyRelative(FieldName.Parts)
						.ArrayToEnumerable()
						.Where(p => !p.hasMultipleDifferentValues);

					foreach (var part in parts)
					{
						propertyMapper.MapProperties(part.FindPropertyRelative(FieldName.Property));
						if (GetCirculationApplyTrace(propertyMapper.objectReferenceValues, applyTrace, propertyMapper))
						{
							isCirculation = true;
						}
					}

					if (isCirculation) break;
				}

				childProperty.Dispose();

				if (isCirculation) break;
			}

			applyTrace.Pop();

			return isCirculation;
		}

		#endregion

	}

}