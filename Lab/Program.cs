using System.Text.RegularExpressions;
using AtomCore.Extensions;
using AtomCore.UniqueCodeGenerator;

var isMatch = Regex.IsMatch("+994 702897505", @"^\+(\d{1,4}) (\d{4,14})$");

Console.WriteLine(isMatch);