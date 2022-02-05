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

		#endregion

		#region コンストラクタ

		/// <summary>
		/// ロジック用のプロパティ群生成
		/// </summary>
		/// <param name="skinPartsProperty">スキンパーツのプロパティ</param>
		/// <param name="parentSkinStyle">スキンパーツの親</param>
		/// <param name="applyTrace">呼び出してきたスキナー</param>
		private SkinLogicProperty(SkinPartsPropertry skinPartsProperty, SkinStyle parentSkinStyle, Stack<ISkinStyleParent> applyTrace)
		{
			m_SkinPartsProperty = skinPartsProperty;
			this.parentSkinStyle = parentSkinStyle;
			this.applyTrace = applyTrace;
		}

		/// <summary>
		/// ロジック用のプロパティ群生成
		/// </summary>
		/// <param name="skinParts">自身</param>
		/// <param name="parentSkinStyle">スキンパーツの親</param>
		/// <param name="applyTrace">呼び出してきたスキナー</param>
		internal SkinLogicProperty(SkinParts skinParts, SkinStyle parentSkinStyle, Stack<ISkinStyleParent> applyTrace) : this(skinParts.property, parentSkinStyle, applyTrace) { }

		/// <summary>
		/// ロジック用のプロパティ群生成
		/// </summary>
		/// <param name="skinParts">自身</param>
		internal SkinLogicProperty(SkinParts skinParts) : this(skinParts, null, null) { }

		/// <summary>
		/// ロジック用のプロパティ群生成
		/// </summary>
		/// <param name="skinParts">自身</param>
		internal SkinLogicProperty(SkinLogicProperty @base) : this(@base.m_SkinPartsProperty, @base.parentSkinStyle, @base.applyTrace) { }

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
		public bool hasParentStyle { get { return parentSkinStyle != null; } }

		/// <summary>
		/// ロジックのデータを持つスキンスタイルのスタイルキー
		/// 例外は null を返す
		/// <see cref="SkinStyle.styleKey"/>を参照
		/// </summary>
		public string styleKey { get { return hasParentStyle ? parentSkinStyle.styleKey : null; } }

		/// <summary>
		/// ロジックのデータを持つスキンスタイルのインデックス
		/// 例外は -1 を返す
		/// <see cref="SkinStyle.styleIndex"/>を参照
		/// </summary>
		public int styleIndex { get { return hasParentStyle ? parentSkinStyle.styleIndex : -1; } }

		/// <summary>
		/// 親となるスキンスタイル
		/// </summary>
		internal SkinStyle parentSkinStyle { get; }

		/// <summary>
		/// 呼び出してきたスキナー
		/// </summary>
		internal Stack<ISkinStyleParent> applyTrace { get; }

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
