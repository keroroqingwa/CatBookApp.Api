# 源码说明

当前项目是[喵喵看书]微信小程序的后端api数据接口项目，也是管理后台的api接口项目；将源码下载到本地后用vs2019打开后会看到有两个web应用程序：WechatMiniProgram.Api、Backstage.Api，就是分别对应小程序端和管理后台的api数据接口程序

项目是基于.net core 3.1，使用abp(5.5.0)框架进行开发，若要阅读理解项目源码，建议先了解abp...

数据库使用的是MySQL，如果想改为其他数据库，需要自己去百度一下怎么改，很简单的

# 项目启动

* step1：vs2019打开项目，生成解决方案，确保没有编译异常。如果有，请自行百度解决...
* step2：修改数据库连接字符串，文件位置：Configs/appsettings.json，两个api项目都要改哦。因为涉及到微信小程序的授权登录，所以 Configs/wechatsettings.json 文件里配置了小程序appid和secret，请自行修改为自己的小程序配置信息
* step3：有关数据库的创建，使用“EF code first Migration”进行数据库迁移。具体操作：先将WechatMiniProgram.Api设置为启动项目（重要操作！），打开程序包管理器控制台并指定默认项目为CatBookApp.EntityFrameworkCore，输入命令“Add-Migration CreateDatabase”，回车，再输入“update-database”，回车。如果有使用安装navicat，可以打开看下，这时候数据库应该是创建好了。
* step4：F5，启动~

