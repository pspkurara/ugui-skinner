using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.GraphicEnable, typeof(GraphicEnable), typeof(Logic))]
	public static class GraphicEnable
	{

		public const int BoolIndex = 0;
		public const int BoolLength = BoolIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<Graphic>
		{

			/// <summary>
			/// enabledフラグ
			/// </summary>
			private bool activeFlag;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public override void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.boolValues.Count < BoolLength) return;
				activeFlag = property.boolValues[BoolIndex];
				base.SetValues(property);
			}

			protected override void OnApplyValue(Graphic obj)
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
		public static SkinParts CreateSkinnerParts(IEnumerable<Graphic> graphics, bool enabled)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetBoolean(parts.property.boolValues, BoolLength);
			parts.property.objectReferenceValues.AddRange(graphics.Cast<Object>());
			parts.property.boolValues[BoolIndex] = enabled;
			return parts;
		}

	}

}
