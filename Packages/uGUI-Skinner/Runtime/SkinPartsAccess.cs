using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
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
		/// スキンパーツが増えたら随時追加すること
		/// </summary>
		private static readonly Dictionary<int, SkinPartsAttribute> m_SkinParts = CreateSkinPartsList();

		#endregion

		#region メソッド

		/// <summary>
		/// 該当属性を持つスキンパーツクラスを全取得してリストアップ
		/// </summary>
		private static Dictionary<int, SkinPartsAttribute> CreateSkinPartsList()
		{
			return typeof(SkinPartsAccess).Assembly.GetTypes()
				.Where(t => t.GetCustomAttribute<SkinPartsAttribute>() != null)
				.Select(t => t.GetCustomAttribute<SkinPartsAttribute>())
				.ToDictionary(a => a.Id, a => a);
		}

		/// <summary>
		/// スキンパーツIDを元にスキンロジックを生成し返す
		/// </summary>
		/// <param name="id">スキンパーツID</param>
		/// <returns>スキンロジック</returns>
		public static ISkinLogic CreateSkinLogicInstance(int id)
		{
			return (ISkinLogic)Activator.CreateInstance(m_SkinParts[id].LogicType);
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
			return m_SkinParts[id].RootType;
		}

		#endregion

	}

}
