# Build (стан робочих модулів)

## Останній запуск (Release)
Збірка виконувалась окремо по проєктах (Release), щоб швидко перевірити зміни.

### Успішно зібрались і скопійовані в Beat Saber `Plugins`
- `ChatPlexSDK_BS`
- `Modules/BeatSaberPlus_ChatEmoteRain`
- `Modules/BeatSaberPlus_ChatRequest`
- `Modules/BeatSaberPlus_Chat`
- `Modules/BeatSaberPlus_GameTweaker`
- `Modules/BeatSaberPlus_NoteTweaker`
- `Modules/BeatSaberPlus_SongChartVisualizer`
- `Modules/BeatSaberPlus_ChatIntegrations`

### Попередження (не блокують білд)
- `MSB3101` про `AssemblyReference.cache` (Access denied) у `BeatSaberPlus_ChatIntegrations`.

### Примітки
- `PrivateModules/BeatSaberPlus_CustomEdition` прибрано з solution: `.csproj` відсутній на диску.
- 31.01.2026: `build.ps1` без параметра `-Project` для `BeatSaberPlus.sln` повернув `FAILED` без діагностики; збірку перевірено через `MSBuild.exe` напряму.

## Команда (ручний запуск)
```powershell
& 'C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe' Modules\BeatSaberPlus_ChatIntegrations\BeatSaberPlus_ChatIntegrations.csproj /t:Build /p:Configuration=Release /m:1 /v:minimal
```

## Скрипт для запуску
- `build.ps1` / `build.cmd` — запуск збірки; логи в `build\logs\`.
