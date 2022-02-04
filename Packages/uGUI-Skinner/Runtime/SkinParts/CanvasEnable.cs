using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.CanvasEnable, typeof(Logic))]
	public static class CanvasEnable
	{

		public const int FlagIndex = 0;
		public const int FloatLength = FlagIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<Canvas>
		{

			/// <summary>
			/// enabledフラグ
			/// </summary>
			private bool activeFlag;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public override void SetValues(SkinLogicProperty property)
			{
				//値がないなら何もしない
				if (property.floatValues.Count < FloatLength) return;
				activeFlag = property.floatValues[FlagIndex].ToBool();
				base.SetValues(property);
			}

			protected override void OnApplyValue(Canvas obj)
			{
				obj.enabled = activeFlag;
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="outlines">設定したいGraphic</param>
		/// <param name="color">Graphicのenabled</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(IEnumerable<Canvas> graphics, bool enabled)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetList(parts.property.floatValues, FloatLength);
			parts.property.objectReferenceValues.AddRange(graphics.Cast<Object>());
			parts.property.floatValues[FlagIndex] = enabled.ToFloat();
			return parts;
		}

	}

}
