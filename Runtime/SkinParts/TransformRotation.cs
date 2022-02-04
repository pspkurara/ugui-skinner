using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.TransformRotation, typeof(Logic))]
	public static class TransformRotation
	{

		public const int RotationIndex = 0;
		public const int VectorLength = RotationIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : SkinLogicOnArray<Transform>
		{

			/// <summary>
			/// TransformのLocalScale
			/// </summary>
			private Quaternion activeRotation;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public override void SetValues(SkinLogicProperty property)
			{
				//値がないなら何もしない
				if (property.vector4Values.Count < VectorLength) return;
				activeRotation = Quaternion.Euler(property.vector4Values[RotationIndex]);
				base.SetValues(property);
			}

			protected override void OnApplyValue(Transform obj)
			{
				obj.localRotation = activeRotation;
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="transforms">設定したいTransform</param>
		/// <param name="localRotation">TransformのlocalRotation</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(IEnumerable<Transform> transforms, Quaternion localRotation)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetList(parts.property.vector4Values, VectorLength);
			parts.property.objectReferenceValues.AddRange(transforms.Cast<Object>());
			parts.property.vector4Values[RotationIndex] = localRotation.eulerAngles;
			return parts;
		}
	}

}
