# [1.9.0](https://github.com/pspkurara/ugui-skinner/compare/v1.8.0...v1.9.0) (2020-11-09)


### Bug Fixes

* クリーンアップでマルチオブジェクトでエラーが出ることがあるので修正 ([32ad59f](https://github.com/pspkurara/ugui-skinner/commit/32ad59ffe05148dca28720b5ae5cf523caeb1cb3))
* 複数オブジェクト編集時に配列サイズに差があると発生していたエラーを抑制、複数選択中のエディタを追加及び見栄えを改善 ([e2ca64e](https://github.com/pspkurara/ugui-skinner/commit/e2ca64ead33e2abaf6faa8c4d3a6d3980190b503))


### Features

* UISkinnerの循環参照チェックを追加、クリーンアップでもチェックして引っかかったら消去するようにした ([9d62706](https://github.com/pspkurara/ugui-skinner/commit/9d6270631a0a2cde3b42d0c15a76638c5c7c5847))

# [1.8.0](https://github.com/pspkurara/ugui-skinner/compare/v1.7.0...v1.8.0) (2020-11-08)


### Bug Fixes

* AddNewSkinPartsボタンやAddNewSkinStyleボタンを押下した際、1回目のみFoldoutを開き、それ以降同じボタンを連打した場合は閉じた状態で増やさせるようにした ([98a59b7](https://github.com/pspkurara/ugui-skinner/commit/98a59b70d698f1e7d3dd660ab5280127b78ebddd))


### Features

* Canvasのenabled設定に対応 ([8250c77](https://github.com/pspkurara/ugui-skinner/commit/8250c77442e0499803ed3b7462e4243fb358047a))
* スキンパーツ・各スキンパーツプロパティ ToStringでクラスの中身を出力可能にした ([71c3054](https://github.com/pspkurara/ugui-skinner/commit/71c30548582f5a2a9d9487aa890cd5028d8fe802))


### Performance Improvements

* クリーンアップの削除対象を強化 (まったく同じ設定のスキンパーツを消去対象に) ([41f1d0b](https://github.com/pspkurara/ugui-skinner/commit/41f1d0bfd2aaef605eff5cc027972ac75a8a86f0))

# [1.7.0](https://github.com/pspkurara/ugui-skinner/compare/v1.6.4...v1.7.0) (2020-11-06)


### Bug Fixes

* Enum初期化が可能なように修正 ([477b0d4](https://github.com/pspkurara/ugui-skinner/commit/477b0d49ddd633e96c517ad598eacce39b432339))
* Label等で数字が正常に表示されない不具合を修正 ([0aeac91](https://github.com/pspkurara/ugui-skinner/commit/0aeac912590550283cb9b22962ca2326d3040a5f))
* MaskFieldのEnumコンバートの不具合を修正 ([2d9d53e](https://github.com/pspkurara/ugui-skinner/commit/2d9d53e5048b90cb056a22904b472ebe998eb8de))
* コンポーネントインデックスで非アクティブオブジェクトも検出できるように修正 ([67b335c](https://github.com/pspkurara/ugui-skinner/commit/67b335c141f9bc0706a52c0356dc9ff9e53935f2))
* コンポーネントインデックスの判定が逆になっていた問題を修正 ([4436e7a](https://github.com/pspkurara/ugui-skinner/commit/4436e7a12b61a3cb272cba89be21da9e31990f5a))
* 全てのオブジェクトにRecordObjectが行われるように修正 ([78eb45f](https://github.com/pspkurara/ugui-skinner/commit/78eb45f88ebe8ab59d997fcded065dc04f65bed9))


### Features

* Enum エディター表記に対応 ([0b4b44c](https://github.com/pspkurara/ugui-skinner/commit/0b4b44cd4766d6ba74fbc24350546d4676806a36))
* Enumの簡易変換を追加 ([16f3ac5](https://github.com/pspkurara/ugui-skinner/commit/16f3ac5ac72418f02c4d3ef9a1a71927b4473a64))
* ScriptableLogic FieldTypeからFlagsAttributeを取得してMaskFieldに自動的に切り替わるように修正 ([740f1c7](https://github.com/pspkurara/ugui-skinner/commit/740f1c79556394aa4796460fd232de2a251bcefd))
* ScriptableLogic RectとLayerMaskに対応 ([6c3232f](https://github.com/pspkurara/ugui-skinner/commit/6c3232fe95344f796296c2e8d98805215a8d94bf))
* ScriptableLogic フィールド初期値に対応 ([4c33f1f](https://github.com/pspkurara/ugui-skinner/commit/4c33f1f69ff1db00b6e1841e04cfc91612dcabe8))
* ScriptableLogic プロパティドローAttributeに対応 ([a26aeff](https://github.com/pspkurara/ugui-skinner/commit/a26aeff86a47b9708aa819d69fa48f8d6076fe6b))
* クリーンアップ機能を強化(参照漏れ、使えないデータも削除するようにした) ([84b7c66](https://github.com/pspkurara/ugui-skinner/commit/84b7c66ba545151e27ee5ba3d6c75557b0134700))
* 配列表示以外の全てのObjectFieldでのコンポーネントインデックス表示に対応 ([66b737c](https://github.com/pspkurara/ugui-skinner/commit/66b737c94b02470025e334f49750c53b170eb525))

## [1.6.4](https://github.com/pspkurara/ugui-skinner/compare/v1.6.3...v1.6.4) (2020-11-06)


### Bug Fixes

* Inspector上でApplySkinを行った際に値が即時反映されない問題を修正 ([a8c0731](https://github.com/pspkurara/ugui-skinner/commit/a8c07311c7f9ef1378a4e1aa3820a54d3597dceb))
* ScriptableLogicが切り替えのたびにリセットされてしまう不具合を修正 ([1b996fb](https://github.com/pspkurara/ugui-skinner/commit/1b996fb237e8dfa6b24e9cf86dc34bc4078c7297))


### Performance Improvements

* ScriptableLogicを初期化する際に1フレーム待たなくてもよくした ([6733a15](https://github.com/pspkurara/ugui-skinner/commit/6733a15c35d576a41a2e680f95ad7830c8c4059f))

## [1.6.3](https://github.com/pspkurara/ugui-skinner/compare/v1.6.2...v1.6.3) (2020-10-09)


### Bug Fixes

* Array系インスペクターの削除ボタンが「追加」の見た目になっている問題を修正 ([55d285f](https://github.com/pspkurara/ugui-skinner/commit/55d285fb3da6e451af7a5eee78dd3db3646837e6))

## [1.6.2](https://github.com/pspkurara/ugui-skinner/compare/v1.6.1...v1.6.2) (2020-09-27)


### Bug Fixes

* ビルド後にシリアライズフィールドが破損する問題を修正 ([7ce7f44](https://github.com/pspkurara/ugui-skinner/commit/7ce7f446b2fa9fef883e9fcee5f995acb30f711d))

## [1.6.1](https://github.com/pspkurara/ugui-skinner/compare/v1.6.0...v1.6.1) (2020-09-16)


### Performance Improvements

* UserLogicでエラーが起こった際にそれ以降の処理をスキップさせるようにした ([f98ecd3](https://github.com/pspkurara/ugui-skinner/commit/f98ecd35f1427b51cede35c26e1dafe450eeaa9c))

# [1.6.0](https://github.com/pspkurara/ugui-skinner/compare/v1.5.0...v1.6.0) (2020-09-15)


### Bug Fixes

* charがリセットされた際にはcharが消えないように修正 ([c1b1454](https://github.com/pspkurara/ugui-skinner/commit/c1b1454f41bff2b8f4a6ce03f977a90c1cb31092))
* ScriptableLogic インスペクターでロジック差し替え時に発生するエラーを修正 ([b1f0919](https://github.com/pspkurara/ugui-skinner/commit/b1f09190f565b01b941be454c81c7f7934ab4c0e))
* 配列サイズの違うSkinnerを複数選択した際に出るエラーを抑制 ([c0182bc](https://github.com/pspkurara/ugui-skinner/commit/c0182bc0b7da40d4eb1da79107c842556bb569c4))


### Features

* SkinPartsAttribute ルートタイプの指定を不要にした ([b7226c3](https://github.com/pspkurara/ugui-skinner/commit/b7226c33b0c9774aba57e29163947d00f3266047))

# [1.5.0](https://github.com/pspkurara/ugui-skinner/compare/v1.4.0...v1.5.0) (2020-09-09)


### Bug Fixes

* ScriptableLogicのクリーンアップが正しく動作しなかった問題を修正 ([35fa527](https://github.com/pspkurara/ugui-skinner/commit/35fa527759cf0585b79736d886effb08e212411e))
* ユーザーロジックの細かい初期化の問題修正 ([0d5c521](https://github.com/pspkurara/ugui-skinner/commit/0d5c521fa81554f1533d555a5b668861830a9439))
* 文字列のインスペクター表示がなかったので追加 ([578fe8d](https://github.com/pspkurara/ugui-skinner/commit/578fe8d584f7b14d3e7c7948d5deff9cdf0f3c3d))


### Features

* ScriptableLogicのユーザー変数ラベルが空の場合は型の名前を出しておくように変更 ([5e66e65](https://github.com/pspkurara/ugui-skinner/commit/5e66e651392628245ead5d81e659a9538afb087b))
* UserLogic ロジックオブジェクトのインスペクターにSkinParts上の表示サンプルが出る機能を追加 ([4cc0254](https://github.com/pspkurara/ugui-skinner/commit/4cc02549ee42b9aa9c19539c528a36b91e0acc9b))
* UserLogic.GetValueIndexを廃止し、代わりにUserLogicExtensionに糖衣構文を追加 ([c9625b5](https://github.com/pspkurara/ugui-skinner/commit/c9625b5a2625b2cbbc1d9e851a2f21f1b74de350))
* UserLogicのユーザー変数 そのままキーに使うように変更 ([dd5adad](https://github.com/pspkurara/ugui-skinner/commit/dd5adadafd93b8d3905ac7ffb9fe5b173bcc0a82))
* ユーザー変数にIDを持たせてインデックスを安全に取得できるようにした ([d60e4d7](https://github.com/pspkurara/ugui-skinner/commit/d60e4d7cebed40f8a277cc7636d2173d51fcd8bc))

# [1.4.0](https://github.com/pspkurara/ugui-skinner/compare/v1.3.0...v1.4.0) (2020-09-09)


### Features

* 文字列保存を可能にした ([df851b8](https://github.com/pspkurara/ugui-skinner/commit/df851b85561ed97010a61c57412079b7b0521dee))

# [1.3.0](https://github.com/pspkurara/ugui-skinner/compare/v1.2.5...v1.3.0) (2020-09-09)


### Bug Fixes

* Foldout の状態保存を EditorPrefs から SerializedProperty.isExpanded に切り替え ([17de2e8](https://github.com/pspkurara/ugui-skinner/commit/17de2e813cc472f2f8c64acf504cb029e776d19d))
* Unity2019用に必要モジュール追加 ([fb43b1d](https://github.com/pspkurara/ugui-skinner/commit/fb43b1dea818bff2700525fddbec2a41a07b736c))
* インスペクターのFoldoutでラベルクリックでもドロップダウンが開くように修正 ([4a12766](https://github.com/pspkurara/ugui-skinner/commit/4a12766a6ce0f63527d29d41c360f11f698d809b))
* インスペクターのアクセス修飾子を統一 ([adc8bc6](https://github.com/pspkurara/ugui-skinner/commit/adc8bc6c76af595bb3392dcf98bc3156721e4079))
* 開発用Unityバージョンを2019.4.9f1に変更 ([c67d694](https://github.com/pspkurara/ugui-skinner/commit/c67d69457834346acae813e8a1c3d2a84ac89113))


### Features

* AnimationSampleを追加 ([36469f0](https://github.com/pspkurara/ugui-skinner/commit/36469f0e50741f3fd2dac689932e70410984cbbc))
* ScriptableObjectで挙動を追加できるScriptableLogicを追加 ([3f333a9](https://github.com/pspkurara/ugui-skinner/commit/3f333a908e418a999b43a2d1db5320088112e5c0))
* スキンパーツIDが不正なときのエラー処理を追加 ([3e07484](https://github.com/pspkurara/ugui-skinner/commit/3e07484c4153e5f41b0f1c78f4cb4e3152454a14))

## [1.2.5](https://github.com/pspkurara/ugui-skinner/compare/v1.2.4...v1.2.5) (2020-09-08)


### Bug Fixes

* uGUIモジュール追加 ([74768b2](https://github.com/pspkurara/ugui-skinner/commit/74768b2373c4197e86c00b044d323eef8e0e4414))

## [1.2.4](https://github.com/pspkurara/ugui-skinner/compare/v1.2.3...v1.2.4) (2020-09-08)


### Bug Fixes

* .Editorのasmdefをエディターのみにする ([ca50138](https://github.com/pspkurara/ugui-skinner/commit/ca50138f15c24b4e81c09e51583fc3ac791b53e5))
* プリプロセッサで囲わないと動かない可能性がある ([5ad836d](https://github.com/pspkurara/ugui-skinner/commit/5ad836d71ba8a03b4be6f4bf87c4d838e9313e45))

## [1.2.3](https://github.com/pspkurara/ugui-skinner/compare/v1.2.2...v1.2.3) (2020-09-08)


### Bug Fixes

* エディタのFoldoutの位置が切り替えるたびにずれる問題を修正 ([4d4a9a6](https://github.com/pspkurara/ugui-skinner/commit/4d4a9a67915cb35c2e6ecf03e6bc9f5c51180674))

## [1.2.2](https://github.com/pspkurara/ugui-skinner/compare/v1.2.1...v1.2.2) (2020-09-08)


### Bug Fixes

* name fix ([bab6545](https://github.com/pspkurara/ugui-skinner/commit/bab654526f0c622de0d046588eaa8505d7cec903))

## [1.2.1](https://github.com/pspkurara/ugui-skinner/compare/v1.2.0...v1.2.1) (2020-09-07)


### Bug Fixes

* Add New Skin Object -> Add New Skin Style ([b532a41](https://github.com/pspkurara/ugui-skinner/commit/b532a418d50884cb232aa3b864e67990d47ebb7c))
* GraphicColorのインスペクター オブジェクトフィールドのタイプが間違っていたので修正 ([d9f353d](https://github.com/pspkurara/ugui-skinner/commit/d9f353daec58530dd0e6ce8d6d92ef55ef2872de))

# [1.2.0](https://github.com/pspkurara/ugui-skinner/compare/v1.1.3...v1.2.0) (2020-09-07)


### Bug Fixes

* Vector4のリセットが抜けていたので修正 ([e520b25](https://github.com/pspkurara/ugui-skinner/commit/e520b25b1cd24d9a9a50844f91630cd6174edbbc))


### Features

* TransformRotationを追加 ([d6e2b88](https://github.com/pspkurara/ugui-skinner/commit/d6e2b8839d67fa37c2252402018be638c014dfbe))

## [1.1.3](https://github.com/pspkurara/ugui-skinner/compare/v1.1.2...v1.1.3) (2020-09-07)


### Bug Fixes

* スキンを生成する関数名はSkinnerでなくSkinで統一すべきである ([db2e443](https://github.com/pspkurara/ugui-skinner/commit/db2e44318b8c8eaea2219aa1a3925b99350d3ad3))

## [1.0.1](https://github.com/pspkurara/ugui-skinner/compare/v1.0.0...v1.0.1) (2020-09-07)


### Bug Fixes

* 自由サイズの配列スキンでクリーンアップ時に正しいオブジェクトも消していた問題を修正 ([0350ebe](https://github.com/pspkurara/ugui-skinner/commit/0350ebe8c39cc401480f30df274bd303a3d65fac))
