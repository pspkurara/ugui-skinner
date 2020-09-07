using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// 全てのスキンパーツインスペクターを探せるアクセスクラス
	/// </summary>
	/// <seealso cref="SkinPartsAccess"/>
	internal static class SkinPartsInspectorAccess
	{

		#region 変数

		/// <summary>
		/// スキンパーツの型とインスペクターを紐付ける一覧
		/// スキンパーツが増えたら随時追加すること
		/// </summary>
		private static readonly Dictionary<Type, ISkinPartsInspector> m_SkinPartsInspectors = CreateSkinPartsInspectorList();

		#endregion

		#region メソッド

		/// <summary>
		/// 該当属性を持つスキンパーツインスペクタークラスを全取得してリストアップ
		/// </summary>
		private static Dictionary<Type, ISkinPartsInspector> CreateSkinPartsInspectorList()
		{
			return typeof(SkinPartsInspectorAccess).Assembly.GetTypes()
				.Where(t => t.GetCustomAttribute<SkinPartsInspectorAttribute>() != null)
				.ToDictionary(t => t.GetCustomAttribute<SkinPartsInspectorAttribute>().RootType, t => (ISkinPartsInspector)Activator.CreateInstance(t));
		}

		/// <summary>
		/// スキンパーツの型に応じたインスペクターを取得する
		/// </summary>
		/// <param name="rootType">スキンパーツクラスの型</param>
		/// <returns>スキンパーツインスペクター</returns>
		public static ISkinPartsInspector GetSkinInspector(Type rootType)
		{
			return m_SkinPartsInspectors[rootType];
		}

		#endregion

	}

}
