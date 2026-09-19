# نسخة Visual Studio 2015

هذه نسخة ASP.NET MVC 5 على .NET Framework 4.6.1، وليست ASP.NET Core.

## التشغيل
1. ثبّت Visual Studio 2015 مع workload **Web Developer Tools** و.NET Framework 4.6.1 Developer Pack.
2. افتح `EmployeeManagementVS2015.sln`.
3. من NuGet نفّذ Restore Packages، أو ابْنِ المشروع ليتم تنزيل الحزم.
4. شغّل بـ IIS Express.

الاتصال الافتراضي هو LocalDB: `(LocalDB)\\MSSQLLocalDB`.
ينشئ Entity Framework الجداول عند أول تشغيل. إذا لم يوجد LocalDB، عدّل `VS2015/Web.config`.
