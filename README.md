# MiniMartialWorld-CustomCharacters

> 为《Mini Martial World》制作自定义角色的入门模板。  
> 标准流程：**在 `DLLBuilder` 编译出你的 `*.dll` → 放进 `ContentSample` 里作为示例包 → 上传到 Steam 创意工坊**。

---

## 目录结构

```
MiniMartialWorld-CustomCharacters
├─ DLLBuilder/           # 你的 C# Mod 工程（编译成 .dll）
├─ ContentSample/        # 创意工坊示例（放置编译好的 .dll 与预览图）
└─ README.md             # 本说明（你正在看）
```

---

## （一分钟上手）

1. 克隆仓库并用 Visual Studio / Rider 打开 `DLLBuilder`。  
2. 把你的自定义代码（技能/事件/效果）放进 `DLLBuilder` 并 **构建**。  
3. 将生成的 `.dll` 放到ContentSample/并删除原有的`.dll`。
4. 参见资源替换教程，补全图片素材。
5. 打开游戏的创意工坊发布器，选择上述文件夹上传即可。

> 备注：如果你只想给别人一个“可直接订阅使用”的示例包，把 `.dll` 放在 `ContentSample` 的子文件夹里上传就好。

---

## 这是什么接口？

```csharp
public interface IWorkshopCharacterModifier
{
    List<Character> WorkShopModifyCharacters(List<Character> characters, string challengeCode);
}
```

- **调用时机**：游戏加载你的 Workshop 内容时被调用。  
- **输入**：`characters` 为当前候选角色列表；`challengeCode` 为挑战/模式代码（如“无尽模式/金塔”等）。  
- **输出**：返回 **最终要生效** 的角色列表。你可以在里面**添加、删除或替换**角色。

> 实现该接口的 `public`、**非抽象**类会被自动反射发现；无需手工注册。
---

---

## 3. 示例：MyCharacterSpawnTweaks（可直接改名使用）

> 功能：**非“一掷千金（Golden Tower）挑战”**时，增加一个自定义角色，并将其设置为玩家。还会给角色添加一个来自效果的“关键天赋”。

```csharp
using System.Collections.Generic;

public class MyCharacterSpawnTweaks : IWorkshopCharacterModifier
{
    public List<Character> WorkShopModifyCharacters(List<Character> characters, string challengeCode)
    {
        // 仅在非“金塔挑战”下生效，避免影响该模式
        if (challengeCode != GoldenTowerControl.GetChallengeModeName())
        {
            var me = BuildCustomPlayer();
            characters.Add(me);
        }
        return characters;
    }

    private static Character BuildCustomPlayer()
    {
        var c = new Character();

        // ====== 属性面板（可按需调整） ======
        c.AttributePart.Charm         = 5.0f;
        c.AttributePart.Strength      = 4.0f;
        c.AttributePart.Physique      = 6.0f;
        c.AttributePart.Speed         = 6.5f;
        c.AttributePart.Comprehension = 5.0f;
        c.AttributePart.Concentrate   = 4.0f;

        // ====== 个人数据 ======
        c.PersonalData.FamilyName = "测试";
        c.PersonalData.SecondName = "人员";
        c.PersonalData.Gender     = 1;  0表示女性1表示男性
        c.PersonalData.SpriteName = c.PersonalData.GetFullName(); // 用全名做立绘/头像 Key（与资源匹配）

        // ====== 玩家标记与唯一识别码 ======
        c.Identifier = "CESHIRENYUAN";  // 建议全大写，唯一
        Controller.GameData.Player = c; // 将该角色设置为“玩家”

        // ====== 添加关键天赋（基于效果） ======
        // HanZhangEffect 需在你的 DLL 中可用；若没有，请替换为你自己的 Effect 类
        var keyTalent = new TalentFromEffect(new HanZhangEffect());
        keyTalent.IsBuff = true;
        TalentManager.AddTalent(keyTalent);

        return c;
    }
}
```
## 环境与要求

- **开发工具**：Visual Studio 。  


## 贡献

欢迎 PR：补充更多模板、工具脚本与示例素材。  
如果你在使用中遇到问题，也欢迎提 Issue 反馈。

