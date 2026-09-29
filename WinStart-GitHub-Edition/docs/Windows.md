
# Windows

创建三个任务计划：

| 名称 | 触发 |
|------|------|
| Win1223 Idle | RDP断开 |
| Win1223 Resume | RDP连接 |
| Win1223 Startup Sync | 开机 |

Idle：

```http
POST /api/idle
```

Resume：

```http
POST /api/cancel-idle
```
