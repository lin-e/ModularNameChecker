# ModularNameChecker
Easily expandable username availability checker
# Usage
Referencing
```
using ModularNameChecker;
```
Example code for simple usage in C# Console
```
accountChecker newChecker = new accountChecker(Console.ReadLine()); // Declares a new checker with the specified name
foreach (Func<string> singleModule in newChecker.checkModules) // Iterates for each module
{
  Console.ResetColor(); // Resets the colour
  Console.Write(singleModule.Method.Name + ": "); // Prints the module name
  string nameAvailability = singleModule(); // Gets the value of said module
  Console.ForegroundColor = (nameAvailability == "Available") ? ConsoleColor.DarkGreen : (nameAvailability == "Taken") ? ConsoleColor.DarkRed : ConsoleColor.Yellow; // Changes colour depending on the previous output
  Console.Write(nameAvailability + Environment.NewLine); // Prints output
}
```
