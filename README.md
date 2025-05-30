# 🚗 Westminster Vehicle Rental System

A simple yet comprehensive console-based application written in **C#** that simulates a vehicle rental management system. The program demonstrates solid **Object-Oriented Programming (OOP)** principles, modular design with interfaces, and practical use of inheritance and polymorphism.

---

## 📋 Features

- Add, list, and remove vehicles (Cars, ElectroCars, Vans, Motorbikes)
- Make, change, and delete **reservations** for specific time periods
- Assign **drivers** to reservations with validated input
- Check **availability** of vehicles based on schedule
- Generate textual **rental reports**
- Manage both **customer** and **admin** workflows
- Prevent overlapping bookings

---

## 💡 Object-Oriented Design

This project was designed with extensibility and separation of concerns in mind:

| Component         | Description |
|------------------|-------------|
| `Vehicle`         | Abstract base class for all vehicles. Implements reservation logic. |
| `Car`, `ElectroCar`, `Van`, `Motorbike` | Specific vehicle types with additional attributes. |
| `Schedule`        | Encapsulates rental period, driver info, and pricing logic. Implements `IOverlappable` and `IComparable`. |
| `Driver`          | Represents reservation owner. Input collected via console prompts. |
| `WestminsterRentalVehicle` | Main controller class for vehicle and reservation management. Implements both `IRentalCustomer` and `IRentalManager`. |
| `IRentalCustomer`, `IRentalManager` | Interfaces used to separate user/admin responsibilities. |
| `IOverlappable`   | Interface for checking schedule overlaps. |

---

## 🔄 OOP Principles in Practice

| Principle       | How it's applied in this project |
|----------------|-----------------------------------|
| **Encapsulation** | All vehicle and driver data are stored in private fields and accessed via public getters/setters. For example, `Vehicle` encapsulates reservation logic internally, and `Schedule` hides how total price is calculated. |
| **Abstraction** | `Vehicle` is an abstract class that defines common behavior (`GetVehicleInfo`, reservation handling), but cannot be instantiated directly. Each concrete vehicle type must implement its own info logic. |
| **Inheritance** | `Car`, `ElectroCar`, `Van`, and `Motorbike` all inherit from `Vehicle` and extend or override its behavior. |
| **Polymorphism** | The application uses overridden `GetVehicleInfo()` methods to output vehicle-specific details. Methods like `AddReservation` are applied generically to `Vehicle` references, regardless of the concrete type. |

---

## 🧑‍💻 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/en-us/download) (version 6.0 or later recommended)
- Any C# compatible IDE (e.g., Visual Studio, Rider, or VS Code)

### Run the App

```bash
dotnet run
```

You will be presented with a **menu-driven console interface** for interacting with the system.

---

## 🎯 Example Use Cases
As a customer, you can:
- Search for available vehicles by type and date range
- Make a reservation and assign a driver
- Change or cancel existing reservations

As an admin, you can:
- Add new vehicles to the rental fleet
- Remove existing ones
- Generate reservation reports to a file
- View all vehicles sorted by brand

---

## 🛠️ Possible Improvements
- Migrate to WPF or ASP.NET for GUI/Web interface
- Store vehicle and reservation data in a database or file for persistence
- Add unit tests for schedule overlap and reservation logic
- Improve data validation and error handling
- Support different pricing rules (e.g., weekend rates)

---

## 👩‍🏫 Educational Value
This project was made for:
- Learning and applying OOP principles
- Demonstrating practical use of interfaces, inheritance, and encapsulation
- Practicing date manipulation, console I/O, and input validation
