using LabManager.Database;
using LabManager.Repositories;
using LabManager.Models;

var databaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(databaseConfig);
var computerRepository = new ComputerRepository(databaseConfig);
var labRepository = new LabRepository(databaseConfig);

// Routing
var modelName = args[0];
var modelAction = args[1];

if(modelName == "Computer")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Computer List: "); 
        foreach (var computer in computerRepository.GetAll())
        {
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");
            
        }
    } 
    
    if (modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Save(computer);
    }

    if (modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var ram = args[3];
        var processor = args[4];

        var computer = new Computer(id, ram, processor);
        computerRepository.Update(computer);
    }

    if(modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);

        if (computerRepository.ExistsByID(id))
        {
            computerRepository.Delete(id);
        }
        else
        {
            Console.WriteLine($"O Computador {id} não existe.");
        }
    }

    if(modelAction == "Show") 
    {
        var id = Convert.ToInt32(args[2]);

        if (computerRepository.ExistsByID(id))
        {
            var computer = computerRepository.GetById(id);
            Console.WriteLine($"{computer.Id}, {computer.Ram}, {computer.Processor}");   
        }
        else
        {
            Console.WriteLine($"O Computador {id} não existe.");
        }
    }
}

if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        Console.WriteLine("Lab List: ");
        foreach (var lab in labRepository.GetAll())
        {
            Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}");
        }
    } 
    
    if (modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = Convert.ToChar(args[5]);

        var lab = new Lab(id, number, name, block);
        labRepository.Save(lab);
    }  

    if (modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = Convert.ToChar(args[5]);

        var lab = new Lab(id, number, name, block);
        labRepository.Update(lab);
    }

    if(modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);

        if (labRepository.ExistsByID(id))
        {
            labRepository.Delete(id);
        }
        else
        {
            Console.WriteLine($"O Laboratório {id} não existe.");
        }
    }

    if(modelAction == "Show") 
    {
        var id = Convert.ToInt32(args[2]);

        if (labRepository.ExistsByID(id))
        {
            var lab = labRepository.GetById(id);
            Console.WriteLine($"{lab.Id}, {lab.Number}, {lab.Name}, {lab.Block}");  
        }
        else
        {
            Console.WriteLine($"O Laboratório {id} não existe.");
        }
    } 
}
