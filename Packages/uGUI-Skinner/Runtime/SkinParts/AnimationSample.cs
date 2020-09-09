using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.AnimationSample, typeof(AnimationSample), typeof(Logic))]
	public static class AnimationSample
	{

		public const int GameObjectIndex = 0;
		public const int AnimationClipIndex = 1;
		public const int TimeIndex = 0;
		public const int ObjectLength = AnimationClipIndex + 1;
		public const int FloatLength = TimeIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : ISkinLogic
		{

			private GameObject rootGameObject = null;
			private AnimationClip animationClip = null;
			private float activeTime;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.objectReferenceValues.Count < ObjectLength) return;
				if (!rootGameObject)
				{
					rootGameObject = property.objectReferenceValues[GameObjectIndex] as GameObject;
					//空か型違いのため処理終了
					if (!rootGameObject) return;
				}
				if (!animationClip)
				{
					animationClip = property.objectReferenceValues[AnimationClipIndex] as AnimationClip;
					//空か型違いのため処理終了
					if (!animationClip) return;
				}
				//値がないなら何もしない
				if (property.floatValues.Count < FloatLength) return;
				activeTime = property.floatValues[TimeIndex];
				animationClip.SampleAnimation(rootGameObject, activeTime);
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="rootGameObject">ルートとなるGameObject</param>
		/// <param name="animationClip">アニメーションクリップ</param>
		/// <param name="time">クリップをサンプルする時間</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(GameObject rootGameObject, AnimationClip animationClip, float time)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetObjectReference(parts.property.objectReferenceValues, ObjectLength);
			SkinnerUtility.ResetFloat(parts.property.floatValues, FloatLength);
			parts.property.objectReferenceValues[GameObjectIndex] = rootGameObject;
			parts.property.objectReferenceValues[AnimationClipIndex] = animationClip;
			parts.property.floatValues[TimeIndex] = time;
			return parts;
		}

	}

}
