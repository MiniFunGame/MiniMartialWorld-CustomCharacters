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

## 环境与要求

- **开发工具**：Visual Studio 。  


## 贡献

欢迎 PR：补充更多模板、工具脚本与示例素材。  
如果你在使用中遇到问题，也欢迎提 Issue 反馈。

