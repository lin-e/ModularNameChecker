# ModularNameChecker
Easily expandable username availability checker
# Usage
Example code for simple usage in C# Console
```C#
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
Adding a (VERY) simple module
```C#
// Add this to the 'Modules' region of the accountChecker class
public string Foobar() // Name of module
{
  try
  {
    string toTest = new WebClient().DownloadString("http://example.com/signup.php?user=<NAME>".Replace("<NAME>", usernameInput)); // URL of checking API
    if (toTest.Contains(":true,")) // Boolean check
    {
      return "Available"; // Returns available
    }
    else
    {
      return "Taken"; // Returns taken
    }
  }
  catch
  {
    return "Unable to reach server"; // Returns error
  }
}
```
