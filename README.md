# 🐾 SniffHikes Mobile App

A cross-platform mobile app for dog lovers who enjoy hiking in nature, build as an extension of the SniffHikes Web Application.

This app is built with a strong focus on user experience, community interaction, and seamless integration with the backend services.

Target platforms: Android & Windows
Built with Xamarin.Forms and follows the MVVM pattern.

---

## 📱 Description

The SniffHikes Mobile App allows registered users to:

📍 Explore and register for dog-friendly events

🐕 Manage their profile and add information about their dogs

🧭 Navigate routes via external apps like Waze / google maps

💬 Post and read comments about hikes

🇧🇪 Discover dog zones across Belgium suitable for hiking

🛠️ Access an admin section (for authorized users) to manage:

Users

Events

Routes

Community comments

---

## 🛤️ (In Progress) Record personal hikes, share them with others, and receive feedback from the community

---

## 🧱 Tech Stack

🔹 Frontend (Mobile):

- C#, Xamarin.Forms

- XAML for UI

- MVVM pattern

- DataBinding

- Navigation

XUnit for unit testing

🔹 Backend:

- C#, domain and infrastructure

- Entity-based services with DTOs and models

- Firebase (Realtime Database)

- Firebase authentication

---

### 📂 Project Structure

📦 MDE.Project.Rosseel_Almanzo/

├── 📁 Constants/

├── 📁 Domain/

│   └── Models/

│   └── Services/

│

├── 📁 Infrastructure/

│   └── DTOs/

│   └── Services/

│

├── 📁 Pages/

│   └── Pages (.xaml)

│

├── 📁 Styles/

│   └── Styles (.xaml)

│

├── 📁 ViewModels/

│

├── App.xaml / App.xaml.cs

├── MDE.Project.Rosseel_Almanzo.Android

├── MDE.Project.Rosseel_Almanzo.UWP

└── MDE.Project.Rosseel_Almanzo.Tests

---

## 🚀 Features

🔐 User Authentication (via Firebase authentication)

🔐 User Authorization (via token stored in SecureStorage)

📍 Route Navigation

🐶 Dog-friendly trail discovery

📸 Hike recording and sharing (coming soon)

🔧 Admin controls

💬 Community-based commenting system

🌐 Planned integration with GraphQL API & Firebase

## 🧠 What I Learned

Throughout this project, I gained practical experience with:

📲 Developing cross-platform mobile apps using Xamarin.Forms

🧭 Implementing MVVM architecture and data binding

📐 Designing responsive UIs in XAML

🧠 Structuring applications using Domain-Driven Design (DDD)

🧩 Creating reusable services and DTOs

🔄 Integrating with cloud databases like Firebase

🧪 Writing and running unit tests with XUnit

🚀 Connecting mobile clients to web APIs

---

## 📦 Installation & Running

### Requires:

- .NET SDK 8.0+

- Visual Studio with Xamarin Workload

- Android SDK or Windows UWP support

- Emulator or physical device

### Clone & Run
```bash
git clone https://github.com/RAlmanzo/SniffHikes-Mobile-App.git
```
Open the .sln file in Visual Studio, build the solution, and run it on:

📱 Android Emulator or device

🖥️ Windows desktop (UWP)

---

## 🛠 In Progress
🔧 Full GraphQL backend API using HotChocolate and ASP.NET Web API with Onion/ Clean Architecture

🗺️ Hike tracking & route sharing

📥 Offline caching & sync functionality

---

## 📬 Contact

Want to connect or ask questions?

📧 Email: ralmanzo@gmail.com

💼 LinkedIn: https://www.linkedin.com/in/rosseel-almanzo-5241172ba/

🐙 GitHub: https://github.com/RAlmanzo
