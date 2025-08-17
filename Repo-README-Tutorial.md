# MiniMartialWorld 自定义角色：构建与发布教程

> 本教程面向**创意工坊作者**与**模组制作者**，手把手教你：用仓库中的 **DLLBuilder** 生成自定义 DLL，将其放入 **ContentSample** 样例结构，最终发布到创意工坊。  
> 仓库地址：<https://github.com/MiniFunGame/MiniMartialWorld-CustomCharacters>

---

## 目录
- [环境准备](#环境准备)
- [仓库结构速览](#仓库结构速览)
- [用 DLLBuilder 生成你的 DLL](#用-dllbuilder-生成你的-dll)
- [放入 ContentSample 并准备发布](#放入-contentsample-并准备发布)
- [游戏内验证（强烈建议）](#游戏内验证强烈建议)
- [常见问题 FAQ](#常见问题-faq)
- [进阶：自定义内容建议](#进阶自定义内容建议)

---

## 环境准备
- **开发工具**：Visual Studio 2019/2022 或 Rider（任选）。
- **项目框架**：与游戏一致（通常 .NET Framework 4.x）。
- **游戏相关引用**（仅当你在 DLLBuilder 中新建/改代码时需要）：
  - `Assembly-CSharp.dll`（游戏主程序集）
  - `UnityEngine*.dll`（Unity 运行库）  
  以上 DLL 通常位于 `游戏安装目录/xxx_Data/Managed/`。

> 如果你的 DLLBuilder 已预置引用，可直接构建；若编译报缺失引用，请在项目中添加以上 DLL 的引用。

---

## 仓库结构速览
仓库包含两个关键目录：

```
/DLLBuilder        ← 用于生成（编译）自定义 DLL 的工程或脚手架
/ContentSample     ← 示例内容结构；把生成的 DLL 放这里，再按需打包/上传
```

> 以上结构可在仓库主页看到（`DLLBuilder` / `ContentSample` 目录）。

---

## 用 DLLBuilder 生成你的 DLL
> 目标：得到一个 `YourModName.dll`。

1. **克隆仓库**
   ```bash
   git clone https://github.com/MiniFunGame/MiniMartialWorld-CustomCharacters
   ```

2. **打开 DLLBuilder 项目**
   - 用 VS 打开 `DLLBuilder` 里的 `.sln` 或 `.csproj`（如未提供解决方案文件，请在该目录下新建 Class Library 工程）。
   - 将你的功能代码（如自定义角色、被动、事件等）添加到该项目中。  
     - 参考示例：实现 `IWorkshopCharacterModifier` 添加角色；继承 `ExclusiveEffect` 写被动；继承 `Event` 写事件。
   
3. **检查引用**
   - 若项目里需要直接调用游戏类型（如 `Character`、`Event`、`ExclusiveEffect`），请添加游戏 DLL 引用（见“环境准备”）。
   
4. **编译（Build）**
   - VS：选择 `Release`，点击 **Build**。
   - 产物：在 `DLLBuilder/bin/Release/`（或 `bin/Debug/`）获得 `YourModName.dll`。

> 小贴士：建议把显示名（`Name`）与类名区分，例如事件类名用于控制台触发，显示名用于游戏内 UI。

---

## 放入 ContentSample 并准备发布
> 目标：把你的 DLL 放进样例目录，然后按需上传到创意工坊。

1. **创建一个模组子目录（推荐）**
   ```text
   ContentSample/
     YourModName/
       YourModName.dll
       readme.md          ← 给订阅者看的说明（可选）
       preview.png        ← 创意工坊封面图（可选）
   ```

   - 也可以直接把 DLL 放在 `ContentSample` 根目录，但**为避免与其他示例混淆，建议独立子文件夹**。

2. **（可选）补充你的说明**
   - 在 `readme.md` 中写明：模组功能、兼容的游戏版本、使用方法与注意事项。

3. **上传到创意工坊**
   - 进入游戏内/官方上传工具，选择 `ContentSample/YourModName/` 作为上传源（或按工具要求选择对应路径）。
   - 填写条目标题、简介、版本号与变更说明，提交发布。

> 如果你的游戏/工具要求特定打包方式（如 zip / 特殊元数据），请按工具向导操作；通常把 DLL 与资源一并选中上传即可。

---

## 游戏内验证（强烈建议）
发布前，建议在本地做快速验证：

1. **放置路径**：确保游戏在它的“外置 DLL 目录”或你设置的 Mod 目录能扫到这个 DLL。  
2. **功能自检**：
   - 若你添加了 **角色**：新开局/符合条件的模式下检查角色是否出现。
   - 若你添加了 **被动/事件**：在战斗或日常事件中观察触发；或用控制台命令手动触发：  
     - `event <事件类名>`  
     - `skill <技能名>` / `award <关键字>`  
     - `modify <属性> <数值>` / `money <数值>`  
3. **日志**：关注游戏控制台/日志输出，排查是否有类型找不到、引用缺失或反射失败等报错。

---

## 常见问题 FAQ
**Q1：编译报缺少 Unity/游戏 DLL？**  
A：在 DLLBuilder 的项目里添加 `Assembly-CSharp.dll` 与 `UnityEngine*.dll` 的引用（通常位于 `…_Data/Managed/`）。

**Q2：游戏识别不到我的 DLL？**  
A：确认放置路径与游戏的外置 DLL 扫描目录一致；有些游戏只扫描**顶层目录**的 `*.dll`，不要放到过深的嵌套层级。

**Q3：控制台触发事件无反应？**  
A：使用**事件类名**（而非中文展示名）来触发；确认类继承自 `Event` 并可被反射到。

**Q4：多人协作如何管理？**  
A：建议每个模组一个 `ContentSample/YourModName/` 子目录，避免冲突；提交 PR 时附简要说明。

---

## 进阶：自定义内容建议
- **命名唯一**：避免与原游戏/其他模组重复的类名与标识符。
- **平衡性**：为被动/事件设置合理的数值与权重，避免刷屏或强度失衡。
- **本地化**：将文案抽离到本地化文件，以便后续翻译与维护。
- **版本兼容**：在 `readme.md` 中注明支持的游戏版本，并在更新时维护变更日志。

---

祝你创作愉快！如果你希望我**把这份教程直接放进仓库**（或顺手附上一个可运行的示例 DLL 工程骨架），告诉我你希望的目录与命名，我可以直接生成文件给你。
