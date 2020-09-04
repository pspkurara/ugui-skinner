using System.Collections.Generic;
using System;

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
		private static readonly Dictionary<Type, ISkinPartsInspector> m_SkinPartsInspectors = new Dictionary<Type, ISkinPartsInspector>()
		{
			{ typeof(ObjectSetActives),		CreateInstance<ObjectSetActivesInspector>()		},
			{ typeof(GraphicColor),			CreateInstance<GraphicColorInspector>()			},
			{ typeof(CanvasGroupAlpha),		CreateInstance<CanvasGroupAlphaInspector>()		},
			{ typeof(ImageSprite),			CreateInstance<ImageSpriteInspector>()			},
			{ typeof(RawImageTexture),		CreateInstance<RawImageTextureInspector>()		},
			{ typeof(GraphicMaterial),		CreateInstance<GraphicMaterialInspector>()		},
			{ typeof(ShadowColor),			CreateInstance<ShadowColorInspector>()			},
			{ typeof(OutlineColor),			CreateInstance<OutlineColorInspector>()			},
			{ typeof(BaseMeshEffectEnable), CreateInstance<BaseMeshEffectEnableInspector>() },
			{ typeof(GraphicEnable),        CreateInstance<GraphicEnableInspector>()		},
			{ typeof(TransformScale),       CreateInstance<TransformScaleInspector>()       },
		};

		#endregion

		#region メソッド

		/// <summary>
		/// スキンパーツの型に応じたインスペクターを取得する
		/// </summary>
		/// <param name="rootType">スキンパーツクラスの型</param>
		/// <returns>スキンパーツインスペクター</returns>
		public static ISkinPartsInspector GetSkinInspector(Type rootType)
		{
			return m_SkinPartsInspectors[rootType];
		}

		/// <summary>
		/// スキンパーツインスペクターを生成して返す
		/// </summary>
		private static ISkinPartsInspector CreateInstance<T>() where T : ISkinPartsInspector
		{
			return (ISkinPartsInspector)Activator.CreateInstance(typeof(T));
		}

		#endregion

	}

}
