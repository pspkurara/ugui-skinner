using UnityEngine;
using UnityEditor;

namespace Pspkurara.UI.Skinner
{

	[SkinPartsInspector(typeof(AnimationSample))]
	internal sealed class AnimationSampleInspector : ISkinPartsInspector
	{
		public void CleanupFields(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.CleanArray(property.objectReferenceValues, AnimationSample.ObjectLength);
			SkinnerEditorUtility.CleanObject<GameObject>(property.objectReferenceValues, AnimationSample.GameObjectIndex);
			SkinnerEditorUtility.CleanObject<AnimationClip>(property.objectReferenceValues, AnimationSample.AnimationClipIndex);
			SkinnerEditorUtility.CleanArray(property.boolValues);
			SkinnerEditorUtility.CleanArray(property.colorValues);
			SkinnerEditorUtility.CleanArray(property.floatValues, AnimationSample.FloatLength);
			SkinnerEditorUtility.CleanArray(property.intValues);
			SkinnerEditorUtility.CleanArray(property.vector4Values);
		}

		public void DrawInspector(EditorSkinPartsPropertry property)
		{
			SkinnerEditorUtility.ResetArray(property.objectReferenceValues, AnimationSample.ObjectLength);
			SkinnerEditorUtility.ResetArray(property.floatValues, AnimationSample.FloatLength);
			var gameObjectProperty = property.objectReferenceValues.GetArrayElementAtIndex(AnimationSample.GameObjectIndex);
			var animationClipProperty = property.objectReferenceValues.GetArrayElementAtIndex(AnimationSample.AnimationClipIndex);
			var timeProperty = property.floatValues.GetArrayElementAtIndex(AnimationSample.TimeIndex);
			gameObjectProperty.objectReferenceValue = EditorGUILayout.ObjectField(SkinContent.RootGameObject, gameObjectProperty.objectReferenceValue, typeof(GameObject), true);
			animationClipProperty.objectReferenceValue = EditorGUILayout.ObjectField(SkinContent.AnimationClip, animationClipProperty.objectReferenceValue, typeof(AnimationClip), false);
			if (!animationClipProperty.hasMultipleDifferentValues && animationClipProperty.objectReferenceValue is AnimationClip)
			{
				var clip = animationClipProperty.objectReferenceValue as AnimationClip;
				EditorGUILayout.Slider(timeProperty, 0, clip.length, SkinContent.SampleTime);
			}
			else
			{
				bool showMixedValue = EditorGUI.showMixedValue;
				if (timeProperty.hasMultipleDifferentValues)
				{
					EditorGUI.showMixedValue = true;
				}
				timeProperty.floatValue = EditorGUILayout.FloatField(SkinContent.SampleTime, timeProperty.floatValue);
				EditorGUI.showMixedValue = showMixedValue;
			}
		}

	}

}
