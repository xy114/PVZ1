# PVZ_Arknights - 明日方舟风格植物大战僵尸

一款结合了植物大战僵尸玩法与明日方舟风格UI的塔防游戏。

## 项目特色

- **经典塔防玩法**：放置植物抵御僵尸入侵
- **明日方舟风格UI**：深色科幻主题，霓虹色调
- **角色皮肤系统**：植物可切换明日方舟角色皮肤，改变技能和属性
- **阶段波次系统**：每关卡分为前期、中期、后期三个阶段
- **多元敌人**：包含PVZ和明日方舟世界观的敌人
- **阳光系统**：阳光掉落收集，3秒变淡，6秒消失

## 核心玩法

### 战斗机制
- 选择6种植物进入战斗
- 使用阳光放置植物
- 植物自动攻击僵尸
- 阻止僵尸到达防线左侧

### 关卡系统
- 前期：僵尸密度较低，适合布置防线
- 中期：僵尸密度增加，出现更强敌人
- 后期：僵尸密度最高，高强度战斗

### 角色系统
- 每种植物拥有多个皮肤
- 切换皮肤可改变技能和属性
- 皮肤主题为明日方舟角色

## 项目结构

```
PVZ_Arknights/
├── Assets/
│   ├── Scripts/
│   │   ├── Core/           # 核心框架
│   │   │   ├── GameManager.cs
│   │   │   ├── EventManager.cs
│   │   │   ├── SceneLoader.cs
│   │   │   └── GameDataManager.cs
│   │   ├── Plants/         # 植物系统
│   │   │   ├── Plant.cs
│   │   │   ├── PlantData.cs
│   │   │   ├── PlantSkin.cs
│   │   │   └── PlantSkill.cs
│   │   ├── Zombies/        # 僵尸系统
│   │   │   ├── Zombie.cs
│   │   │   └── ZombieData.cs
│   │   ├── Sunlight/       # 阳光系统
│   │   │   ├── Sunlight.cs
│   │   │   └── SunlightSpawner.cs
│   │   ├── Level/          # 关卡系统
│   │   │   ├── LevelManager.cs
│   │   │   ├── WaveManager.cs
│   │   │   └── LevelData.cs
│   │   ├── UI/             # UI系统
│   │   │   ├── MainMenuUI.cs
│   │   │   ├── LevelSelectUI.cs
│   │   │   ├── CharacterSelectUI.cs
│   │   │   ├── BattleUI.cs
│   │   │   ├── BattleManager.cs
│   │   │   └── ScrollRectEnhanced.cs
│   │   └── Audio/          # 音频系统
│   │       └── AudioManager.cs
│   ├── Prefabs/            # 预制件
│   ├── Resources/          # 资源文件
│   └── Scenes/             # 场景
└── ProjectSettings/        # 项目设置
```

## 游戏场景

1. **主菜单**：开始游戏、关卡选择、角色选择、设置
2. **关卡选择**：选择要挑战的关卡
3. **角色选择**：选择出战植物及皮肤
4. **战斗场景**：核心战斗界面

## 操作说明

### 鼠标操作
- **左键点击**：选择植物卡片、放置植物、收集阳光
- **右键点击**：取消选择植物
- **滚轮滚动**：在列表页面滚动
- **拖拽**：在列表页面滑动

### 快捷键
- **ESC**：暂停游戏

## 开发环境

- Unity 2022.3.x
- .NET Framework 4.x
- 支持Windows平台

## 构建步骤

1. 使用Unity Hub打开项目
2. 导入必要的资源文件（Sprite、Audio等）
3. 创建场景并配置UI
4. 设置Build Settings中的场景列表
5. 构建Windows版本

## 注意事项

1. 需要手动创建Unity场景文件
2. 需要添加Sprite资源和Audio资源
3. 需要配置Layer和Tag
4. 需要设置碰撞层

## License

MIT License
