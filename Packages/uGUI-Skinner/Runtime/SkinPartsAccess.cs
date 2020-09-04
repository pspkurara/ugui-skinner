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
		private static readonly Dictionary<int, SkinPartsAttribute> m_SkinParts = new Dictionary<int, SkinPartsAttribute>()
		{
			{ (int)SkinPartsType.ObjectsSetActives,		GetAttribute(typeof(ObjectSetActives)) },
			{ (int)SkinPartsType.GraphicColor,			GetAttribute(typeof(GraphicColor)) },
			{ (int)SkinPartsType.CanvasGroupAlpha,		GetAttribute(typeof(CanvasGroupAlpha)) },
			{ (int)SkinPartsType.ImageSprite,			GetAttribute(typeof(ImageSprite)) },
			{ (int)SkinPartsType.RawImageTexture,		GetAttribute(typeof(RawImageTexture)) },
			{ (int)SkinPartsType.GraphicMaterial,		GetAttribute(typeof(GraphicMaterial)) },
			{ (int)SkinPartsType.ShadowColor,			GetAttribute(typeof(ShadowColor)) },
			{ (int)SkinPartsType.OutlineColor,			GetAttribute(typeof(OutlineColor)) },
			{ (int)SkinPartsType.BaseMeshEffectEnable,	GetAttribute(typeof(BaseMeshEffectEnable)) },
			{ (int)SkinPartsType.GraphicEnable,			GetAttribute(typeof(GraphicEnable)) },
			{ (int)SkinPartsType.TransformScale,        GetAttribute(typeof(TransformScale)) },
		};

		#endregion

		#region メソッド

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
			return m_SkinParts.Select(d => d.Key).ToArray();
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

		/// <summary>
		/// <see cref="SkinPartsAttribute"/>属性を取得する
		/// </summary>
		/// <param name="rootType">親クラスの型</param>
		/// <returns>属性</returns>
		private static SkinPartsAttribute GetAttribute(Type rootType)
		{
			var attribute = rootType.GetCustomAttribute<SkinPartsAttribute>();
			return attribute;
		}

		#endregion

	}

}
