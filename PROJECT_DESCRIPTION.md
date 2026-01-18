# 🎁 GiftShop E-commerce Platform - Project Description

## 📋 نظرة عامة على المشروع

**GiftShop** هو منصة تجارة إلكترونية متكاملة مبنية على **Microservices Architecture** لتقديم تجربة تسوق متقدمة للمستخدمين. المشروع مصمم ليكون قابلاً للتوسع، موثوقاً، وسهل الصيانة باستخدام أحدث التقنيات والمعماريات الحديثة.

---

## 🏗️ المعمارية (Architecture)

### **Microservices Architecture**
المشروع مبني على نمط **Microservices** حيث كل خدمة مستقلة تماماً ولها قاعدة بيانات خاصة بها، مما يوفر:
- **Scalability**: إمكانية توسيع كل خدمة بشكل مستقل
- **Maintainability**: سهولة الصيانة والتطوير
- **Fault Isolation**: عزل الأخطاء في خدمة واحدة لا تؤثر على الباقي
- **Technology Diversity**: إمكانية استخدام تقنيات مختلفة لكل خدمة

### **API Gateway Pattern**
استخدام **API Gateway** كبوابة موحدة لجميع الخدمات، مما يوفر:
- نقطة دخول واحدة لجميع الطلبات
- توجيه الطلبات للخدمات المناسبة
- إدارة المصادقة والتفويض المركزي

---

## 🔧 الخدمات (Services)

### 1. **IdentityService** 🔐
**المسؤولية**: إدارة المصادقة والتفويض والمستخدمين

#### Features:
- **Authentication**:
  - تسجيل الدخول (Login) مع JWT Tokens
  - تسجيل مستخدم جديد (Sign Up)
  - تحديث Refresh Token
  - التحقق من البريد الإلكتروني (Email Verification)
  
- **Password Management**:
  - نسيان كلمة المرور (Forget Password)
  - إعادة تعيين كلمة المرور (Reset Password)
  
- **User Management**:
  - إدارة المستخدمين والأدوار (Roles)
  - نظام الصلاحيات (Permissions)
  - إدارة User Tokens

- **Queries**:
  - الحصول على المستخدم بالبريد الإلكتروني
  - الحصول على الأدوار والصلاحيات للمستخدم

---

### 2. **ProductCatalogService** 📦
**المسؤولية**: إدارة الكتالوج الكامل للمنتجات والفئات

#### Features:
- **Product Management**:
  - إنشاء منتج جديد مع رفع الصور
  - تحديث المنتج
  - حذف المنتج (Soft Delete)
  - تفعيل/تعطيل المنتج
  - إدارة مخزون المنتجات (Stock Management)
  - دعم المنتجات الأكثر مبيعاً (Best Sellers)
  
- **Category Management**:
  - إنشاء فئة جديدة
  - تحديث الفئة
  - حذف الفئة
  - تفعيل/تعطيل الفئة
  - استعراض جميع الفئات
  
- **Occasion Management**:
  - إدارة المناسبات الخاصة
  - ربط المنتجات بالمناسبات
  - إنشاء QR Codes للمناسبات
  
- **Cart Integration**:
  - إضافة منتج للسلة
  - تحديث كمية المنتج في السلة
  - الحصول على سلة المستخدم

---

### 3. **OrderService** 🛒
**المسؤولية**: إدارة دورة حياة الطلبات بالكامل

#### Features:
- **Order Management**:
  - إنشاء طلب جديد من السلة
  - إعادة الطلب (Re-Order)
  - تحديث حالة الطلب (Order Status)
  - تتبع الطلب (Track Order) مع Real-time Updates
  - إضافة عناصر للطلب
  
- **Order Status Tracking**:
  - Received (تم الاستلام)
  - Preparing (قيد التحضير)
  - OutForDelivery (في الطريق)
  - Delivered (تم التسليم)
  - Cancelled (ملغي)
  
- **Payment Methods**:
  - الدفع عند الاستلام (Cash on Delivery)
  - الدفع بالبطاقة الائتمانية (Credit Card)
  
- **Delivery Features**:
  - تتبع موقع التوصيل (GPS Tracking)
  - معلومات مندوب التوصيل (Delivery Hero)
  - حساب رسوم التوصيل
  - نظام النقاط (Points System)
  
- **Order Status Log**:
  - سجل كامل لتاريخ تغييرات حالة الطلب

---

### 4. **CartService** 🛍️
**المسؤولية**: إدارة سلة التسوق للمستخدمين

#### Features:
- إضافة منتج للسلة
- تحديث كمية المنتج في السلة
- الحصول على سلة المستخدم
- إدارة السلة باستخدام Redis للسرعة

---

### 5. **UserProfileService** 👤
**المسؤولية**: إدارة ملفات المستخدمين والعناوين

#### Features:
- **Profile Management**:
  - تعديل ملف المستخدم
  - الحصول على بيانات المستخدم
  
- **Address Management**:
  - إدارة عناوين المستخدم
  - العناوين للتوصيل

---

### 6. **PromotionService** 🎉
**المسؤولية**: إدارة العروض والخصومات

#### Features:
- إدارة العروض الترويجية
- تطبيق الخصومات على الطلبات
- نظام الكوبونات

---

### 7. **ApiGateway** 🌐
**المسؤولية**: بوابة موحدة لجميع الخدمات

#### Features:
- توجيه الطلبات للخدمات المناسبة
- Load Balancing
- Rate Limiting
- Centralized Authentication

---

## 🛠️ التقنيات المستخدمة (Technologies)

### **Backend Framework**
- **.NET 8** - أحدث إصدار من .NET
- **ASP.NET Core Web API** - لبناء RESTful APIs

### **Database & ORM**
- **SQL Server** - قاعدة البيانات العلائقية
- **Entity Framework Core** - ORM للتعامل مع قاعدة البيانات
- **Code-First Migrations** - إدارة تغييرات قاعدة البيانات

### **Caching**
- **Redis** - للتخزين المؤقت عالي الأداء
  - تخزين سلة التسوق
  - Session Management
  - Caching للبيانات المتكررة

### **Message Queue**
- **RabbitMQ** - Message Broker
- **MassTransit** - Abstraction layer للتعامل مع Message Queues
- **Asynchronous Communication** بين الخدمات

### **Real-time Communication**
- **SignalR** - للاتصال في الوقت الفعلي
  - تتبع الطلبات Live
  - إشعارات فورية

### **Design Patterns & Libraries**
- **MediatR** - لتنفيذ CQRS Pattern
- **FluentValidation** - للتحقق من صحة البيانات
- **AutoMapper** (محتمل) - لتحويل الكائنات

### **Architecture Patterns**
- **CQRS (Command Query Responsibility Segregation)**
  - فصل Commands (الكتابة) عن Queries (القراءة)
  - تحسين الأداء والمرونة
  
- **Repository Pattern**
  - Generic Repository للتعامل مع البيانات
  - Unit of Work Pattern لإدارة المعاملات
  
- **Pipeline Behaviors**
  - Validation Behavior للتحقق من البيانات
  - Transaction Behavior لإدارة المعاملات

### **Containerization**
- **Docker** - لتجميع الخدمات في Containers
- **Docker Compose** - لإدارة وتشغيل جميع الخدمات معاً

### **HTTP Client**
- **HttpClient** - للتواصل بين الخدمات
- **Service-to-Service Communication**

---

## 📐 Design Patterns المستخدمة

### 1. **CQRS Pattern**
- فصل Commands (Create, Update, Delete) عن Queries (Read)
- استخدام MediatR لتنفيذ الـ Pattern
- تحسين الأداء والمرونة

### 2. **Repository Pattern**
- Generic Repository للتعامل مع جميع الـ Entities
- Unit of Work لإدارة المعاملات
- Abstraction layer بين Business Logic و Data Access

### 3. **Dependency Injection**
- استخدام Built-in DI Container في .NET
- Scoped, Transient, Singleton lifetimes

### 4. **Pipeline Behaviors**
- Validation Behavior: التحقق من البيانات قبل التنفيذ
- Transaction Behavior: إدارة المعاملات تلقائياً

### 5. **Middleware Pattern**
- Global Exception Handler للتعامل مع الأخطاء
- Request/Response Logging
- Authentication/Authorization Middleware

---

## 🗄️ قاعدة البيانات (Database Structure)

### **IdentityService Database**
- Users
- Roles
- UserRoles
- Permissions
- RolePermissions
- RefreshTokens
- UserTokens

### **ProductCatalogService Database**
- Products
- Categories
- ProductOccasions
- ProductImages
- ProductAttributes
- ProductTags

### **OrderService Database**
- Orders
- OrderItems
- OrderStatusLogs

### **UserProfileService Database**
- UserProfiles
- UserAddresses

### **CartService**
- استخدام Redis بدلاً من SQL Database

---

## 🔄 تدفق العمل (Workflow)

### **عملية إنشاء الطلب (Order Creation Flow)**
1. المستخدم يضيف منتجات للسلة (CartService)
2. المستخدم يطلب إنشاء طلب (OrderService)
3. OrderService يجلب السلة من Redis
4. OrderService ينشئ الطلب مع OrderItems
5. OrderService يحفظ الطلب في قاعدة البيانات
6. OrderService يرسل إشعار عبر SignalR للمستخدم
7. OrderService يرسل رسالة عبر RabbitMQ للخدمات الأخرى

### **عملية المصادقة (Authentication Flow)**
1. المستخدم يسجل دخول (IdentityService)
2. IdentityService يتحقق من البيانات
3. IdentityService ينشئ JWT Token و Refresh Token
4. المستخدم يستخدم Token للوصول للخدمات الأخرى

---

## 🚀 المميزات الرئيسية

### ✅ **Scalability**
- كل خدمة يمكن توسيعها بشكل مستقل
- استخدام Redis للـ Caching يقلل الحمل على قاعدة البيانات
- Message Queue للتعامل مع الأحمال العالية

### ✅ **Reliability**
- Fault Isolation: خطأ في خدمة واحدة لا يؤثر على الباقي
- Retry Mechanisms للتعامل مع الأخطاء المؤقتة
- Transaction Management لضمان سلامة البيانات

### ✅ **Security**
- JWT Authentication
- Role-Based Access Control (RBAC)
- Password Hashing
- Email Verification

### ✅ **Performance**
- Redis Caching
- Asynchronous Processing
- Database Indexing
- Connection Pooling

### ✅ **Maintainability**
- Clean Architecture
- Separation of Concerns
- SOLID Principles
- Code Reusability

---

## 📦 Deployment

### **Docker Compose**
جميع الخدمات مجمعة في Docker Containers ويمكن تشغيلها باستخدام:
```bash
docker-compose up
```

### **Services Dependencies**
- SQL Server (لكل خدمة قاعدة بيانات خاصة)
- Redis (للـ Caching)
- RabbitMQ (للـ Message Queue)

---

## 🔮 التطويرات المستقبلية (Future Enhancements)

- [ ] إضافة Payment Gateway Integration
- [ ] نظام التقييمات والمراجعات
- [ ] نظام الإشعارات (Push Notifications)
- [ ] Analytics Dashboard
- [ ] Recommendation Engine
- [ ] Multi-language Support
- [ ] Advanced Search with Elasticsearch
- [ ] GraphQL API
- [ ] Event Sourcing
- [ ] Saga Pattern للـ Distributed Transactions

---

## 📝 ملاحظات

- المشروع يستخدم **Clean Architecture** مع فصل واضح للطبقات
- كل خدمة لها **BaseEntity** مع Soft Delete Support
- استخدام **DTOs** لفصل Domain Models عن API Contracts
- **Validation** على جميع المدخلات باستخدام FluentValidation
- **Error Handling** مركزي باستخدام Global Exception Handler

---

## 👨‍💻 التطوير

المشروع مبني باستخدام:
- **.NET 8**
- **C# 12**
- **Entity Framework Core 8**
- **ASP.NET Core 8**

---

**تم إنشاء هذا الوصف بناءً على تحليل شامل لبنية المشروع والكود المصدري.**


