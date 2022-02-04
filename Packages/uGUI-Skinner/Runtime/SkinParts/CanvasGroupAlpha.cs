using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.CanvasGroupAlpha, typeof(Logic))]
	public static class CanvasGroupAlpha
	{

		public const int AlphaIndex = 0;
		public const int FloatLength = AlphaIndex + 1;

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
			public override void SetValues(SkinLogicProperty property)
			{
				//値がないなら何もしない
				if (property.floatValues.Count < FloatLength) return;
				activeAlpha = property.floatValues[AlphaIndex];
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
			SkinnerUtility.ResetList(parts.property.floatValues, FloatLength);
			parts.property.objectReferenceValues.AddRange(canvasGroups.Cast<Object>());
			parts.property.floatValues[AlphaIndex] = alpha;
			return parts;
		}

	}

}
