using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(SubSkinner))]
	internal sealed class SubSkinnerInspector : SkinPartsOnArrayInspector<UISkinner>
	{

		protected override void CleanupFieldsOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.CleanArray(property.floatValues, SubSkinner.FloatLength, SkinDefaultValue.Integer);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
			SkinnerEditorUtility.CleanArray(property.stringValues, SubSkinner.StringLength, SkinDefaultValue.String);
		}

		protected override void ResetArrayOtherThanObjectReference(EditorSkinPartsPropertryWithoutObjectReference property)
		{
			SkinnerEditorUtility.ResetArray(property.floatValues, SubSkinner.FloatLength, SkinDefaultValue.Integer);
			SkinnerEditorUtility.ResetArray(property.stringValues, SubSkinner.StringLength, SkinDefaultValue.String);
		}

		protected override void DrawOptionProperty(EditorSkinPartsPropertry property)
		{
			EditorGUILayout.BeginVertical();

			var styleIndex = property.floatValues.GetArrayElementAtIndex(SubSkinner.StyleIndex);
			var styleKey = property.stringValues.GetArrayElementAtIndex(SubSkinner.StyleKeyIndex);

			var skinners = property.objectReferenceValues.ArrayToEnumerable()
				.Select(p => p.objectReferenceValue as UISkinner)
				.Where(po => po != null);

			var allStyleKeys = skinners
				.SelectMany(s => s.GetHasStyleKeyStyles())
				.Select(s => s.Value.styleKey)
				.Distinct()
				.ToList();

			var displayOptions = allStyleKeys
				.InsertThen(0, "* Use Style Index *")
				.ToArray();

			var optionValues = allStyleKeys
				.ReplaceThen(0, string.Empty)
				.ToArray();

			SkinnerEditorGUILayout.StringPopup(SkinContent.StyleKey, styleKey, displayOptions, optionValues);

			bool guiEnabled = GUI.enabled;
			if (!string.IsNullOrEmpty(styleKey.stringValue))
			{
				styleIndex.floatValue = SkinDefaultValue.Integer;
				GUI.enabled = false;
			}

			if (skinners.Count() > 0)
			{
				int mostMaxStyleCount = skinners.Max(po => po.Length) - 1;
				SkinnerEditorGUILayout.IntSlider(SkinContent.StyleIndex, styleIndex, 0, mostMaxStyleCount);
			}
			else
			{
				SkinnerEditorGUILayout.IntField(SkinContent.StyleIndex, styleIndex, 0, int.MaxValue);
			}

			GUI.enabled = guiEnabled;

			EditorGUILayout.EndVertical();

		}

	}

}
