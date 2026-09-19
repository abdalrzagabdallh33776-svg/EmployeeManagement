# EmployeeManagement

مشروع إدارة موظفين كامل باستخدام ASP.NET Core MVC و.NET 8 وEntity Framework Core وSQL Server.

## التشغيل في Visual Studio
1. نزّل المشروع من **Code > Download ZIP** ثم فك الضغط.
2. افتح الملف `EmployeeManagement.csproj` في Visual Studio 2022.
3. تأكد من تثبيت workload: **ASP.NET and web development**.
4. شغّل المشروع بالضغط على `Ctrl+F5`.

يستخدم المشروع LocalDB افتراضياً:
`(localdb)\\MSSQLLocalDB`

ينشئ التطبيق قاعدة البيانات والجداول والبيانات التجريبية تلقائياً عند التشغيل الأول. إذا لم يكن LocalDB مثبتاً، عدّل `ConnectionStrings:DefaultConnection` في `appsettings.json` إلى اسم SQL Server لديك.

يتضمن: إدارة الموظفين، البحث، إدارة الأقسام، الإضافة والتعديل والحذف، وواجهة عربية RTL.
