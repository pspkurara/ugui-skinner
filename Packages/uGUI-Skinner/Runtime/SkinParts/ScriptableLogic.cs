using System;
using System.Diagnostics;

namespace Pspkurara.UI.Skinner
{

	[SkinParts(SkinPartsType.ScriptableLogic, typeof(Logic))]
	public static class ScriptableLogic
	{

		public const int LogicIndex = 0;
		public const int RequiredObjectLength = LogicIndex + 1;

		/// <summary>
		/// 適応ロジック
		/// </summary>
		internal sealed class Logic : ISkinLogic
		{

			UserLogic userLogic;
			SkinLogicProperty ? ignoredLogicProperty;
			bool catchedError;

			/// <summary>
			/// 値をオブジェクトに反映させる
			/// </summary>
			/// <param name="property">プロパティ</param>
			public void SetValues(SkinLogicProperty property)
			{
				// エラー出るようなら何もしてほしくない
				if (catchedError) return;
				//値がないなら何もしない
				if (property.objectReferenceValues.Count < RequiredObjectLength) return;
				if (!userLogic)
				{
					userLogic = property.objectReferenceValues[LogicIndex] as UserLogic;
					//空か型違いのため処理終了
					if (!userLogic) return;
				}
				if (!ignoredLogicProperty.HasValue)
				{
					ignoredLogicProperty = SkinLogicProperty.GetCopyWithUniqueSkinPartsProperty(property);
					ignoredLogicProperty.Value.objectReferenceValues.Remove(userLogic);
				}
				UserLogicExtension.SetActiveUserLogic(userLogic);
				// 自由に処理をかけるのでエラー回避を入れておく
				try
				{
					userLogic.SetValues(ignoredLogicProperty.Value);
				}
				// エラーが起こっても何もせず終わる
				catch (Exception)
				{
					// フラグ立ててそれ以上処理不要にする
					catchedError = true;
				}
				UserLogicExtension.ReleaseActiveUserLogic();
			}

		}

		/// <summary>
		/// 対象のスキンパーツを生成
		/// </summary>
		/// <param name="userLogic">設定したいロジックのインスタンスオブジェクト</param>
		/// <param name="property">ロジックに合ったプロパティ</param>
		/// <returns>生成したスキンパーツ</returns>
		public static SkinParts CreateSkinParts(UserLogic userLogic, SkinPartsPropertry property)
		{
			var parts = new SkinParts();
			SkinnerUtility.ResetList(parts.property.objectReferenceValues, RequiredObjectLength);
			parts.property.objectReferenceValues[LogicIndex] = userLogic;
			parts.property.objectReferenceValues.AddRange(property.objectReferenceValues);
			parts.property.floatValues.AddRange(property.floatValues);
			parts.property.vector4Values.AddRange(property.vector4Values);
			return parts;
		}

	}

}
