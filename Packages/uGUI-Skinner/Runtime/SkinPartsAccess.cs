using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 全てのスキンパーツを探せるアクセスクラス
	/// </summary>
	internal static class SkinPartsAccess
	{

		#region 変数

		/// <summary>
		/// スキンパーツのIDとクラスを紐付ける一覧
		/// キー: スキンパーツID ( <see cref="SkinPartsType"/>が使われる )
		/// 値 (キー) : 親の型 ( ルートタイプ )
		/// 値 (値) : ルートタイプのクラスに紐付けられた<see cref="SkinPartsAttribute"/>属性
		/// </summary>
		private static readonly Dictionary<int, KeyValuePair<Type, SkinPartsAttribute>> m_SkinParts = CreateSkinPartsList();

		/// <summary>
		/// スキンパーツIDがおかしいときに返すダミー処理
		/// </summary>
		private static readonly DoNothingLogic m_DoNothingLogic = new DoNothingLogic();

		#endregion

		#region メソッド

		/// <summary>
		/// 該当属性を持つスキンパーツクラスを全取得してリストアップ
		/// </summary>
		private static Dictionary<int, KeyValuePair<Type, SkinPartsAttribute>> CreateSkinPartsList()
		{
			return typeof(SkinPartsAccess).Assembly.GetTypes()
				.Where(t => t.GetCustomAttribute<SkinPartsAttribute>() != null)
				.Select(t => new KeyValuePair<Type, SkinPartsAttribute>(t, t.GetCustomAttribute<SkinPartsAttribute>()))
				.ToDictionary(a => a.Value.Id, a => a);
		}

		/// <summary>
		/// スキンパーツIDを元にスキンロジックを生成し返す
		/// </summary>
		/// <param name="id">スキンパーツID</param>
		/// <returns>スキンロジック</returns>
		public static ISkinLogic CreateSkinLogicInstance(int id)
		{
			// 正しくないものがきたら空処理を返しておく
			if (!IsCorrectSkinPartsId(id))
			{
				return m_DoNothingLogic;
			}
			return (ISkinLogic)Activator.CreateInstance(m_SkinParts[id].Value.LogicType);
		}

		/// <summary>
		/// すべてのスキンパーツのIDを一覧で取得する
		/// </summary>
		/// <returns>スキンパーツID</returns>
		public static int[] GetAllSkinPartsIds()
		{
			return m_SkinParts.Select(d => d.Key).OrderBy(id => id).ToArray();
		}

		/// <summary>
		/// スキンパーツIDを元にスキンパーツのクラスを取得する
		/// </summary>
		/// <param name="id">スキンパーツID</param>
		/// <returns>スキンパーツクラスの型</returns>
		public static Type GetSkinPartsRootType(int id)
		{
			return m_SkinParts[id].Key;
		}

		/// <summary>
		/// スキンパーツIDが正しいか取得する
		/// </summary>
		/// <param name="id">スキンパーツID</param>
		/// <returns>
		/// IDに一致するスキンパーツが存在する場合は真
		/// </returns>
		public static bool IsCorrectSkinPartsId(int id)
		{
			return m_SkinParts.ContainsKey(id);
		}

		#endregion

	}

}
