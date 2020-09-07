using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.CanvasGroupAlpha, typeof(CanvasGroupAlpha), typeof(Logic))]
	public static class CanvasGroupAlpha
	{

		public const int FloatIndex = 0;
		public const int FloatLength = FloatIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<CanvasGroup>
		{

			/// <summary>
			/// CanvasGroupのアルファ
			/// </summary>
			private float activeAlpha = 0;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public override void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.floatValues.Count < FloatLength) return;
				activeAlpha = property.floatValues[FloatIndex];
				base.SetValues(property);
			}

			protected override void OnApplyValue(CanvasGroup obj)
			{
				obj.alpha = activeAlpha;
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="canvasGroups">設定したいCanvasGroup</param>
		/// <param name="alpha">CanvasGroupのアルファ</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(IEnumerable<CanvasGroup> canvasGroups, float alpha)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetFloat(parts.property.floatValues, FloatLength);
			parts.property.objectReferenceValues.AddRange(canvasGroups.Cast<Object>());
			parts.property.floatValues[FloatIndex] = alpha;
			return parts;
		}

	}

}
