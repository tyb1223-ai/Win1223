
# Architecture

## 核心思想

- Windows 主动通知
- Ubuntu 负责逻辑
- PVE 负责执行

## 网络拓扑

```text
Internet
   │
win.1223.fun:3400
   │
 Ubuntu
   │
 PVE API
   │
 Windows VM101
```
