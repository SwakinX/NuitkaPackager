<h1>
<img src="./NuitkaPackager/Assets/WindowIcon.png" align="right">
NuitkaPackager
</h1>

<p>
  <img alt="" src="https://img.shields.io/badge/platform-Windows-blue?style=flat-square&color=4096d8" />
</p>

## 功能简介
基于winUI3的[nutika](https://github.com/Nuitka/Nuitka)GUI，根据个人的使用需求添加了常用的设置选项，可以导出命令，还可以在结束后复制整理好的资源和依赖文件

## 下载安装

前往 [Releases](https://github.com/SwakinX/NuitkaPackager/releases) 下载后解压双击BB图标的 `NuitkaPackager.exe` 打开图形界面

## 注意事项
不提供环境配置，用的默认环境执行，我用的是nuitka2.4..5，没用过老版本，不清楚能否正常使用，以及 `--include-data-file` 和 `--include-data-dir` 我按照官方文档怎么改命令都不行，不过还是保留了选项，可能老版本的能用吧，反正可以直接自动复制整理好的文件

## 界面展示
![image](https://github.com/user-attachments/assets/65d9eab0-c89f-4713-bbd4-6c85c0b1342d)
![image](https://github.com/user-attachments/assets/d4126831-87e7-43e1-9b89-09134582f459)
![image](https://github.com/user-attachments/assets/c50f14b9-be53-4f58-bb34-f4f53c69a4bd)
## 开发相关

做这个主要是为了方便另一个项目打包，以及想学习一下wwinUi3，终端不能显示进度条，之前考虑过嵌入终端[WindowsTerminal](https://github.com/Corillian/WindowsTerminal)，但很久没更新过了还是.net6，提供的Sample跟最新的nuget包都对不上，也找不到其他参考，折腾了很久应该是只能自己编译新版本的了，没有正确的参考怎么用也是个问题，还是算了，打包测试好了，有没有进度条都无所谓，主要还是为了方便以后一键打包

---
