# TogglePauseButton

[![Build and Release](https://github.com/denpadokei/TogglePauseButton/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/denpadokei/TogglePauseButton/actions/workflows/build-and-release.yml)

ポーズボタン (メニューボタン) の挙動を扱いやすいように作り直す MOD です。押下の検出とポーズメニューの更新処理を独自実装に置き換えます。

## 機能

- `InstantMenuButtonTrigger.Tick` / `DelayedMenuButtonTrigger.Tick` を置き換え、メニューボタンの押下を自前で判定する
- 押下時間と前回値を `ButtonStateEntity` に保持し、同じ押下でイベントが連続して発火しないようにする
- ポーズメニュー表示中にメニューボタンを押すと再開 (Continue) する
- ポーズメニュー表示直後の `disabledInteractionRemainingTime` の待ち時間を維持する
- キーボードでも操作できる: `R` でリスタート、`M` でメニューへ、`C` で再開

## 動作環境

| 項目 | 内容 |
| --- | --- |
| Beat Saber | 1.30.2 |
| 依存 MOD | BSIPA ^4.3.0 |

## インストール

1. Beat Saber を 1.30.2 に合わせる (BSManager などのバージョン管理ツールを推奨)
2. BSIPA と上記の依存 MOD をインストールする
3. [Releases](https://github.com/denpadokei/TogglePauseButton/releases) から `TogglePauseButton-0.0.1-bs1.30.2.zip` をダウンロードし、`Plugins` フォルダをゲームフォルダへ上書きコピーする

## 使い方

ゲーム内に設定はありません。ポーズメニューを開いた状態でメニューボタンを押すと再開します。ポーズメニュー表示中はキーボードの `R` / `M` / `C` でも操作できます。

## ビルド

```powershell
git clone https://github.com/denpadokei/TogglePauseButton.git
cd TogglePauseButton
dotnet build .\TogglePauseButton\TogglePauseButton.csproj -c Release
```

`BeatSaberDir` (または `GameDirectory`) に Beat Saber のインストール先を設定すると、ゲームの参照アセンブリを解決できます。

GitHub Actions の `build-and-release.yml` が push ごとにビルドし、`X.X.X` 形式のタグを push するとリリースを作成します。

## ライセンス

MIT ライセンスです。詳細は [LICENSE.txt](LICENSE.txt) を参照してください。
