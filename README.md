# MiniMartialWorld-CustomCharacters

> 为《Mini Martial World》制作自定义角色的入门模板。  
> 标准流程：**在 `DLLBuilder` 编译出你的 `*.dll` → 放进 `ContentSample` 里作为示例包 → 上传到 Steam 创意工坊**。

---

## 目录结构

```
MiniMartialWorld-CustomCharacters
├─ DLLBuilder/           # 你的 C# Mod 工程（编译成 .dll）
├─ ContentSample/        # 打包示例（放置编译好的 .dll 与预览图）
└─ README.md             # 本说明（你正在看）
```

---

## （一分钟上手）

1. 克隆仓库并用 Visual Studio / Rider 打开 `DLLBuilder`。  
2. 把你的自定义代码（技能/事件/效果）放进 `DLLBuilder` 并 **Release 构建**。  
3. 将生成的 `.dll` 复制到 `ContentSample/<你的包名>/`。  
4. 打开游戏的创意工坊发布器，选择上述文件夹上传即可。

> 备注：如果你只想给别人一个“可直接订阅使用”的示例包，把 `.dll` + 预览图放在 `ContentSample` 的子文件夹里上传就好。

---

## 环境与要求

- **开发工具**：Visual Studio 2022（或 Rider / VS Code + C# 扩展）。  
---

## 创建你的第一个 Mod

### 1) 最小技能示例

在 `DLLBuilder` 工程中新建 `MyFirstMod.cs`，添加：

```csharp
using System.Collections.Generic;
using UnityEngine;

// ① 一个最小的技能构建器（把技能注册进游戏的技能库）
public class MyFirstSkillBuilder : SkillFactory
{
    public override void BuildSkill()
    {
        var skills = new List<Skill>();

        // 用内置“执行型技能”快速造一个基础技能
        skills.Add(new EffectExcecuteSkillPro("新手·直刺", Quality.Common)
        {
            DamageRate = 1.2f,
            ManaCost = 100,
            CD = 2,
            Discription = "简单的一击。",
            AttributeSelector = s =>
            {
                // BaseExecute 内部会处理命中/暴击/伤害等流程
                s.BaseExecute();
            }
        });

        // 注册到全局技能列表
        SkillBuilder.SkillList.AddRange(skills);
    }
}
```

### 2) 最小事件示例（可战斗）

```csharp
using System.Collections.Generic;

[System.Serializable]
public class AlleyThugEvent : FightEvent
{
    public override string getName()
    {
        Quality = Quality.Common;
        return "巷口劫匪★";
    }

    public override string getDescription() => "随手练练手。";

    public override void onLoad()
    {
        base.onLoad();

        var enemy = Controller.FightController.GetCharacter(2500);
        enemy.PersonalData.FamilyName = "劫";
        enemy.PersonalData.SecondName = "匪";
        enemy.PersonalData.Gender = 1;

        // 敌人使用你刚注册的技能
        enemy.CombatSkills = new List<int>
        {
            SkillBuilder.GetSkillIndexByName("新手·直刺")
        };

        EnermyList = new List<Character> { enemy };
    }

    public override void execute()
    {
        DisplayControl.HaveFight(EnermyList, Win, null);
        DisplayControl.ShowFight();
    }

    void Win() { /* 掉落/奖励可写在这里 */ }

    public override string getImageName() => "巷口劫匪"; // 与游戏内资源名匹配
    public override float getWeight() => 1f;            // 触发权重
}
```

> 进阶：你也可以仿照项目里的效果系统（如中毒、护甲、会心、Boss 专属加成等）组合出更多花样：
> - **Effect**：可在 `OnInit / OnTurnStart / OnTurnEnd / BeHit / Recover / RecoverMana` 等回调里实现持续回合、叠层、正负面效果等；
> - **战术/门派/专属**：通过不同的效果派生类做差异化；
> - **技能建造器**：集中注册技能，便于维护与开关。

---

## 构建 DLL

1. 在 IDE 中将 `DLLBuilder` 切换为 **Release**；目标框架设为 **.NET Framework 4.x**。  
2. **生成解决方案**，得到 `YourModName.dll`（通常在 `DLLBuilder/bin/Release/`）。

---

## 打包到 `ContentSample`

1. 在 `ContentSample/` 下新建一个**你的 Mod 包名**的文件夹，例如：
   ```
   ContentSample/
   └─ MyFirstMod/
      ├─ MyFirstMod.dll
      ├─ preview.png        # 创意工坊封面（可选，建议 16:9 < 1MB）
      └─ readme.txt         # 额外说明（可选）
   ```
2. 将刚编译好的 `.dll` 复制到该文件夹。

> 这个目录就是你上传到创意工坊时选择的“内容目录”。

---

## 上传到 Steam 创意工坊

> 以游戏内置发布器 / 官方上传器为准（界面可能不同）。

1. 打开 **创意工坊上传**。  
2. 选择内容目录：指向 `ContentSample/<你的包名>/`。  
3. 填写标题、描述、标签；添加预览图。  
4. 选择公开/非公开并发布。  
5. **更新版本**：替换 `.dll`，在上传器中点击“更新”即可。

---

## 调试与排错

- **类型未发现 / 内容未注入**
  - 类必须 `public`、**非抽象**；
  - 保证目标框架为 .NET Framework 4.x；
  - 名称/命名空间不影响扫描，但避免与游戏内部类**完全同名**造成混淆。
- **运行时报错**
  - 在关键逻辑处加入 `Debug.Log(...)`；
  - 用 `try/catch` 包裹高风险代码并打印堆栈；
  - 逐步注释定位出问题的效果/技能。
- **预览图或更新失败**
  - 路径不要含空格/中文；
  - 预览图 PNG/JPG，体积 < 1MB；
  - 确保有写入权限。

---

## 贡献

欢迎 PR：补充更多模板、工具脚本与示例素材。  
如果你在使用中遇到问题，也欢迎提 Issue 反馈。


---

## 变更日志

- `v1.0.0`：初始化 README 与最小示例。
