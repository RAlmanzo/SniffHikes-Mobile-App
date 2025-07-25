# 🐾 SniffHikes Mobile App

A cross-platform mobile app for dog lovers who enjoy hiking in nature — built as an extension of the SniffHikes Web Application.

Target platforms: Android & Windows
Built with Xamarin.Forms and follows the MVVM pattern.

## 📱 Description

The SniffHikes Mobile App allows registered users to:

📍 Explore and register for dog-friendly hiking events

🐕 Manage their profile and add information about their dogs

🧭 Navigate routes via in-app maps or external apps like Waze

💬 Post and read comments about hikes

🇧🇪 Discover dog zones across Belgium suitable for hiking

🛠️ Access an admin section (for authorized users) to manage:

Users

Events

Routes

Community comments

## 🛤️ (In Progress) Record personal hikes, share them with others, and receive feedback from the community

This app is built with a strong focus on user experience, community interaction, and seamless integration with the backend services.

## 🧱 Tech Stack

🔹 Frontend (Mobile):

C#, Xamarin.Forms

XAML for UI

MVVM pattern

DataBinding

Navigation

XUnit for unit testing

🔹 Backend (In Progress):

C# GraphQL API using HotChocolate .NET

Firebase (Cloud Database)

Entity-based services with DTOs and models

📂 Project Structure
pgsql
Kopiëren
Bewerken
📦 SniffHikes.MobileApp/
├── 📁 Domain/
│   └── Models/
│   └── Interfaces/
│   └── Services/
│
├── 📁 Infrastructure/
│   └── Services/
│   └── DTOs/
│
├── 📁 Views/
│   └── Pages (.xaml)
│   └── ViewModels
│
├── 📁 Resources/
│   └── Styles, Images, Fonts
│
├── App.xaml / App.xaml.cs
└── MainPage.xaml / MainPageViewModel.cs

## 🚀 Features

🔐 User Authentication (via backend)

📍 Route Navigation

🐶 Dog-friendly trail discovery

📸 Hike recording and sharing (coming soon)

🔧 Admin controls (moderation interface)

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

🌐 Planning a GraphQL backend with HotChocolate

🚀 Connecting mobile clients to web APIs

📦 Installation & Running
Requires:

.NET SDK 6.0+

Visual Studio with Xamarin Workload

Android SDK or Windows UWP support

Emulator or physical device

Clone & Run
bash
Kopiëren
Bewerken
git clone https://github.com/your-username/sniffhikes-mobileapp.git
cd sniffhikes-mobileapp
Open the .sln file in Visual Studio, build the solution, and run it on:

📱 Android Emulator or device

🖥️ Windows desktop (UWP)

🛠 In Progress
🔧 Full GraphQL backend API using HotChocolate

🗺️ Hike tracking & route sharing

📈 More analytics & profile statistics

📥 Offline caching & sync functionality
