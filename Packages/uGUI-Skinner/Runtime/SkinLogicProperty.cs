using System.Collections.Generic;
using UnityEngine;

namespace Pspkurara.UI.Skinner
{

	/// <summary>
	/// スキンロジックのプロパティ群
	/// <seealso cref="ISkinLogic"/>
	/// </summary>
	public struct SkinLogicProperty : ISkinPartsProperty
	{

		#region 変数

		/// <summary>
		/// ロジックのデータを持つスキンパーツのプロパティ
		/// </summary>
		private SkinPartsPropertry m_SkinPartsProperty;

		/// <summary>
		/// ロジックのデータを持つスキンスタイル
		/// </summary>
		private SkinStyle m_ParentSkinStyle;

		#endregion

		#region コンストラクタ

		/// <summary>
		/// ロジック用のプロパティ群生成
		/// </summary>
		/// <param name="skinPartsProperty">スキンパーツのプロパティ</param>
		/// <param name="parentSkinStyle">スキンパーツの親</param>
		private SkinLogicProperty(SkinPartsPropertry skinPartsProperty, SkinStyle parentSkinStyle)
		{
			m_SkinPartsProperty = skinPartsProperty;
			m_ParentSkinStyle = parentSkinStyle;
		}

		/// <summary>
		/// ロジック用のプロパティ群生成
		/// </summary>
		/// <param name="skinParts">自身</param>
		/// <param name="parentSkinStyle">スキンパーツの親</param>
		internal SkinLogicProperty(SkinParts skinParts, SkinStyle parentSkinStyle) : this(skinParts.property, parentSkinStyle) { }

		/// <summary>
		/// ロジック用のプロパティ群生成
		/// </summary>
		/// <param name="skinParts">自身</param>
		internal SkinLogicProperty(SkinParts skinParts) : this(skinParts, null) { }

		/// <summary>
		/// ロジック用のプロパティ群生成
		/// </summary>
		/// <param name="skinParts">自身</param>
		internal SkinLogicProperty(SkinLogicProperty @base) : this(@base.m_SkinPartsProperty, @base.m_ParentSkinStyle) { }

		#endregion

		#region プロパティ

		/// <summary>
		/// Unityオブジェクト
		/// </summary>
		public List<Object> objectReferenceValues { get { return m_SkinPartsProperty.objectReferenceValues; } }

		/// <summary>
		/// 少数値
		/// </summary>
		public List<float> floatValues { get { return m_SkinPartsProperty.floatValues; } }

		/// <summary>
		/// Vector4
		/// </summary>
		public List<Vector4> vector4Values { get { return m_SkinPartsProperty.vector4Values; } }

		/// <summary>
		/// 文字列
		/// </summary>
		public List<string> stringValues { get { return m_SkinPartsProperty.stringValues; } }

		/// <summary>
		/// ロジックのデータを持つスキンスタイルが正しく設定されているかを返す
		/// </summary>
		public bool hasParentStyle { get { return m_ParentSkinStyle != null; } }

		/// <summary>
		/// ロジックのデータを持つスキンスタイルのスタイルキー
		/// 例外は null を返す
		/// <see cref="SkinStyle.styleKey"/>を参照
		/// </summary>
		public string styleKey { get { return hasParentStyle ? m_ParentSkinStyle.styleKey : null; } }

		/// <summary>
		/// ロジックのデータを持つスキンスタイルのインデックス
		/// 例外は -1 を返す
		/// <see cref="SkinStyle.styleIndex"/>を参照
		/// </summary>
		public int styleIndex { get { return hasParentStyle ? m_ParentSkinStyle.styleIndex : -1; } }

		#endregion

		#region メソッド

		/// <summary>
		/// 内部の<see cref="SkinPartsPropertry"/>を元の参照から切り離したものを取得する
		/// </summary>
		/// <param name="base">自身</param>
		/// <returns>独立した参照を持つバージョン</returns>
		internal static SkinLogicProperty GetCopyWithUniqueSkinPartsProperty(SkinLogicProperty @base)
		{
			var clone = new SkinLogicProperty(@base);
			clone.m_SkinPartsProperty = new SkinPartsPropertry(clone.m_SkinPartsProperty);
			return clone;
		}

		#endregion

	}

}
