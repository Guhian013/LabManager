# LabManager
> Console Application using .NET

## Functionalities
- Inserts the "Computer" entities to the "Computers" table in the database
- Lists all "Computer" entities inserted in the "Computers" table present in the database
- Inserts the "Lab" entities to the "Lab" table in the database
- Lists all "Lab" entities inserted in the "Lab" table present in the database

## Technologies Used
- .NET SDK 6.0.201

## How to Run It
- Clone the repository with this command:
```
git clone https://github.com/Guhian013/LabManager.git
```

- To insert "Computer" entities, use this command on the terminal:
```
dotnet run -- Computer New [Id] [Ram] [Processor]
```

- To list all the "Computer" entities from the database, use this command on the terminal:
```
dotnet run -- Computer List
```

- To insert "Lab" entities, use this command on the terminal:
```
dotnet run -- Lab New [Id] [Number] [Name] [Block]
```

- To list all the "Lab" entities from the database, use this command on the terminal:
```
dotnet run -- Lab List
```