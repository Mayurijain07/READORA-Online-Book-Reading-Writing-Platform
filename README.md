# 📖 Readora — Online Book Reading & Writing Platform

![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-C%23-512BD4?style=flat-square)
![Web API](https://img.shields.io/badge/Web_API-RESTful-6DB33F?style=flat-square)
![Database](https://img.shields.io/badge/Database-SQL_Server-CC2927?style=flat-square)
![Auth](https://img.shields.io/badge/Auth-JWT-FB8C00?style=flat-square)
![ORM](https://img.shields.io/badge/ORM-Entity_Framework-9B1B6E?style=flat-square)
![Frontend](https://img.shields.io/badge/Frontend-Bootstrap-7952B3?style=flat-square)
![HTML](https://img.shields.io/badge/HTML-5-E34F26?style=flat-square)
![CSS](https://img.shields.io/badge/CSS-3-1572B6?style=flat-square)

---

## 📌 Overview

Readora is a web-based platform designed for both **readers and writers** — a free, category-wise reading and publishing space where creativity comes without subscription barriers or forced identity exposure.

Most existing platforms push users to reveal personal information, lock features behind paywalls, or overwhelm beginners with cluttered interfaces. Readora fixes this by letting writers publish freely and anonymously, giving readers a clean category-wise browsing experience, and keeping the entire platform free — with a secure admin panel for managing users, content, categories, and platform activities.

The system runs on a **single unified user profile** — every user can switch between reading and writing without creating separate accounts, and their history/interactions stay consistent across both roles.

---



## 🎯 Key Objectives

- ✅ Categorized Reading
- ✅ User-Friendly UI
- ✅ Community Interactions
- ✅ Dual-Role User Profiles (Reader + Writer in one account)
- ✅ Secure Admin Panel
- ✅ Anonymous or Public Publishing — no forced identity exposure
- ✅ Fully free — no paid restrictions

---

## 🧩 Module Description

| Module | What it does |
|---|---|
| 🔐 **Login** | Authenticates users and allows secure access — verifies credentials so only authorized users can access platform features |
| 👤 **Profile Creation** | Creates a single user profile (name, username, bio, picture) used for both reading and writing — no separate accounts needed |
| 📖 **Reader** | Browse, read, like, comment, share, favourite, and follow/unfollow writers — maintains reading history across role switches |
| ✍️ **Writer** | Write, edit, and publish stories or posts — writers can also read, like, comment, and follow other users |
| 💬 **Content Interaction** | Handles likes, comments, shares, favourites, follows, and draft-saving — maintains interaction history across both roles |
| ⚙️ **Profile Management** | Update existing profile details — edit name, username, bio, and profile picture anytime |
| 🛡️ **Admin** | Manages users, monitors activity, controls content, removes inappropriate data, and responds to feedback/queries |
| 📩 **Feedback and Query** | Users submit questions, issues, and suggestions — admin reviews and responds, improving platform experience |
---

### 🏗️ Project Architecture

The project follows a clean, modular, and layered architecture to maintain separation of concerns and make the application easier to manage and maintain.

- **Presentation / API Layer (Controllers):** Handles incoming HTTP requests, performs request validation, and returns appropriate API responses.
- **Service Layer (Business Logic):** Handles the application's core business logic, user operations, content validation, and workflow processing.
- **Repository Layer (Data Access):** Handles CRUD operations and data retrieval using Entity Framework Core.
- **Data Layer (`ReadoraDbContext`):** Manages entity relationships, database configurations, and communication with SQL Server through Entity Framework Core.
- **Abstraction Layer (Interfaces):** Defines service and repository contracts such as `IService` and `IRepository`, supporting loose coupling and dependency injection.

```
Client / Razor Pages
        ↓
    Controllers
        ↓
     Services
        ↓
   Repositories
        ↓
  ReadoraDbContext
        ↓
    SQL Server
```
---

## 🔒 Security Features

- 🔑 JWT-based authentication (`Microsoft.AspNetCore.Authentication.JwtBearer`) for secure API access
- 🔐 Passwords hashed with **BCrypt** — never stored in plain text
- 🛡️ Role-based access control (User, Admin)
- 🚫 Prevents unauthorized access via direct URL entry
- 👁️ Admin-level monitoring and dashboard reporting
- 🔗 Data integrity maintained through relational constraints (PK/FK) in SQL Server

---

## 🗄️ Database Design (high-level)

Built on **SQL Server** with Entity Framework Core, using a normalized schema including:

`UserDetails` · `RoleDetailstbl` · `ContentDetails` · `ContentInteractionDetails` · `CommentDetails` · `LikeDetails` · `FavouriteDetails` · `FollowerDetails` · `FeedbackDetails` · `QueryDetails` · `ReadingHistoryDetails` · `CategoryDetails` · `AdminDetails`

---

## 🛠️ Technologies Used

**Backend:**  ASP.NET Core Web API, C#

**Auth:**  JWT Bearer Authentication, BCrypt password hashing

**ORM:**  Entity Framework Core + EF Core SQL Server provider

**Database:**  Microsoft SQL Server

**Frontend:**  Razor Pages, HTML5, CSS3, Bootstrap, jQuery

**API Docs:**  Swagger (Swashbuckle.AspNetCore)

**IDE:**  Visual Studio 2022

---

## 🚀 Getting Started

```bash
git clone https://github.com/Mayurijain07/READORA-Online-Book-Reading-Writing-Platform.git
```

Open in Visual Studio 2022, restore NuGet packages, update the connection string in `appsettings.json`, run `dotnet ef database update`, then `dotnet run`. Swagger UI will be available for testing the API endpoints directly.

---

## 📈 Future Enhancements

- 🎯 Recommendation system for users based on reading history and preferences
- 📊 Detailed interaction and engagement analytics for writers and readers
- ✍️ Collaborative writing and co-authoring of stories
- 🛡️ Advanced content moderation and reporting features for better platform management
- 💎 Premium subscriptions for exclusive content and additional features
---

## 🙌 Acknowledgement

Readora aims to give every reader and writer a free, accessible, and identity-safe space to explore stories and share their creativity — without paywalls, clutter, or forced exposure of personal identity.

---

Home Page: -
<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/32080666-581a-4c8e-97ff-ddada7d90455" />

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/cd7e605c-2780-4865-920b-4e362a61bde0" />


About Us Page :

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/b58dc736-e737-4479-918f-316ab45d0f9f" />

<img width="940" height="528" alt="image" src="https://github.com/user-attachments/assets/003adab0-4e91-4880-8f6f-a3909c4fd47c" />


Browse Page:

<img width="940" height="528" alt="image" src="https://github.com/user-attachments/assets/ea450ea0-207a-419c-83af-d4283314f21b" />

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/0558e237-b82d-4ea1-a9ae-c7c5fbb6c6d2" />


<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/3b07243b-80e4-4257-81fb-e16fd328ffeb" />
Categories Page:

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/6aed577f-f69c-4acf-a51e-ce0956354f43" />

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/88e56b12-9d9e-4128-b151-d7499f69a9be" />
Login Page:

<img width="940" height="528" alt="image" src="https://github.com/user-attachments/assets/41efc643-91e2-4b20-91ca-82690717ecdd" />
Register Page:

<img width="940" height="528" alt="image" src="https://github.com/user-attachments/assets/29dafbab-6c12-466b-9d66-f11629fd6b27" />
  Writer Dashboard:

 <img width="940" height="517" alt="image" src="https://github.com/user-attachments/assets/a2ed36d0-9003-4d0d-a9da-67be7d5e4dfd" />

 <img width="940" height="513" alt="image" src="https://github.com/user-attachments/assets/f1999d72-bd8a-494d-b180-d5aa21622423" />
Write Page:

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/d81f3164-a68f-4152-96d2-c14ca753e115" />
 Writer Profile Page
 
Private Profile Page: 

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/72ddd953-18d1-4cf1-addb-9d1011a59c17" />

<img width="940" height="527" alt="image" src="https://github.com/user-attachments/assets/fafaa387-23b0-42c6-b2d6-190dc65fdf22" />
Public Profile Page:

 <img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/37a69ecd-b2dd-4489-b90a-dd39df53f624" />
Reader Dashboard: 

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/f83274bd-d0a4-4e82-9f36-e328d899b033" />
Reader Profile Page: 

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/a3877389-354f-40a4-a5c2-57588a39c424" />
Details Page: 

 <img width="1090" height="613" alt="image" src="https://github.com/user-attachments/assets/491a3476-77a5-4ac4-835d-c9885ede4b8a" />

 <img width="1090" height="613" alt="image" src="https://github.com/user-attachments/assets/0869ba04-0fd1-497d-86c6-7cbc44f0e0a8" />
Edit Profile Page: 

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/ef5084a4-a93d-4f64-a760-7b1a12331328" />

Support Page: 

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/4eafba29-0983-4ca2-8a89-5d41e727a7d3" />

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/3b886bb5-eafa-4e14-b030-1a883d59e9f9" />
Admin Dashboard Page:  

<img width="940" height="528" alt="image" src="https://github.com/user-attachments/assets/6f7f00f6-2cbf-4408-90b4-b170ed58c184" />
Admin Dashboard Page: 


<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/05fc792d-960a-44da-a7e0-f1017449ed99" />

User Management Page: 

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/70f7f428-a782-424e-8a0b-79d1ea66b088" />
Content Management Page: 

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/dd8ce32f-1321-4f6b-a467-873b693cb5b7" />
Categories Management Page: 

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/45853f30-f626-4021-bbbc-d738698bcc6b" />
Feedback Management Page:

<img width="940" height="527" alt="image" src="https://github.com/user-attachments/assets/e1d2be72-4488-48ef-8f88-bb9239587ffb" />
Queries Management Page: 

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/d4a8b79e-f5c9-40fc-9875-bec3fae3fcdd" />

Reports Page: 

<img width="940" height="529" alt="image" src="https://github.com/user-attachments/assets/9d0b115d-718f-41e2-b1ae-d30bff80ebc8" />

## 📌 Status

Built as a final-year academic project. Actively looking to polish and extend it as part of ongoing learning.

---
*Feel free to explore, raise issues, or suggest improvements!*
