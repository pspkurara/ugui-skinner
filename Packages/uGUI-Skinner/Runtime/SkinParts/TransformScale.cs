using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.TransformScale, typeof(Logic))]
	public static class TransformScale
	{

		public const int VectorIndex = 0;
		public const int VectorLength = VectorIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<Transform>
		{

			/// <summary>
			/// TransformのLocalScale
			/// </summary>
			private Vector3 activeScale;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public override void SetValues(SkinPartsPropertry property)
			{
				//値がないなら何もしない
				if (property.vector4Values.Count < VectorLength) return;
				activeScale = property.vector4Values[VectorIndex];
				base.SetValues(property);
			}

			protected override void OnApplyValue(Transform obj)
			{
				obj.localScale = activeScale;
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="transforms">設定したいTransform</param>
		/// <param name="localScale">TransformのlocalScale</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(IEnumerable<Transform> transforms, Vector3 localScale)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetList(parts.property.vector4Values, VectorLength);
			parts.property.objectReferenceValues.AddRange(transforms.Cast<Object>());
			parts.property.vector4Values[VectorIndex] = localScale;
			return parts;
		}
	}

}
